using MechTools.Core.Enums;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct NoCritData
{
	public readonly BattleMechEquipmentLocation Location { get; init; }
	public readonly string Name { get; init; }

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
}
