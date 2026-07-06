using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct LocationStructureData : IEquatable<LocationStructureData>
{
	public readonly required int Value { get; init; }
	public readonly required StructureData? StructureData { get; init; }

	[SetsRequiredMembers]
	public LocationStructureData(int value, StructureData? structureData)
	{
		Value = value;
		StructureData = structureData;
	}

	public readonly void Deconstruct(out int value, out StructureData? structureData)
	{
		value = Value;
		structureData = StructureData;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Value}```{StructureData}";
	}

#endif
	#region Equality

	public static bool operator ==(LocationStructureData left, LocationStructureData right) => left.Equals(right);

	public static bool operator !=(LocationStructureData left, LocationStructureData right) => !(left == right);

	public readonly bool Equals(LocationStructureData other)
	{
		return Value == other.Value && StructureData == other.StructureData;
	}

	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is LocationStructureData locationStructureData && Equals(locationStructureData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Value, StructureData);
	}

	#endregion Equality
}
