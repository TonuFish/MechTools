using MechTools.Parsers.Enums;
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers.Mtf;

internal sealed class BattleMechParser : IDisposable
{
	private enum Mode
	{
		Default,
		Weapons,
		EquipmentAtLocation,
	}

	internal int LineNumber { get; private set; }

	private const int ScratchBufferLength = 2_048;
	private const int LongScratchBufferLength = ScratchBufferLength * 2;

	private readonly IBattleMechBuilder _builder;

	private bool _disposed;
	private BattleMechEquipmentLocation? _equipmentLocation;
	private char[]? _longScratchBuffer;
	private Mode _mode;
	private char[]? _scratchBuffer;

	public BattleMechParser(IBattleMechBuilder builder)
	{
		_builder = builder;
	}

	internal void Parse(ReadOnlySpan<char> chars)
	{
		foreach (var bound in chars.Split('\n'))
		{
			ProcessLine(chars[bound]);
		}
	}

	[MemberNotNull(nameof(_scratchBuffer))]
	internal async Task ParseAsync(PipeReader reader, CancellationToken ct)
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

				var buffer = result.Buffer;
				ProcessBuffer(ref buffer);

				if (result.IsCompleted)
				{
					break;
				}

				reader.AdvanceTo(buffer.Start, buffer.End);
			}
		}
		finally
		{
			await reader.CompleteAsync().ConfigureAwait(false);
		}
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
		return _longScratchBuffer.AsSpan(start: 0, length: charCount);
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
				line = scratchBuffer.AsSpan(start: 0, length: charCount);
			}
			else
			{
				line = GetCharsRare(in sequence);
			}

			ProcessLine(line);

			buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
		}
	}

	private void ProcessLine(ReadOnlySpan<char> line)
	{
		LineNumber++;

		var trimmedLine = line.Trim();
		if (trimmedLine.IsEmpty)
		{
			// Not clearing _equipmentLocation each empty line will leaves last value behind, but is side-effect free
			// as it's not used further.
			_mode = Mode.Default;
			return;
		}
		else if (trimmedLine[0] == Sections.Comment)
		{
			_builder.AddComment(trimmedLine);
			return;
		}

		switch (_mode)
		{
			case Mode.Default:
				ProcessDefaultLine(trimmedLine);
				break;
			case Mode.Weapons:
				_builder.AddWeaponToWeaponList(trimmedLine);
				break;
			case Mode.EquipmentAtLocation:
				// Only set via ProcessDefaultLine.SetEquipmentAtLocationMode, will not be null.
				_builder.AddEquipmentAtLocation(trimmedLine, _equipmentLocation!.Value);
				break;
			default:
				ThrowHelper.DebugThrowImpossibleException();
				break;
		}
	}

	private void ProcessDefaultLine(ReadOnlySpan<char> line)
	{
		const int MaxSectionLength = 64;

		var delimiterIndex = line.IndexOf(':');
		if ((uint)delimiterIndex > MaxSectionLength)
		{
			// TODO: Usually markup driven (having info over multiple lines) - Handle this.
			MtfThrowHelper.ThrowMissingSectionTagException(line);
		}

		var content = line.Length != delimiterIndex ? line[(delimiterIndex + 1)..].Trim() : [];

		var section = (stackalloc char[MaxSectionLength])[..delimiterIndex];
		// Invariant guarantees lengths will match, don't validate lengths.
		_ = line[..delimiterIndex].ToUpperInvariant(section);

		// TODO: Consider adding a call to the builder for starting each equipment section. Nop for defaults.

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
				case Sections.Armour:
					_builder.SetArmour(content);
					break;
				case Sections.Chassis:
					_builder.SetChassis(content);
					break;
				case Sections.Cockpit:
					_builder.SetCockpit(content);
					break;
				case Sections.Configuration:
					_builder.SetConfiguration(content);
					break;
				case Sections.Engine:
					_builder.SetEngine(content);
					break;
				case Sections.Era:
					_builder.SetEra(content);
					break;
				case Sections.Gyro:
					_builder.SetGyro(content);
					break;
				case Sections.History:
					_builder.SetHistory(content);
					break;
				case Sections.JumpMp:
					_builder.SetJumpMp(content);
					break;
				case Sections.Lam:
					_builder.SetLam(content);
					break;
				case Sections.Mass:
					_builder.SetMass(content);
					break;
				case Sections.Model:
					_builder.SetModel(content);
					break;
				case Sections.Motive:
					_builder.SetMotive(content);
					break;
				case Sections.MulId:
					_builder.SetMulId(content);
					break;
				case Sections.Myomer:
					_builder.SetMyomer(content);
					break;
				case Sections.NoCrit:
					_builder.SetNoCrit(content);
					break;
				case Sections.Notes:
					_builder.SetNotes(content);
					break;
				case Sections.Quirk:
					_builder.AddQuirk(content);
					break;
				case Sections.Role:
					_builder.SetRole(content);
					break;
				case Sections.Source:
					_builder.SetSource(content);
					break;
				case Sections.WalkMp:
					_builder.SetWalkMp(content);
					break;
				case Sections.Weapons:
					_builder.SetWeaponListCount(content);
					_mode = Mode.Weapons;
					break;
				case Sections.EquipmentLocation.Head:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.Head);
					break;
				default:
					MtfThrowHelper.ThrowUnknownSectionTagException(section);
					break;
			}
		}

		void ProcessMediumLengthTags(ReadOnlySpan<char> section, ReadOnlySpan<char> content)
		{
			switch (section)
			{
				case Sections.ArmourLocation.CentreLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.CentreLeg);
					break;
				case Sections.ArmourLocation.CentreTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.CentreTorso);
					break;
				case Sections.ArmourLocation.FrontLeftLeg:
				case Sections.ArmourLocation.LeftLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftLeg);
					break;
				case Sections.ArmourLocation.FrontRightLeg:
				case Sections.ArmourLocation.RightLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightLeg);
					break;
				case Sections.ArmourLocation.Head:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.Head);
					break;
				case Sections.ArmourLocation.LeftArm:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftArm);
					break;
				case Sections.ArmourLocation.LeftTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.LeftTorso);
					break;
				case Sections.ArmourLocation.RearLeftLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearLeftLeg);
					break;
				case Sections.ArmourLocation.RearRightLeg:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearRightLeg);
					break;
				case Sections.ArmourLocation.RightArm:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightArm);
					break;
				case Sections.ArmourLocation.RightTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RightTorso);
					break;
				case Sections.ArmourLocation.RearCentreTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearCentreTorso);
					break;
				case Sections.ArmourLocation.RearLeftTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearLeftTorso);
					break;
				case Sections.ArmourLocation.RearRightTorso:
					_builder.SetArmourAtLocation(content, BattleMechArmourLocation.RearRightTorso);
					break;
				case Sections.ClanName:
					_builder.SetClanName(content);
					break;
				case Sections.Ejection:
					_builder.SetEjection(content);
					break;
				case Sections.Generator:
					_builder.SetGenerator(content);
					break;
				case Sections.ImageFile:
					_builder.SetImageFile(content);
					break;
				case Sections.Overview:
					_builder.SetOverview(content);
					break;
				case Sections.Structure:
					_builder.SetStructure(content);
					break;
				case Sections.TechBase:
					_builder.SetTechBase(content);
					break;
				case Sections.EquipmentLocation.LeftLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.LeftLeg);
					break;
				case Sections.EquipmentLocation.RightLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RightLeg);
					break;
				case Sections.EquipmentLocation.LeftArm:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.LeftArm);
					break;
				case Sections.EquipmentLocation.RightArm:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RightArm);
					break;
				default:
					MtfThrowHelper.ThrowUnknownSectionTagException(section);
					break;
			}
		}

		void ProcessLongLengthTags(ReadOnlySpan<char> section, ReadOnlySpan<char> content)
		{
			switch (section)
			{
				case Sections.BaseChassisHeatSinks:
					_builder.SetBaseChassisHeatSinks(content);
					break;
				case Sections.Capabilities:
					_builder.SetCapabilities(content);
					break;
				case Sections.Deployment:
					_builder.SetDeployment(content);
					break;
				case Sections.HeatSinkKit:
					_builder.SetHeatSinkKit(content);
					break;
				case Sections.HeatSinks:
					_builder.SetHeatSinks(content);
					break;
				case Sections.Manufacturer:
					_builder.SetManufacturer(content);
					break;
				case Sections.PrimaryFactory:
					_builder.SetPrimaryFactory(content);
					break;
				case Sections.RulesLevel:
					_builder.SetRulesLevel(content);
					break;
				case Sections.SystemManufacturer:
					_builder.SetSystemManufacturer(content);
					break;
				case Sections.SystemModel:
				case Sections.SystemModelTypo:
					_builder.SetSystemModel(content);
					break;
				case Sections.WeaponQuirk:
					_builder.AddWeaponQuirk(content);
					break;
				case Sections.EquipmentLocation.CentreLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.CentreLeg);
					break;
				case Sections.EquipmentLocation.CentreTorso:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.CentreTorso);
					break;
				case Sections.EquipmentLocation.FrontLeftLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.LeftLeg);
					break;
				case Sections.EquipmentLocation.FrontRightLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RightLeg);
					break;
				case Sections.EquipmentLocation.LeftTorso:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.LeftTorso);
					break;
				case Sections.EquipmentLocation.RearLeftLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RearLeftLeg);
					break;
				case Sections.EquipmentLocation.RearRightLeg:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RearRightLeg);
					break;
				case Sections.EquipmentLocation.RightTorso:
					SetEquipmentAtLocationMode(BattleMechEquipmentLocation.RightTorso);
					break;
				default:
					MtfThrowHelper.ThrowUnknownSectionTagException(section);
					break;
			}
		}

		[MemberNotNull(nameof(_equipmentLocation))]
		void SetEquipmentAtLocationMode(BattleMechEquipmentLocation equipmentLocation)
		{
			_mode = Mode.EquipmentAtLocation;
			_equipmentLocation = equipmentLocation;
		}
	}

	#region IDisposable

	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}

		if (_scratchBuffer is not null)
		{
			ArrayPool<char>.Shared.Return(_scratchBuffer, clearArray: false);

			// _longScratchBuffer may only be allocated via the ParseAsync entry point, which always
			// allocates _scratchBuffer.
			if (_longScratchBuffer is not null)
			{
				ArrayPool<char>.Shared.Return(_longScratchBuffer, clearArray: false);
			}
		}

		_disposed = true;
		GC.SuppressFinalize(this);
	}

	#endregion IDisposable
}
