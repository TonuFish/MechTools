using MechTools.Parsers.Enums;
using MechTools.Parsers.Helpers;

namespace MechTools.UnitTests;

internal static class TestData
{
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

	public static TheoryData<string> NotSimplePositiveNumberStrings()
	{
		return new(
			" -1 ",
			" 5.0 ",
			" 2e10 ");
	}

	#region Armour

	public static TheoryData<string> InvalidArmour()
	{
		return new(
			// Non-enum value
			" OtherValue ",
			// Unhandled edge case - Unbracketed trailing origin
			" Reflective IS (Inner Sphere)");
	}

	public static TheoryData<string, ArmourData> ValidArmour()
	{
		return new()
		{
			// Basic case
			{ " Standard ", new(Armour.Standard, Origin.Unknown) },
			// Concatenated case
			{ " Heavy Ferro-Fibrous(Inner Sphere) ", new(Armour.HeavyFerroFibrous, Origin.InnerSphere) },
			// Armour case
			{ " Standard Armor (Clan) ", new(Armour.Standard, Origin.Clan) },
			// Edge case - Malformed IS reflective
			{ " IS Reflective(Inner Sphere) ", new(Armour.Reflective, Origin.InnerSphere) },
			// Hypothetical edge case - Malformed Clan FL
			{ " Clan Ferro-Lamellor(Clan) ", new(Armour.FerroLamellor, Origin.Clan) },
			// Legacy abbreviated case
			{ " Ballistic-Reinforced (IS) ", new(Armour.BallisticReinforced, Origin.InnerSphere) },
			// Edge case - Prototype FF
			{ " Ferro-Fibrous Prototype(Inner Sphere) ", new(Armour.PrototypeFerroFibrous, Origin.InnerSphere) },
			// Edge case - (Unknown Technology Base)
			{ " Standard((Unknown Technology Base)) ", new(Armour.Standard, Origin.Unknown) },
			// Edge case - Malformed bracketed origin
			{ " Hardened (IS ", new(Armour.Hardened, Origin.Unknown) },
		};
	}

	#endregion Armour

	#region Armour at Location

	public static TheoryData<string> InvalidArmourAtLocation()
	{
		return new(
			" OtherValue ",
			" -20 ",
			" 34:Reactive(Inner Sphere) ");
	}

	public static TheoryData<string, LocationArmourData> ValidArmourAtLocation()
	{
		return new()
		{
			{ " 20 ", new(20, null, null) },
			{ " 90 ", new(90, null, null) },
			{ " Standard(IS/Clan):26 ", new(26, Armour.Standard, Origin.Unknown) },
			{ " Reactive(Inner Sphere):34 ", new(34, Armour.Reactive, Origin.InnerSphere) },
		};
	}

	#endregion Armour at Location

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

	#region Engine

	public static TheoryData<string> InvalidEngine()
	{
		return new(
			// No size
			" Fusion Engine ",
			// No engine text
			" 250 Fusion ",
			// No engine type
			" 410 Large Engine ");
	}

	public static TheoryData<string, EngineData> ValidEngine()
	{
		return new()
		{
			{ " 250 Fusion Engine ", new(Engine.Fusion, false, false, 250) },
			{ " 350 XXL Engine(IS) ", new(Engine.Xxl, false, true, 350) },
			{ " 420 Large XXL (Clan) Engine ", new(Engine.Xxl, true, false, 420) },
			{ " 400 XXL (Clan) Engine(IS) ", new(Engine.Xxl, true, true, 400) },
		};
	}

	#endregion Engine

	#region Equipment at Location

	public static TheoryData<string> InvalidEquipmentAtLocation()
	{
		return new("(T)");
	}

