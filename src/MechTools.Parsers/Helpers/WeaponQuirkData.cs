using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

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

	#region Equality

	public static bool operator ==(WeaponQuirkData left, WeaponQuirkData right) => left.Equals(right);

	public static bool operator !=(WeaponQuirkData left, WeaponQuirkData right) => !(left == right);

	public bool Equals(WeaponQuirkData other)
	{
		return Location == other.Location
			&& Name.Equals(other.Name, StringComparison.Ordinal)
			&& Slot == other.Slot
			&& Weapon.Equals(other.Weapon, StringComparison.Ordinal);
	}

	public override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is WeaponQuirkData && Equals((WeaponQuirkData)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Location, Name, Slot, Weapon);
	}

	#endregion Equality
}
