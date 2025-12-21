using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct NoCritData : IEquatable<NoCritData>
{
	public readonly required BattleMechEquipmentLocation Location { get; init; }
	public readonly required string Name { get; init; }

	[SetsRequiredMembers]
	public NoCritData(BattleMechEquipmentLocation location, string name)
	{
		Location = location;
		Name = name;
	}

	public readonly void Deconstruct(out BattleMechEquipmentLocation location, out string name)
	{
		location = Location;
		name = Name;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Location}```{Name}";
	}

#endif
	#region Equality

	public static bool operator ==(NoCritData left, NoCritData right) => left.Equals(right);

	public static bool operator !=(NoCritData left, NoCritData right) => !(left == right);

	public readonly bool Equals(NoCritData other)
	{
		return Location == other.Location && Name.Equals(other.Name, StringComparison.Ordinal);
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is NoCritData noCritData && Equals(noCritData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Location, Name);
	}

	#endregion Equality
}
