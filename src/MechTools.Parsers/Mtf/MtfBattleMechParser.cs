using MechTools.Parsers.Enums;
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers.Mtf;

public sealed class MtfBattleMechParser<TMech> : IBattleMechParser<TMech>
{
	private enum Mode
	{
		Default,
		Weapons,
		EquipmentAtLocation,
	}

	private const int ScratchBufferLength = 2_048;
	private const int LongScratchBufferLength = ScratchBufferLength * 2;

	private readonly IBattleMechBuilder _builder;

	private bool _disposed;
	private BattleMechEquipmentLocation? _equipmentLocation;
	private int _lineCount = 1;
	private char[]? _longScratchBuffer;
	private Mode _mode;
	private char[]? _scratchBuffer;

	public MtfBattleMechParser(IBattleMechBuilder<TMech> builder)
	{
		_builder = builder;
	}

	public TMech? Parse(ReadOnlySpan<char> chars)
	{
		if (chars.IsEmpty)
		{
			// TODO: Take a stab at min length
			return default;
		}

		// TODO: Non-sequence based version
		return ProcessSource(chars);
	}

	public TMech? Parse(ReadOnlyMemory<byte> memory)
	{
		if (memory.IsEmpty)
		{
			// TODO: Take a stab at min length
			return default;
		}

		// TODO: Implement properly and remove this allocation.
		var chars = Encoding.UTF8.GetString(memory.Span);
		return ProcessSource(chars);
	}

