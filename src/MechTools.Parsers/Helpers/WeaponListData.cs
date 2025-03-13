using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct WeaponListData : IEquatable<WeaponListData>
{
	public readonly int? Ammo { get; init; }
	public readonly int? Count { get; init; }
	public readonly bool IsRear { get; init; }
	public readonly BattleMechEquipmentLocation Location { get; init; }
	public readonly string Name { get; init; }

	public WeaponListData(int? ammo, int? count, bool isRear, BattleMechEquipmentLocation location, string name)
	{
		Ammo = ammo;
		Count = count;
		IsRear = isRear;
		Location = location;
		Name = name;
	}

	public readonly void Deconstruct(
		out int? ammo,
		out int? count,
		out bool isRear,
		out BattleMechEquipmentLocation location,
		out string name)
	{
		ammo = Ammo;
		count = Count;
		isRear = IsRear;
		location = Location;
		name = Name;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Ammo}```{Count}```{IsRear}```{Location}```{Name}";
	}

#endif
	#region Equality

	public static bool operator ==(WeaponListData left, WeaponListData right) => left.Equals(right);

	public static bool operator !=(WeaponListData left, WeaponListData right) => !(left == right);

	public readonly bool Equals(WeaponListData other)
	{
		return Ammo == other.Ammo
			&& Count == other.Count
			&& IsRear == other.IsRear
			&& Location == other.Location
		    && Name.Equals(other.Name, StringComparison.Ordinal);
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is WeaponListData && Equals((WeaponListData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Ammo, Count, IsRear, Location, Name);
	}

	#endregion Equality
}
