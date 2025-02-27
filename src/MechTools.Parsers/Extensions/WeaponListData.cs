using MechTools.Core;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Extensions;

[StructLayout(LayoutKind.Auto)]
public readonly struct WeaponListData
{
	public readonly int? Ammo { get; init; }
	public readonly int? Count { get; init; }
	public readonly BattleMechEquipmentLocation Location { get; init; }
	public readonly string Name { get; init; }
	public readonly bool IsRear { get; init; }

	public WeaponListData(int? ammo, int? count, BattleMechEquipmentLocation location, string name, bool isRear)
	{
		Ammo = ammo;
		Count = count;
		Location = location;
		Name = name;
		IsRear = isRear;
	}

	public readonly void Deconstruct(
		out int? ammo,
		out int? count,
		out BattleMechEquipmentLocation location,
		out string name,
		out bool isRear)
	{
		ammo = Ammo;
		count = Count;
		location = Location;
		name = Name;
		isRear = IsRear;
	}
}
