using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct WeaponQuirkData : IEquatable<WeaponQuirkData>
{
	public readonly required BattleMechEquipmentLocation Location { get; init; }
	public readonly required string Name { get; init; }
	public readonly required int Slot { get; init; }
	public readonly required string Weapon { get; init; }

	[SetsRequiredMembers]
	public WeaponQuirkData(BattleMechEquipmentLocation location, string name, int slot, string weapon)
	{
		Location = location;
		Name = name;
		Slot = slot;
		Weapon = weapon;
	}

	public readonly void Deconstruct(
		out BattleMechEquipmentLocation location,
		out string name,
		out int slot,
		out string weapon)
	{
		location = Location;
		name = Name;
		slot = Slot;
		weapon = Weapon;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Location}```{Name}```{Slot}```{Weapon}";
	}

#endif
	#region Equality

	public static bool operator ==(WeaponQuirkData left, WeaponQuirkData right) => left.Equals(right);

	public static bool operator !=(WeaponQuirkData left, WeaponQuirkData right) => !(left == right);

	public readonly bool Equals(WeaponQuirkData other)
	{
		return Location == other.Location
			&& Name.Equals(other.Name, StringComparison.Ordinal)
			&& Slot == other.Slot
			&& Weapon.Equals(other.Weapon, StringComparison.Ordinal);
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is WeaponQuirkData && Equals((WeaponQuirkData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Location, Name, Slot, Weapon);
	}

	#endregion Equality
}
