using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Sequential)] // 0-offset Value as ArmourData is rarely used.
public readonly struct LocationArmourData : IEquatable<LocationArmourData>
{
	public readonly required int Value { get; init; }
	public readonly required ArmourData? ArmourData { get; init; }

	[SetsRequiredMembers]
	public LocationArmourData(int value, ArmourData? armourData)
	{
		Value = value;
		ArmourData = armourData;
	}

	public readonly void Deconstruct(out int value, out ArmourData? armourData)
	{
		value = Value;
		armourData = ArmourData;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Value}```{ArmourData}";
	}

#endif
	#region Equality

	public static bool operator ==(LocationArmourData left, LocationArmourData right) => left.Equals(right);

	public static bool operator !=(LocationArmourData left, LocationArmourData right) => !(left == right);

	public readonly bool Equals(LocationArmourData other)
	{
		return Value == other.Value && ArmourData == other.ArmourData;
	}

	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is LocationArmourData locationArmourData && Equals(locationArmourData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Value, ArmourData);
	}

	#endregion Equality
}
