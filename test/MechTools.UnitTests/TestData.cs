using MechTools.Parsers.Extensions;

namespace MechTools.UnitTests;

internal static class TestData
{
	public static TheoryData<string> EmptyAndWhiteSpaceStrings()
	{
		return new(
			"",
			"   ");
	}

	public static TheoryData<string> InvalidWeaponQuirks()
	{
		return new(
			// Leading delimeter
			":jettison_capable:RA:4:CLHAG30",
			// Trailing delimeter
			"jettison_capable:RA:4:CLHAG30:",
			// Empty section
			"jettison_capable:RA::4CLHAG30",
			// Not enough delimeters
			"jettison_capable:RA:4",
			// Invalid location
			"jettison_capable:QQ:4:CLHAG30",
			// Invalid slot
			"jettison_capable:RA:-1:CLHAG30");
	}

	public static TheoryData<string, WeaponQuirkData> ValidWeaponQuirks()
	{
		return new()
		{
			{ " jettison_capable :RA : 4 : CLHAG30 ", new(Core.BattleMechEquipmentLocation.RightArm, "jettison_capable", 4, "CLHAG30") },
		};
	}
}
