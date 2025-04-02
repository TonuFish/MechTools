using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct ArmourData : IEquatable<ArmourData>
{
	public readonly required Armour Armour { get; init; }
	public readonly required Origin? Origin { get; init; }

	[SetsRequiredMembers]
	public ArmourData(Armour armour, Origin? origin)
	{
		Armour = armour;
		Origin = origin;
	}

	public readonly void Deconstruct(out Armour armour, out Origin? origin)
	{
		armour = Armour;
		origin = Origin;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Armour}```{Origin}";
	}

#endif
	#region Equality

	public static bool operator ==(ArmourData left, ArmourData right) => left.Equals(right);

	public static bool operator !=(ArmourData left, ArmourData right) => !(left == right);

	public readonly bool Equals(ArmourData other)
	{
		return Armour == other.Armour && Origin == other.Origin;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is ArmourData && Equals((ArmourData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Armour, Origin);
	}

	#endregion Equality
}
