using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct EngineData : IEquatable<EngineData>
{
	public readonly required Engine Engine { get; init; }
	public readonly required bool HasClanFlag { get; init; }
	public readonly required bool HasInnerSphereFlag { get; init; }
	public readonly required int Size { get; init; }

	[SetsRequiredMembers]
	public EngineData(Engine engine, bool hasClanFlag, bool hasInnerSphereFlag, int size)
	{
		Engine = engine;
		HasClanFlag = hasClanFlag;
		HasInnerSphereFlag = hasInnerSphereFlag;
		Size = size;
	}

	public readonly void Deconstruct(out Engine engine, out bool hasClanFlag, out bool hasInnerSphereFlag, out int size)
	{
		engine = Engine;
		hasClanFlag = HasClanFlag;
		hasInnerSphereFlag = HasInnerSphereFlag;
		size = Size;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Engine}```{HasClanFlag}```{HasInnerSphereFlag}```{Size}";
	}

#endif
	#region Equality

	public static bool operator ==(EngineData left, EngineData right) => left.Equals(right);

	public static bool operator !=(EngineData left, EngineData right) => !(left == right);

	public readonly bool Equals(EngineData other)
	{
		return Engine == other.Engine
			&& HasClanFlag == other.HasClanFlag
			&& HasInnerSphereFlag == other.HasInnerSphereFlag
			&& Size == other.Size;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is EngineData && Equals((EngineData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Engine, HasClanFlag, HasInnerSphereFlag, Size);
	}

	#endregion Equality
}
