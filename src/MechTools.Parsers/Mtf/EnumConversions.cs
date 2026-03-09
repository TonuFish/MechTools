using MechTools.Parsers.Enums;
using System;

namespace MechTools.Parsers.Mtf;

internal static class EnumConversions
{
	public static Armour GetArmour(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"ANTI-PENETRATIVE ABLATION" => Armour.AntiPenetrativeAblation,
			"BALLISTIC-REINFORCED" => Armour.BallisticReinforced,
			"COMMERCIAL" => Armour.Commercial,
			"FERRO-FIBROUS" => Armour.FerroFibrous,
			"FERRO-FIBROUS PROTOTYPE" => Armour.PrototypeFerroFibrous,
			"FERRO-LAMELLOR" => Armour.FerroLamellor,
			"HARDENED" => Armour.Hardened,
			"HEAT-DISSIPATING" => Armour.HeatDissipating,
			"HEAVY FERRO-FIBROUS" => Armour.HeavyFerroFibrous,
			"HEAVY INDUSTRIAL" => Armour.HeavyIndustrial,
			"IMPACT-RESISTANT" => Armour.ImpactResistant,
			"INDUSTRIAL" => Armour.Industrial,
			"LIGHT FERRO-FIBROUS" => Armour.LightFerroFibrous,
			"PATCHWORK" => Armour.Patchwork,
			"PRIMITIVE" => Armour.Primitive,
			"REACTIVE" => Armour.Reactive,
			"REFLECTIVE" => Armour.Reflective,
			"STANDARD" => Armour.Standard,
			"STEALTH" => Armour.Stealth,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Armour>(chars),
		};
	}

	public static Cockpit GetCockpit(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"COMMAND CONSOLE" => Cockpit.CommandConsole,
			"DUAL COCKPIT" => Cockpit.DualCockpit,
			"INDUSTRIAL COCKPIT" => Cockpit.IndustrialCockpit,
			"INTERFACE COCKPIT" => Cockpit.InterfaceCockpit,
			"PRIMITIVE COCKPIT" => Cockpit.PrimitiveCockpit,
			"PRIMITIVE INDUSTRIAL COCKPIT" => Cockpit.PrimitiveIndustrialCockpit,
			"QUADVEE COCKPIT" => Cockpit.QuadVeeCockpit,
			"SMALL COCKPIT" => Cockpit.SmallCockpit,
			"SMALL COMMAND CONSOLE" => Cockpit.SmallCommandConsole,
			"STANDARD COCKPIT" => Cockpit.StandardCockpit,
			"SUPERHEAVY COCKPIT" => Cockpit.SuperHeavyCockpit,
			"SUPERHEAVY COMMAND CONSOLE" => Cockpit.SuperHeavyCommandConsole,
			"SUPERHEAVY INDUSTRIAL COCKPIT" => Cockpit.SuperHeavyIndustrialCockpit,
			"SUPERHEAVY TRIPOD COCKPIT" => Cockpit.SuperHeavyTripodCockpit,
			"SUPERHEAVY TRIPOD INDUSTRIAL COCKPIT" => Cockpit.SuperHeavyTripodIndustrialCockpit,
			"TORSO-MOUNTED COCKPIT" => Cockpit.TorsoMountedCockpit,
			"TRIPOD COCKPIT" => Cockpit.TripodCockpit,
			"TRIPOD INDUSTRIAL COCKPIT" => Cockpit.TripodIndustrialCockpit,
			"VIRTUAL REALITY PILOTING POD" => Cockpit.VirtualRealityPilotingPod,
			_ => GetShortCockpit(upper),
		};
	}

	public static Configuration GetConfiguration(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"BIPED" => Configuration.Biped,
			"LAM" => Configuration.LAM,
			"QUAD" => Configuration.Quad,
			"QUADVEE" => Configuration.QuadVee,
			"TRIPOD" => Configuration.Tripod,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Configuration>(chars),
		};
	}

	public static Engine GetEngine(ReadOnlySpan<char> chars)
	{
		if (chars.Equals("(NONE)", StringComparison.OrdinalIgnoreCase))
		{
			return Engine.None;
		}

		const string largeEngineMarker = "LARGE ";
		const string primitiveEngineMarker = "PRIMITIVE ";
		const string fusionEngineMarker = " FUSION";

		var cleanChars = chars;
		if (cleanChars.StartsWith(largeEngineMarker, StringComparison.OrdinalIgnoreCase))
		{
			cleanChars = cleanChars[largeEngineMarker.Length..].TrimStart();
		}
		if (cleanChars.StartsWith(primitiveEngineMarker, StringComparison.OrdinalIgnoreCase))
		{
			cleanChars = cleanChars[primitiveEngineMarker.Length..].TrimStart();
		}

		if (cleanChars.Equals("FUSION", StringComparison.OrdinalIgnoreCase))
		{
			return Engine.Fusion;
		}
		else if (cleanChars.EndsWith(" FUSION", StringComparison.OrdinalIgnoreCase))
		{
			cleanChars = cleanChars[..^fusionEngineMarker.Length].TrimEnd();
		}

		var upper = (stackalloc char[64])[..cleanChars.Length];
		_ = cleanChars.ToUpperInvariant(upper);

		return upper switch
		{
			"BATTERY" => Engine.Battery,
			"I.C.E." or "ICE" => Engine.Combustion,
			"COMPACT" => Engine.Compact,
			"EXTERNAL" => Engine.External,
			"FISSION" => Engine.Fission,
			"FUEL CELL" or "FUEL-CELL" => Engine.FuelCell,
			// "FUSION" would imply a {} FUSION FUSION engine.
			"LIGHT" => Engine.Light,
			"MAGLEV" => Engine.Maglev,
			// "(NONE)" explicitly handled earlier.
			"SOLAR" => Engine.Solar,
			"STEAM" => Engine.Steam,
			"XL" => Engine.Xl,
			"XXL" => Engine.Xxl,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Engine>(chars),
		};
	}

	public static BattleMechEquipmentLocation GetEquipmentLocation(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			Sections.EquipmentLocation.CentreLeg => BattleMechEquipmentLocation.CentreLeg,
			Sections.EquipmentLocation.CentreTorso => BattleMechEquipmentLocation.CentreTorso,
			Sections.EquipmentLocation.FrontLeftLeg => BattleMechEquipmentLocation.LeftLeg,
			Sections.EquipmentLocation.FrontRightLeg => BattleMechEquipmentLocation.RightLeg,
			Sections.EquipmentLocation.Head => BattleMechEquipmentLocation.Head,
			Sections.EquipmentLocation.LeftArm => BattleMechEquipmentLocation.LeftArm,
			Sections.EquipmentLocation.LeftLeg => BattleMechEquipmentLocation.LeftLeg,
			Sections.EquipmentLocation.LeftTorso => BattleMechEquipmentLocation.LeftTorso,
			Sections.EquipmentLocation.None => BattleMechEquipmentLocation.None, // ATAE-70 special case
			Sections.EquipmentLocation.RearLeftLeg => BattleMechEquipmentLocation.RearLeftLeg,
			Sections.EquipmentLocation.RearRightLeg => BattleMechEquipmentLocation.RearRightLeg,
			Sections.EquipmentLocation.RightArm => BattleMechEquipmentLocation.RightArm,
			Sections.EquipmentLocation.RightLeg => BattleMechEquipmentLocation.RightLeg,
			Sections.EquipmentLocation.RightTorso => BattleMechEquipmentLocation.RightTorso,
			_ => MtfThrowHelper.ThrowUnknownEnumException<BattleMechEquipmentLocation>(chars),
		};
	}

	public static BattleMechEquipmentLocation GetEquipmentLocationFromAbbreviation(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"CL" => BattleMechEquipmentLocation.CentreLeg,
			"CT" => BattleMechEquipmentLocation.CentreTorso,
			"FLL" => BattleMechEquipmentLocation.LeftLeg,
			"FRL" => BattleMechEquipmentLocation.RightLeg,
			"HD" => BattleMechEquipmentLocation.Head,
			"LA" => BattleMechEquipmentLocation.LeftArm,
			"LL" => BattleMechEquipmentLocation.LeftLeg,
			"LT" => BattleMechEquipmentLocation.LeftTorso,
			"NONE" => BattleMechEquipmentLocation.None,
			"RLL" => BattleMechEquipmentLocation.RearLeftLeg,
			"RRL" => BattleMechEquipmentLocation.RearRightLeg,
			"RA" => BattleMechEquipmentLocation.RightArm,
			"RL" => BattleMechEquipmentLocation.RightLeg,
			"RT" => BattleMechEquipmentLocation.RightTorso,
			_ => MtfThrowHelper.ThrowUnknownEnumException<BattleMechEquipmentLocation>(chars),
		};
	}

	public static Gyro GetGyro(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		// TODO: There's exactly one entry of a gyro without " Gyro" suffix... Sigh.

		return upper switch
		{
			"COMPACT GYRO" => Gyro.Compact,
			"HEAVY DUTY GYRO" => Gyro.HeavyDuty,
			"NONE" => Gyro.None,
			"STANDARD GYRO" => Gyro.Standard,
			"SUPERHEAVY GYRO" => Gyro.SuperHeavyDuty,
			"XL" or "XL GYRO" => Gyro.XL,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Gyro>(chars),
		};
	}

	public static HeatSink GetHeatSinks(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"SINGLE" => HeatSink.Single,
			"COMPACT" => HeatSink.Compact,
			"DOUBLE" => HeatSink.Double,
			"LASER" => HeatSink.Laser,
			_ => MtfThrowHelper.ThrowUnknownEnumException<HeatSink>(chars),
		};
	}

	public static Lam GetLam(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"STANDARD" => Lam.Standard,
			"BIMODAL" => Lam.Bimodal,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Lam>(chars),
		};
	}

	public static Motive GetMotive(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"TRACK" => Motive.Track,
			"WHEEL" => Motive.Wheel,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Motive>(chars),
		};
	}

	public static Myomer GetMyomer(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			// Legacy MASC values
			"CLMASC" or "ISMASC" or "MASC" => Myomer.Standard,

			"INDUSTRIAL TRIPLE-STRENGTH" => Myomer.IndustrialTripleStrength,
			"PROTOTYPE TRIPLE-STRENGTH" => Myomer.PrototypeTripleStrength,
			"STANDARD" => Myomer.Standard,
			"SUPER-COOLED" => Myomer.SuperCooled,
			"TRIPLE STRENGTH MYOMER" or "TRIPLE-STRENGTH" => Myomer.TripleStrength,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Myomer>(chars),
		};
	}

	public static Role GetRole(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"NONE" => Role.None,
			"AMBUSHER" => Role.Ambusher,
			"BRAWLER" => Role.Brawler,
			"JUGGERNAUT" => Role.Juggernaut,
			"MISSILE BOAT" => Role.MissileBoat,
			"SCOUT" => Role.Scout,
			"SKIRMISHER" => Role.Skirmisher,
			"SNIPER" => Role.Sniper,
			"STRIKER" => Role.Striker,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Role>(chars),
		};
	}

	public static RulesLevel GetRulesLevel(int num)
	{
		return num switch
		{
			1 => RulesLevel.Introductory,
			2 => RulesLevel.Standard,
			3 => RulesLevel.Advanced,
			4 => RulesLevel.Experimental,
			5 => RulesLevel.Unofficial,
			_ => MtfThrowHelper.ThrowUnknownEnumException<RulesLevel>(num),
		};
	}

	public static SpecificSystem GetSpecificSystem(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"ARMOR" => SpecificSystem.Armour,
			"CHASSIS" => SpecificSystem.Chassis,
			"COMMUNICATIONS" => SpecificSystem.Communications,
			"ENGINE" => SpecificSystem.Engine,
			"JUMPJET" => SpecificSystem.JumpJet,
			"TARGETING" => SpecificSystem.Targeting,
			_ => MtfThrowHelper.ThrowUnknownEnumException<SpecificSystem>(chars),
		};
	}

	public static Structure GetStructure(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"STANDARD" => Structure.Standard,
			"COMPOSITE" => Structure.Composite,
			"ENDO-COMPOSITE" => Structure.EndoComposite,
			"ENDO STEEL" or "ENDO-STEEL" => Structure.EndoSteel,
			"ENDO STEEL PROTOTYPE" or "ENDO-STEEL PROTOTYPE" => Structure.EndoSteelPrototype,
			"INDUSTRIAL" => Structure.Industrial,
			"REINFORCED" => Structure.Reinforced,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Structure>(chars),
		};
	}

	public static TechBase GetTechBase(ReadOnlySpan<char> chars)
	{
		var upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"CLAN" => TechBase.Clan,
			"INNER SPHERE" => TechBase.InnerSphere,
			"MIXED (CLAN CHASSIS)" => TechBase.MixedClanChassis,
			"MIXED (IS CHASSIS)" => TechBase.MixedInnerSphereChassis,
			_ => MtfThrowHelper.ThrowUnknownEnumException<TechBase>(chars),
		};
	}

	private static Cockpit GetShortCockpit(this ReadOnlySpan<char> upper)
	{
		// already uppercase.
		return upper switch
		{
			"DUAL" => Cockpit.DualCockpit,
			"INDUSTRIAL" => Cockpit.IndustrialCockpit,
			"INTERFACE" => Cockpit.InterfaceCockpit,
			"PRIMITIVE INDUSTRIAL" => Cockpit.PrimitiveIndustrialCockpit,
			"PRIMITIVE" => Cockpit.PrimitiveCockpit,
			"QUADVEE" => Cockpit.QuadVeeCockpit,
			"SMALL COMMAND" => Cockpit.SmallCommandConsole,
			"SMALL" => Cockpit.SmallCockpit,
			"STANDARD" => Cockpit.StandardCockpit,
			"SUPERHEAVY COMMAND" => Cockpit.SuperHeavyCommandConsole,
			"SUPERHEAVY INDUSTRIAL" => Cockpit.SuperHeavyIndustrialCockpit,
			"SUPERHEAVY TRIPOD INDUSTRIAL" => Cockpit.SuperHeavyTripodIndustrialCockpit,
			"SUPERHEAVY TRIPOD" => Cockpit.SuperHeavyTripodCockpit,
			"SUPERHEAVY" => Cockpit.SuperHeavyCockpit,
			"TORSO MOUNTED" => Cockpit.TorsoMountedCockpit,
			"TRIPOD INDUSTRIAL" => Cockpit.TripodIndustrialCockpit,
			"TRIPOD" => Cockpit.TripodCockpit,
			"VRPP" => Cockpit.VirtualRealityPilotingPod,
			_ => MtfThrowHelper.ThrowUnknownEnumException<Cockpit>(upper),
		};
	}
}
