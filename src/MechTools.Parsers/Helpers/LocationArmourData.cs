using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Sequential)] // 0-offset Value as Armour/Origin are rarely used.
public readonly struct LocationArmourData : IEquatable<LocationArmourData>
{
	public readonly required int Value { get; init; }
	public readonly required Armour? Armour { get; init; }
	public readonly required Origin? Origin { get; init; }

	[SetsRequiredMembers]
	public LocationArmourData(int value, Armour? armour, Origin? origin)
	{
		Value = value;
		Armour = armour;
		Origin = origin;
	}

	public readonly void Deconstruct(
		out int value,
		[NotNullIfNotNull(nameof(origin))] out Armour? armour,
		[NotNullIfNotNull(nameof(armour))] out Origin? origin)
	{
		value = Value;
		armour = Armour;
		origin = Origin;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Value}```{Armour}```{Origin}";
	}

#endif
	#region Equality

	public static bool operator ==(LocationArmourData left, LocationArmourData right) => left.Equals(right);

	public static bool operator !=(LocationArmourData left, LocationArmourData right) => !(left == right);

	public readonly bool Equals(LocationArmourData other)
	{
		return Value == other.Value && Armour == other.Armour && Origin == other.Origin;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is LocationArmourData && Equals((LocationArmourData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Value, Armour, Origin);
	}

	#endregion Equality
}
