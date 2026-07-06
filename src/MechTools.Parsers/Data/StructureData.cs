using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Sequential)] // Used order.
public readonly struct StructureData : IEquatable<StructureData>
{
	public readonly required Structure Structure { get; init; }
	public readonly required Origin? Origin { get; init; }

	[SetsRequiredMembers]
	public StructureData(Structure structure, Origin? origin)
	{
		Structure = structure;
		Origin = origin;
	}

	public readonly void Deconstruct(out Structure structure, out Origin? origin)
	{
		structure = Structure;
		origin = Origin;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Structure}```{Origin}";
	}

#endif
	#region Equality

	public static bool operator ==(StructureData left, StructureData right) => left.Equals(right);

	public static bool operator !=(StructureData left, StructureData right) => !(left == right);

	public readonly bool Equals(StructureData other)
	{
		return Structure == other.Structure && Origin == other.Origin;
	}

	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is StructureData structureData && Equals(structureData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Structure, Origin);
	}

	#endregion Equality
}
