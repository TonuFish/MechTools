using MechTools.Core.Enums;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct WeaponListData
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
}