	public async Task<TMech?> ParseAsync(Stream stream, CancellationToken ct = default)
	{
		ArgumentNullException.ThrowIfNull(stream);

		if (!stream.CanRead)
		{
			return default;
		}

		await ProcessSourceAsync(PipeReader.Create(stream), ct).ConfigureAwait(false);
		// TODO: Implement properly instead of hacking around
		return ((IBattleMechBuilder<TMech>)_builder).Build();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private ReadOnlySpan<char> GetCharsRare(scoped in ReadOnlySequence<byte> sequence)
	{
		char[] scratchBuffer;
		if (sequence.Length <= LongScratchBufferLength)
		{
			_longScratchBuffer ??= ArrayPool<char>.Shared.Rent(LongScratchBufferLength);
			scratchBuffer = _longScratchBuffer;
		}
		else
		{
			// Extra long line, use a one-off buffer.
			scratchBuffer = new char[sequence.Length];
		}

		var charCount = Encoding.UTF8.GetChars(in sequence, scratchBuffer);
		return _longScratchBuffer.AsSpan(start: 0, length: charCount).Trim();
	}

	private void ProcessBuffer(ref ReadOnlySequence<byte> buffer)
	{
		var scratchBuffer = _scratchBuffer!;

		while (true)
		{
			const byte newLine = (byte)'\n';

			if (buffer.PositionOf(newLine) is var position && !position.HasValue)
			{
				break;
			}

			var sequence = buffer.Slice(buffer.Start, position.Value);

			ReadOnlySpan<char> line;
			if (sequence.Length <= scratchBuffer.Length)
			{
				var charCount = Encoding.UTF8.GetChars(in sequence, scratchBuffer);
				line = scratchBuffer.AsSpan(start: 0, length: charCount).Trim();
			}
			else
			{
				line = GetCharsRare(in sequence);
			}

			ProcessLine(line);

			_lineCount++;
			buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
		}
	}

	private void ProcessLine(ReadOnlySpan<char> line)
	{
		if (line.IsEmpty)
		{
			_mode = Mode.Default;
			_equipmentLocation = null;
			return;
		}
		else if (line[0] == MtfSections.Comment)
		{
			_builder.AddComment(line);
			return;
		}

		switch (_mode)
		{
			case Mode.Default:
				ProcessDefaultLine(line);
				break;
			case Mode.Weapons:
				_builder.AddWeaponToWeaponList(line);
				break;
			case Mode.EquipmentAtLocation:
				if (_equipmentLocation.HasValue)
				{
					_builder.AddEquipmentAtLocation(line, _equipmentLocation.Value);
				}
				else
				{
					ThrowHelper.ExceptionToSpecifyLater();
				}
				break;
			default:
				// TODO: Impossible.
				break;
		}
	}

	private void ProcessDefaultLine(ReadOnlySpan<char> line)
	{
		var delimiterIndex = line.IndexOf(':');
		if ((uint)delimiterIndex > 64)
		{
			// TODO: `Antlion LK-3D` - Why on earth do we randomly have markup here?
			ThrowHelper.ExceptionToSpecifyLater();
		}

		// TODO: Think harder about cutting whitespace off here - it might hurt text blobs, but it's very convenient...
		var content = line.Length != delimiterIndex ? line[(delimiterIndex + 1)..].Trim() : [];

		Span<char> section = (stackalloc char[64])[..delimiterIndex];
		_ = line[..delimiterIndex].ToUpperInvariant(section);

		// TODO: Consider adding a call to the builder for starting each equipment section. Nop for defaults.
		// TODO: Handle the unknown case.

		switch (section.Length)
		{
			case <= 7:
				ProcessShortLengthTags(section, content);
				break;
			case >= 10:
				ProcessLongLengthTags(section, content);
				break;
			default: // 8,9
				ProcessMediumLengthTags(section, content);
				break;
		}

		void ProcessShortLengthTags(ReadOnlySpan<char> section, ReadOnlySpan<char> content)
		{
			switch (section)
			{
				case MtfSections.Armour:
					_builder.SetArmour(content);
					break;
				case MtfSections.Chassis:
					_builder.SetChassis(content);
					break;
				case MtfSections.Cockpit:
					_builder.SetCockpit(content);
					break;
				case MtfSections.Configuration:
					_builder.SetConfiguration(content);
					break;
				case MtfSections.Engine:
					_builder.SetEngine(content);
					break;
				case MtfSections.Era:
					_builder.SetEra(content);
					break;
				case MtfSections.Gyro:
					_builder.SetGyro(content);
					break;
				case MtfSections.History:
					_builder.SetHistory(content);
					break;
				case MtfSections.JumpMp:
					_builder.SetJumpMp(content);
					break;
				case MtfSections.Lam:
					_builder.SetLam(content);
					break;
				case MtfSections.Mass:
					_builder.SetMass(content);
					break;
				case MtfSections.Model:
					_builder.SetModel(content);
					break;
				case MtfSections.Motive:
					_builder.SetMotive(content);
					break;
				case MtfSections.MulId:
					_builder.SetMulId(content);
					break;
				case MtfSections.Myomer:
					_builder.SetMyomer(content);
					break;
				case MtfSections.NoCrit:
					_builder.SetNoCrit(content);
					break;
				case MtfSections.Notes:
					_builder.SetNotes(content);
					break;
				case MtfSections.Quirk:
					_builder.AddQuirk(content);
					break;
				case MtfSections.Role:
					_builder.SetRole(content);
					break;
				case MtfSections.Source:
					_builder.SetSource(content);
					break;
				case MtfSections.WalkMp:
					_builder.SetWalkMp(content);
					break;
				case MtfSections.Weapons:
					_builder.SetWeaponListCount(content);
					_mode = Mode.Weapons;
					break;
				case MtfSections.EquipmentLocation.Head:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.Head;
					break;
				default:
					ThrowHelper.ExceptionToSpecifyLater();
					break;
			}
		}

		void ProcessMediumLengthTags(ReadOnlySpan<char> section, ReadOnlySpan<char> content)
		{
			switch (section)
			{
				case MtfSections.ArmourLocation.CentreLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.CentreLeg);
					break;
				case MtfSections.ArmourLocation.CentreTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.CentreTorso);
					break;
				case MtfSections.ArmourLocation.FrontLeftLeg:
				case MtfSections.ArmourLocation.LeftLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftLeg);
					break;
				case MtfSections.ArmourLocation.FrontRightLeg:
				case MtfSections.ArmourLocation.RightLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightLeg);
					break;
				case MtfSections.ArmourLocation.Head:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.Head);
					break;
				case MtfSections.ArmourLocation.LeftArm:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftArm);
					break;
				case MtfSections.ArmourLocation.LeftTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftTorso);
					break;
				case MtfSections.ArmourLocation.RearLeftLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearLeftLeg);
					break;
				case MtfSections.ArmourLocation.RearRightLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearRightLeg);
					break;
				case MtfSections.ArmourLocation.RightArm:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightArm);
					break;
				case MtfSections.ArmourLocation.RightTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightTorso);
					break;
				case MtfSections.ArmourLocation.RearCentreTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearCentreTorso);
					break;
				case MtfSections.ArmourLocation.RearLeftTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearLeftTorso);
					break;
				case MtfSections.ArmourLocation.RearRightTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearRightTorso);
					break;
				case MtfSections.ClanName:
					_builder.SetClanName(content);
					break;
				case MtfSections.Ejection:
					_builder.SetEjection(content);
					break;
				case MtfSections.Generator:
					_builder.SetGenerator(content);
					break;
				case MtfSections.ImageFile:
					_builder.SetImageFile(content);
					break;
				case MtfSections.Overview:
					_builder.SetOverview(content);
					break;
				case MtfSections.Structure:
					_builder.SetStructure(content);
					break;
				case MtfSections.TechBase:
					_builder.SetTechBase(content);
					break;
				case MtfSections.EquipmentLocation.LeftLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.LeftLeg;
					break;
				case MtfSections.EquipmentLocation.RightLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RightLeg;
					break;
				case MtfSections.EquipmentLocation.LeftArm:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.LeftArm;
					break;
				case MtfSections.EquipmentLocation.RightArm:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RightArm;
					break;
				default:
					ThrowHelper.ExceptionToSpecifyLater();
					break;
			}
		}

		void ProcessLongLengthTags(ReadOnlySpan<char> section, ReadOnlySpan<char> content)
		{
			switch (section)
			{
				case MtfSections.BaseChassisHeatSinks:
					_builder.SetBaseChassisHeatSinks(content);
					break;
				case MtfSections.Capabilities:
					_builder.SetCapabilities(content);
					break;
				case MtfSections.Deployment:
					_builder.SetDeployment(content);
					break;
				case MtfSections.HeatSinkKit:
					_builder.SetHeatSinkKit(content);
					break;
				case MtfSections.HeatSinks:
					_builder.SetHeatSinks(content);
					break;
				case MtfSections.Manufacturer:
					_builder.SetManufacturer(content);
					break;
				case MtfSections.PrimaryFactory:
					_builder.SetPrimaryFactory(content);
					break;
				case MtfSections.RulesLevel:
					_builder.SetRulesLevel(content);
					break;
				case MtfSections.SystemManufacturer:
					_builder.SetSystemManufacturer(content);
					break;
				case MtfSections.SystemModel:
				case MtfSections.SystemModelTypo:
					_builder.SetSystemModel(content);
					break;
				case MtfSections.WeaponQuirk:
					_builder.AddWeaponQuirk(content);
					break;
				case MtfSections.EquipmentLocation.CentreLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.CentreLeg;
					break;
				case MtfSections.EquipmentLocation.CentreTorso:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.CentreTorso;
					break;
				case MtfSections.EquipmentLocation.FrontLeftLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.LeftLeg;
					break;
				case MtfSections.EquipmentLocation.FrontRightLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RightLeg;
					break;
				case MtfSections.EquipmentLocation.LeftTorso:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.LeftTorso;
					break;
				case MtfSections.EquipmentLocation.RearLeftLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RearLeftLeg;
					break;
				case MtfSections.EquipmentLocation.RearRightLeg:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RearRightLeg;
					break;
				case MtfSections.EquipmentLocation.RightTorso:
					_mode = Mode.EquipmentAtLocation;
					_equipmentLocation = BattleMechEquipmentLocation.RightTorso;
					break;
				default:
					ThrowHelper.ExceptionToSpecifyLater();
					break;
			}
		}
	}

	[MemberNotNull(nameof(_scratchBuffer))]
	private TMech? ProcessSource(ReadOnlySpan<char> chars)
	{
		// TODO: Implement - should be quick.
		_scratchBuffer = ArrayPool<char>.Shared.Rent(ScratchBufferLength);
		throw new NotImplementedException();
	}

	[MemberNotNull(nameof(_scratchBuffer))]
	private async Task ProcessSourceAsync(PipeReader reader, CancellationToken ct)
	{
		_scratchBuffer = ArrayPool<char>.Shared.Rent(ScratchBufferLength);

		try
		{
			while (true)
			{
				var result = await reader.ReadAsync(ct).ConfigureAwait(false);
				if (result.IsCanceled)
				{
					break;
				}

				// Parse the buffer.
				var buffer = result.Buffer;
				ProcessBuffer(ref buffer);

				if (result.IsCompleted)
				{
					// TODO: If mech not completed, throw? Expand on this more.
					break;
				}

				reader.AdvanceTo(buffer.Start, buffer.End);
			}
		}
		// TODO: Error handling.
		//catch (Exception ex)
		//{
		//}
		finally
		{
			// CompleteAsync is a direct wrap of Complete
#pragma warning disable CA1849, S6966 // Call async methods when in an async method
			reader.Complete();
#pragma warning restore CA1849, S6966 // Call async methods when in an async method
		}
	}

	#region IDisposable

	private void Dispose(bool disposing)
	{
		// TODO: Don't dispose for this... It's clunkier for the user and doesn't work with the eventual STJ-style pattern.

		if (!_disposed)
		{
			if (disposing && _scratchBuffer is not null)
			{
				ArrayPool<char>.Shared.Return(_scratchBuffer, clearArray: false);

				if (_longScratchBuffer is not null)
				{
					ArrayPool<char>.Shared.Return(_longScratchBuffer, clearArray: false);
				}
			}

			_disposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	#endregion IDisposable
}
