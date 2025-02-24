using MechTools.Core;
using MechTools.Parsers.BattleMech;
using System;

namespace MechTools.Parsers.Extensions;

// TODO: Extension methods -> Normal static methods
internal static class EnumExtensions
{
	public static BattleMechEquipmentLocation FromAbbreviationToEquipmentLocation(this ReadOnlySpan<char> chars)
	{
		// TODO: Cleanup later, yick.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			// TODO: Check if there's an entry for none.
			"CL" => BattleMechEquipmentLocation.CentreLeg,
			"CT" => BattleMechEquipmentLocation.CentreTorso,
			"FLL" => BattleMechEquipmentLocation.LeftLeg,
			"FRL" => BattleMechEquipmentLocation.RightLeg,
			"HD" => BattleMechEquipmentLocation.Head,
			"LA" => BattleMechEquipmentLocation.LeftArm,
			"LL" => BattleMechEquipmentLocation.LeftLeg,
			"LT" => BattleMechEquipmentLocation.LeftTorso,
			"RLL" => BattleMechEquipmentLocation.RearLeftLeg,
			"RRL" => BattleMechEquipmentLocation.RearRightLeg,
			"RA" => BattleMechEquipmentLocation.RightArm,
			"RL" => BattleMechEquipmentLocation.RightLeg,
			"RT" => BattleMechEquipmentLocation.RightTorso,
			_ => ThrowHelper.ExceptionToSpecifyLater<BattleMechEquipmentLocation>(),
		};
	}

	public static BattleMechArmourLocation ToArmourLocation(this ReadOnlySpan<char> chars)
	{
		// TODO: May not be necessarily at all.

		return chars switch
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

	public static BattleMechEquipmentLocation ToEquipmentLocation(this ReadOnlySpan<char> chars)
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

	public static Cockpit ToCockpit(this ReadOnlySpan<char> chars)
	{
		// TODO: String cleanup.
		Span<char> upper = (stackalloc char[64])[..chars.Length];
		_ = chars.ToUpperInvariant(upper);

		return upper switch
		{
			"STANDARD COCKPIT" => Cockpit.StandardCockpit,
			"SMALL COCKPIT" => Cockpit.SmallCockpit,
			"COMMAND CONSOLE" => Cockpit.CommandConsole,
			"TORSO-MOUNTED COCKPIT" => Cockpit.TorsoMountedCockpit,
			"DUAL COCKPIT" => Cockpit.DualCockpit,
			"INDUSTRIAL COCKPIT" => Cockpit.IndustrialCockpit,
			"PRIMITIVE COCKPIT" => Cockpit.PrimitiveCockpit,
			"PRIMITIVE INDUSTRIAL COCKPIT" => Cockpit.PrimitiveIndustrialCockpit,
			"SUPERHEAVY COCKPIT" => Cockpit.SuperheavyCockpit,
			"SUPERHEAVY TRIPOD COCKPIT" => Cockpit.SuperheavyTripodCockpit,
			"TRIPOD COCKPIT" => Cockpit.TripodCockpit,
			"INTERFACE COCKPIT" => Cockpit.InterfaceCockpit,
			"VIRTUAL REALITY PILOTING POD" => Cockpit.VirtualRealityPilotingPod,
			"QUADVEE COCKPIT" => Cockpit.QuadVeeCockpit,
			"SUPERHEAVY INDUSTRIAL COCKPIT" => Cockpit.SuperheavyIndustrialCockpit,
			"SUPERHEAVY COMMAND CONSOLE" => Cockpit.SuperheavyCommandConsole,
			"SMALL COMMAND CONSOLE" => Cockpit.SmallCommandConsole,
			"TRIPOD INDUSTRIAL COCKPIT" => Cockpit.TripodIndustrialCockpit,
			"SUPERHEAVY TRIPOD INDUSTRIAL COCKPIT" => Cockpit.SuperheavyTripodIndustrialCockpit,
			_ => ToShortCockpit(upper),
		};
	}

	public static Configuration ToConfiguration(this ReadOnlySpan<char> chars)
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

	public static RulesLevel ToRulesLevel(this int num)
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

	public static Role ToRole(this ReadOnlySpan<char> chars)
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

	private static Cockpit ToShortCockpit(this ReadOnlySpan<char> upper)
	{
		// already uppercase.
		return upper switch
		{
			"STANDARD" => Cockpit.StandardCockpit,
			"SMALL" => Cockpit.SmallCockpit,
			"TORSO MOUNTED" => Cockpit.TorsoMountedCockpit,
			"DUAL" => Cockpit.DualCockpit,
			"INDUSTRIAL" => Cockpit.IndustrialCockpit,
			"PRIMITIVE" => Cockpit.PrimitiveCockpit,
			"PRIMITIVE INDUSTRIAL" => Cockpit.PrimitiveIndustrialCockpit,
			"SUPERHEAVY" => Cockpit.SuperheavyCockpit,
			"SUPERHEAVY TRIPOD" => Cockpit.SuperheavyTripodCockpit,
			"TRIPOD" => Cockpit.TripodCockpit,
			"INTERFACE" => Cockpit.InterfaceCockpit,
			"VRPP" => Cockpit.VirtualRealityPilotingPod,
			"QUADVEE" => Cockpit.QuadVeeCockpit,
			"SUPERHEAVY INDUSTRIAL" => Cockpit.SuperheavyIndustrialCockpit,
			"SUPERHEAVY COMMAND" => Cockpit.SuperheavyCommandConsole,
			"SMALL COMMAND" => Cockpit.SmallCommandConsole,
			"TRIPOD INDUSTRIAL" => Cockpit.TripodIndustrialCockpit,
			"SUPERHEAVY TRIPOD INDUSTRIAL" => Cockpit.SuperheavyTripodIndustrialCockpit,
			_ => ThrowHelper.ExceptionToSpecifyLater<Cockpit>(),
		};
	}
}