	public static TheoryData<string, EquipmentData> ValidEquipmentAtLocation()
	{
		return new()
		{
			{ " medium laser ", new(false, false, false, "medium laser") },
			// Values in CommonEquipmentValues cache should be returned with standardised casing
			{ " -EMPTY- ", new(false, false, false, "-Empty-") },
			{ " upper arm actuator ", new(false, false, false, "Upper Arm Actuator") },
			// Modifiers
			{ " Clan Machine Gun Ammo - Full (omnipod) ", new(true, false, false, "Clan Machine Gun Ammo - Full") },
			{ " Heavy PPC (T) (omnipod) ", new(true, false, true, "Heavy PPC") },
			{ " CLERMediumLaser (R) (omnipod) ", new(true, true, false, "CLERMediumLaser") },
			// Values after modifers are ignored
			{ " ISPPC (T) someText", new(false, false, true, "ISPPC") },
		};
	}

	#endregion Equipment at Location

	#region Heat Sinks

	public static TheoryData<string> InvalidHeatSinks()
	{
		return new(
			// No count
			" Double ",
			// No heat sink type
			" 25 "
			);
	}

	public static TheoryData<string, HeatSinkData> ValidHeatSinks()
	{
		return new()
		{
			{ " 16 Double ", new(16, HeatSink.Double, Origin.Unknown) },
			{ " 10 Clan Double ", new(10, HeatSink.Double, Origin.Clan) },
			{ " 8 Single (Inner Sphere) ", new(8, HeatSink.Single, Origin.InnerSphere) },
		};
	}

	#endregion Heat Sinks

	#region Myomer

	public static TheoryData<string, Myomer> KnownLegacyMyomerStrings()
	{
		// Legacy values found in mtf files
		return new()
		{
			{ "CLMASC", Myomer.Standard },
			{ "ISMASC", Myomer.Standard },
			{ "MASC", Myomer.Standard },
		};
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
			// No delimiter
			" chassis Hollis Mark 1A ",
			// Leading delimiter
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

	#region Weapon For Weapon List

	public static TheoryData<string> InvalidWeaponForWeaponList()
	{
		return new(
			// No name
			", Right Arm ",
			// No location
			" ER Large Laser, ",
			" 1 ER Large Laser, ",
			// No ammo
			" ISAC10, RightTorso, Ammo: ",
			// Non-positive quantity
			" 0 Small Laser, Head ",
			" -1 Small Laser, Head ",
			//
			" ISAC10, RightTorso, ",
			"1 ISAC10, RightTorso, ");
	}

	public static TheoryData<string, WeaponListData> ValidWeaponForWeaponList()
	{
		return new()
		{
			{ " ER Large Laser, Left Arm ", new(null, null, false, BattleMechEquipmentLocation.LeftArm, "ER Large Laser") },
			{ " 1 ISC3SlaveUnit, Center Torso ", new(null, 1, false, BattleMechEquipmentLocation.CentreTorso, "ISC3SlaveUnit") },
			{ " 2 ISMediumPulseLaser, Center Torso (R) ", new(null, 2, true, BattleMechEquipmentLocation.CentreTorso, "ISMediumPulseLaser") },
			{ " 1 ISAC10, Right Arm, Ammo:40 ", new(40, 1, false, BattleMechEquipmentLocation.RightArm, "ISAC10") },
			{ " 2 ISStreakSRM2, Left Torso (R), Ammo:50 ", new(50, 2, true, BattleMechEquipmentLocation.LeftTorso, "ISStreakSRM2") },
			{ " 1 ISERSmallLaser (R), Right Torso (R) ", new(null, 1, true, BattleMechEquipmentLocation.RightTorso, "ISERSmallLaser") },
		};
	}

	#endregion Weapon For Weapon List

	#region Weapon Quirks

	public static TheoryData<string> InvalidWeaponQuirks()
	{
		return new(
			// Leading delimiter
			":jettison_capable:RA:4:CLHAG30",
			// Trailing delimiter
			"jettison_capable:RA:4:CLHAG30:",
			// Empty section
			"jettison_capable:RA::4CLHAG30",
			// Not enough delimiters
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
