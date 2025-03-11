using MechTools.Core.Enums;
using MechTools.Parsers.Helpers;

namespace MechTools.UnitTests;

internal static class TestData
{
	// TODO: Naming of these.

	public static TheoryData<string?, string> AllowAnyTextStrings()
	{
		return new()
		{
			{ null, "" },
			{ "   ", "   " },
			{ " This is a placeholder. ", " This is a placeholder. " },
		};
	}

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

	#region Armour

	public static TheoryData<string> InvalidArmour()
	{
		return new(
			// Non-enum value
			" OtherValue ",
			// Unclosed brackets
			" Hardened (IS ",
			// Unhandled edge case - Trailing origin
			" Reflective IS (Inner Sphere)");
	}

	public static TheoryData<string, ArmourData> ValidArmour()
	{
		return new()
		{
			// Basic case
			{ " Standard ", new(Armour.Standard, null) },
			// Abbreviated case
			{ " Ballistic-Reinforced (IS) ", new(Armour.BallisticReinforced, Origin.InnerSphere) },
			// Concatenated case
			{ " Heavy Ferro-Fibrous(Inner Sphere) ", new(Armour.HeavyFerroFibrous, Origin.InnerSphere) },
			// Armour case
			{ " Standard Armor (Clan)", new(Armour.Standard, Origin.Clan) },
			// Edge case - Malformed IS reflective
			{ " IS Reflective(Inner Sphere) ", new(Armour.Reflective, Origin.InnerSphere) },
			// Hypothetical edge case - Malformed Clan FL
			{ " Clan Ferro-Lamellor(Clan) ", new(Armour.FerroLamellor, Origin.Clan) },
			// Edge case - Prototype FF
			{ " Ferro-Fibrous Prototype(Inner Sphere) ", new(Armour.PrototypeFerroFibrous, Origin.InnerSphere) },
			// Edge case - (Unknown Technology Base)
			{ " Standard((Unknown Technology Base)) ", new(Armour.Standard, Origin.Unknown) },
		};
	}

	#endregion Armour

	#region Configuration

	public static TheoryData<string, ConfigurationData> ValidConfiguration()
	{
		return new()
		{
			{ " QuadVee ", new(Configuration.QuadVee, false) },
			{ " Biped Omnimech ", new(Configuration.Biped, true) },
			{ " Biped Omnimek ", new(Configuration.Biped, true) },
		};
	}

	#endregion Configuration

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

	#region No Crit

	public static TheoryData<string> InvalidNoCrit()
	{
		return new(
			// LAM locations aren't supported
			"ISERLargeLaser:AFT",
			"ISERLargeLaser:NOS",
			"ISERLargeLaser:WNG",
			// Location-less no crit isn't supported
			"ISERLargeLaser");
	}

	public static TheoryData<string, NoCritData> ValidNoCrit()
	{
		return new()
		{
			{ " IS Hardened : None ", new(BattleMechEquipmentLocation.None, "IS Hardened") },
			{ " SmartRoboticControlSystem : None ", new(BattleMechEquipmentLocation.None, "SmartRoboticControlSystem") },
		};
	}

	#endregion No Crit

	#region Source

	public static TheoryData<string, SourceData> ValidSource()
	{
		return new()
		{
			{ " Rec Guide:ilClan #24 ", new("ilClan #24", "Rec Guide") },
			{ " Battle of Tukayyid ", new("Battle of Tukayyid", null) },
			{ " TRO : 3067 ", new("3067", "TRO") },
		};
	}

	#endregion Source

	#region Specific System

	// Covers SystemManufacturer and SystemModel as they're effectively identical.

	public static TheoryData<string> InvalidSpecificSystemData()
	{
		return new(
			// No delimeter
			" chassis Hollis Mark 1A ",
			// Leading delimeter
			" : chassis : Hollis Mark 1A ",
			// Invalid specific system
			" : crab : Hollis Mark 1A ",
			// No name
			" chassis :");
	}

	public static TheoryData<string, SpecificSystemData> ValidSpecificSystemData()
	{
		return new()
		{
			{ " chassis : Hollis Mark 1A ", new("Hollis Mark 1A", SpecificSystem.Chassis) },
		};
	}

	#endregion Specific System

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
