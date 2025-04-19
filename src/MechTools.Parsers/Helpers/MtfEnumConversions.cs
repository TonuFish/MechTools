using MechTools.Core.Enums;
using MechTools.Parsers.BattleMech;
using System;

namespace MechTools.Parsers.Helpers;

internal static class MtfEnumConversions
{
	// TODO: Naming.

	public static BattleMechArmourLocation GetArmourLocation(ReadOnlySpan<char> chars)
	{
		// TODO: May not be necessarily at all.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			MtfSections.ArmourLocation.CentreLeg => BattleMechArmourLocation.CentreLeg,
			MtfSections.ArmourLocation.CentreTorso => BattleMechArmourLocation.CentreTorso,
			MtfSections.ArmourLocation.FrontLeftLeg => BattleMechArmourLocation.LeftLeg,
			MtfSections.ArmourLocation.FrontRightLeg => BattleMechArmourLocation.RightLeg,
			MtfSections.ArmourLocation.Head => BattleMechArmourLocation.Head,
			MtfSections.ArmourLocation.LeftArm => BattleMechArmourLocation.LeftArm,
			MtfSections.ArmourLocation.LeftLeg => BattleMechArmourLocation.LeftLeg,
			MtfSections.ArmourLocation.LeftTorso => BattleMechArmourLocation.LeftTorso,
			MtfSections.ArmourLocation.RearCentreTorso => BattleMechArmourLocation.RearCentreTorso,
			MtfSections.ArmourLocation.RearLeftLeg => BattleMechArmourLocation.RearLeftLeg,
			MtfSections.ArmourLocation.RearLeftTorso => BattleMechArmourLocation.RearLeftTorso,
			MtfSections.ArmourLocation.RearRightLeg => BattleMechArmourLocation.RearRightLeg,
			MtfSections.ArmourLocation.RearRightTorso => BattleMechArmourLocation.RearRightTorso,
			MtfSections.ArmourLocation.RightArm => BattleMechArmourLocation.RightArm,
			MtfSections.ArmourLocation.RightLeg => BattleMechArmourLocation.RightLeg,
			MtfSections.ArmourLocation.RightTorso => BattleMechArmourLocation.RightTorso,
			_ => ThrowHelper.ExceptionToSpecifyLater<BattleMechArmourLocation>(),
		};
	}

	public static Cockpit GetCockpit(ReadOnlySpan<char> chars)
	{
		// TODO: String cleanup.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
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
		// TODO: String cleanup.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"BIPED" => Configuration.Biped,
			"LAM" => Configuration.LAM,
			"QUAD" => Configuration.Quad,
			"QUADVEE" => Configuration.QuadVee,
			"TRIPOD" => Configuration.Tripod,
			_ => ThrowHelper.ExceptionToSpecifyLater<Configuration>(),
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

		Span<char> upper = (stackalloc char[64])[..cleanChars.Length];
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
			_ => ThrowHelper.ExceptionToSpecifyLater<Engine>(),
		};
	}

	public static BattleMechEquipmentLocation GetEquipmentLocation(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			MtfSections.EquipmentLocation.CentreLeg => BattleMechEquipmentLocation.CentreLeg,
			MtfSections.EquipmentLocation.CentreTorso => BattleMechEquipmentLocation.CentreTorso,
			MtfSections.EquipmentLocation.FrontLeftLeg => BattleMechEquipmentLocation.LeftLeg,
			MtfSections.EquipmentLocation.FrontRightLeg => BattleMechEquipmentLocation.RightLeg,
			MtfSections.EquipmentLocation.Head => BattleMechEquipmentLocation.Head,
			MtfSections.EquipmentLocation.LeftArm => BattleMechEquipmentLocation.LeftArm,
			MtfSections.EquipmentLocation.LeftLeg => BattleMechEquipmentLocation.LeftLeg,
			MtfSections.EquipmentLocation.LeftTorso => BattleMechEquipmentLocation.LeftTorso,
			MtfSections.EquipmentLocation.None => BattleMechEquipmentLocation.None, // TODO: ATAE-70 - Thonk.
			MtfSections.EquipmentLocation.RearLeftLeg => BattleMechEquipmentLocation.RearLeftLeg,
			MtfSections.EquipmentLocation.RearRightLeg => BattleMechEquipmentLocation.RearRightLeg,
			MtfSections.EquipmentLocation.RightArm => BattleMechEquipmentLocation.RightArm,
			MtfSections.EquipmentLocation.RightLeg => BattleMechEquipmentLocation.RightLeg,
			MtfSections.EquipmentLocation.RightTorso => BattleMechEquipmentLocation.RightTorso,
			_ => ThrowHelper.ExceptionToSpecifyLater<BattleMechEquipmentLocation>(),
		};
	}

	public static BattleMechEquipmentLocation GetEquipmentLocationFromAbbreviation(ReadOnlySpan<char> chars)
	{
		// TODO: Cleanup later, yick.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
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
			_ => ThrowHelper.ExceptionToSpecifyLater<BattleMechEquipmentLocation>(),
		};
	}

	public static Gyro GetGyro(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
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
			_ => ThrowHelper.ExceptionToSpecifyLater<Gyro>(),
		};
	}

	public static Lam GetLam(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"STANDARD" => Lam.Standard,
			"BIMODAL" => Lam.Bimodal,
			_ => ThrowHelper.ExceptionToSpecifyLater<Lam>(),
		};
	}

	public static Motive GetMotive(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"TRACK" => Motive.Track,
			"WHEEL" => Motive.Wheel,
			_ => ThrowHelper.ExceptionToSpecifyLater<Motive>(),
		};
	}

	public static Myomer GetMyomer(ReadOnlySpan<char> chars)
	{
		// TODO: Decide how to handle known invalid values.
		//CLMASC
		//ISMASC
		//MASC

		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"INDUSTRIAL TRIPLE-STRENGTH" => Myomer.IndustrialTripleStrength,
			"PROTOTYPE TRIPLE-STRENGTH" => Myomer.PrototypeTripleStrength,
			"STANDARD" => Myomer.Standard,
			"SUPER-COOLED" => Myomer.SuperCooled,
			"TRIPLE STRENGTH MYOMER" or "TRIPLE-STRENGTH" => Myomer.TripleStrength,
			_ => ThrowHelper.ExceptionToSpecifyLater<Myomer>(),
		};
	}

	public static Role GetRole(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
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
			_ => ThrowHelper.ExceptionToSpecifyLater<Role>(),
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
			_ => ThrowHelper.ExceptionToSpecifyLater<RulesLevel>(),
		};
	}

	public static SpecificSystem GetSpecificSystem(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"ARMOR" => SpecificSystem.Armour,
			"CHASSIS" => SpecificSystem.Chassis,
			"COMMUNICATIONS" => SpecificSystem.Communications,
			"ENGINE" => SpecificSystem.Engine,
			"JUMPJET" => SpecificSystem.JumpJet,
			"TARGETING" => SpecificSystem.Targeting,
			_ => ThrowHelper.ExceptionToSpecifyLater<SpecificSystem>(),
		};
	}

	public static Structure GetStructure(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
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
			_ => ThrowHelper.ExceptionToSpecifyLater<Structure>(),
		};
	}

	public static TechBase GetTechBase(ReadOnlySpan<char> chars)
	{
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"CLAN" => TechBase.Clan,
			"INNER SPHERE" => TechBase.InnerSphere,
			"MIXED (CLAN CHASSIS)" => TechBase.MixedClanChassis,
			"MIXED (IS CHASSIS)" => TechBase.MixedInnerSphereChassis,
			_ => ThrowHelper.ExceptionToSpecifyLater<TechBase>(),
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
			_ => ThrowHelper.ExceptionToSpecifyLater<Cockpit>(),
		};
	}
}
