using MechTools.Core.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct WeaponQuirkData
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
}
