using MechTools.Core.Enums;
using MechTools.Parsers.Helpers;

namespace MechTools.UnitTests;

internal static class TestData
{
	public static TheoryData<string> EmptyAndWhiteSpaceStrings()
	{
		return new(
			"",
			"   ");
	}

	public static TheoryData<string> NotNonNegativeNumberStrings()
	{
		return new(
			" a ",
			" -1 ",
			" 5.0 ");
	}

	#region Armour Type

	public static TheoryData<string> InvalidArmourTypes()
	{
		return new(
			// Non-enum value
			" OtherValue ",
			// Unclosed brackets
			" Hardened (IS ",
			// Unhandled edge case - Trailing origin
			" Reflective IS (Inner Sphere)");
	}

	public static TheoryData<string, (ArmourType Armour, Origin? Origin)> ValidArmourTypes()
	{
		return new()
		{
			// Basic case
			{ " Standard ", (ArmourType.Standard, null) },
			// Abbreviated case
			{ " Ballistic-Reinforced (IS) ", (ArmourType.BallisticReinforced, Origin.InnerSphere) },
			// Concatenated case
			{ " Heavy Ferro-Fibrous(Inner Sphere) ", (ArmourType.HeavyFerroFibrous, Origin.InnerSphere) },
			// Armour case
			{ " Standard Armor (Clan)", (ArmourType.Standard, Origin.Clan) },
			// Edge case - Malformed IS reflective
			{ " IS Reflective(Inner Sphere) ", (ArmourType.Reflective, Origin.InnerSphere) },
			// Hypothetical edge case - Malformed Clan FL
			{ " Clan Ferro-Lamellor(Clan) ", (ArmourType.FerroLamellor, Origin.Clan) },
			// Edge case - Prototype FF
			{ " Ferro-Fibrous Prototype(Inner Sphere) ", (ArmourType.PrototypeFerroFibrous, Origin.InnerSphere) },
			// Edge case - (Unknown Technology Base)
			{ " Standard((Unknown Technology Base)) ", (ArmourType.Standard, Origin.Unknown) },
		};
	}

	#endregion Armour Type

	#region Myomer

	public static TheoryData<string> KnownLegacyMyomerStrings()
	{
		// Legacy values found in mtf files
		return new(
			"CLMASC",
			"ISMASC",
			"MASC");
	}

	#endregion Myomer

	#region Weapon Quirks

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
			{ " jettison_capable : RA : 4 : CLHAG30 ", new(BattleMechEquipmentLocation.RightArm, "jettison_capable", 4, "CLHAG30") },
		};
	}

	#endregion Weapon Quirks
}
