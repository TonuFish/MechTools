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

	public static TheoryData<string, WeaponQuirkData> ValidWeaponQuirks()
	{
		return new()
		{
			{ "jettison_capable :RA :\t4 : CLHAG30   ", new(Core.BattleMechEquipmentLocation.RightArm, "jettison_capable", 4, "CLHAG30") },
		};
	}
}
