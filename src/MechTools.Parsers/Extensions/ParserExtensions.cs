using MechTools.Core;
using System;

namespace MechTools.Parsers.Extensions;

// TODO: Move, these aren't extensions anymore.
// TODO: Naming of all of these, for now they just match the caller.
public static class ParserExtensions
{
	public static string AddComment(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string AddEquipmentAtLocation(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string AddQuirk(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static (string Name, BattleMechEquipmentLocation Location, int Slot, string Weapon) AddWeaponQuirk(
		ReadOnlySpan<char> chars)
	{
		if (chars.Length < 10
			|| chars[0] == ':'
			|| chars[^1] == ':'
			|| chars.Count(':') != 3
			|| chars.Contains([':', ':'], StringComparison.Ordinal))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		var enumerator = chars.Split(':');
		_ = enumerator.MoveNext();
		var name = chars[enumerator.Current];
		_ = enumerator.MoveNext();
		var location = chars[enumerator.Current].FromAbbreviationToEquipmentLocation();
		_ = enumerator.MoveNext();
		// TODO: TryParse
		var slot = int.Parse(chars[enumerator.Current]);
		_ = enumerator.MoveNext();
		var weapon = chars[enumerator.Current];

		return (name.ToString(), location, slot, weapon.ToString());
	}

	public static (string Name, BattleMechEquipmentLocation Location) AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		// TODO: Handle unusual weapon formats

		if (chars.Length < 4)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}
		else if (char.IsNumber(chars[0]))
		{
			// NN Name, Location, Ammo:N+
			// Format: `1 ISLBXAC10, Right Torso, Ammo:20`
			ThrowHelper.ImExcited();
		}

		var delimeterIndex = chars.LastIndexOf([',', ' ']);
		if (delimeterIndex == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		var location = chars[(delimeterIndex + 2)..].ToEquipmentLocation();
		return (chars[..delimeterIndex].ToString(), location);
	}

	public static string SetArmourType(ReadOnlySpan<char> chars)
	{
		// TODO: Enum-ify.
		// Brackets = origin
		return chars.ToString();
	}

	public static int SetArmourAtLocation(ReadOnlySpan<char> chars)
	{
		// TODO: This will fail on weirder values - See ` armor:[^\d]`
		// IS Hardened(Inner Sphere):12

		if (!int.TryParse(chars, out var armour))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return armour;
	}

	public static int SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var heatSinks))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return heatSinks;
	}

	public static string SetCapabilities(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetChassis(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetClanName(ReadOnlySpan<char> chars)
	{
		// TODO: Enum?
		return chars.ToString();
	}

	public static string SetCockpit(ReadOnlySpan<char> chars)
	{
		// TODO: Enum?
		return chars.ToString();
	}

	public static string SetConfig(ReadOnlySpan<char> chars)
	{
		// TODO: Enum?
		return chars.ToString();
	}

	public static string SetDeployment(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetEjection(ReadOnlySpan<char> chars)
	{
		// TODO: Enum? Only one value in files: `Full Head Ejection System`
		return chars.ToString();
	}

	public static string SetEngine(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		return chars.ToString();
	}

	public static int SetEra(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var era))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return era;
	}

	public static string SetGenerator(ReadOnlySpan<char> chars)
	{
		// TODO: More parsing and better return object - Special handle the popular generators
		return chars.ToString();
	}

	public static string SetGyro(ReadOnlySpan<char> chars)
	{
		// TODO: Enum?
		return chars.ToString();
	}

	public static string SetHeatSinks(ReadOnlySpan<char> chars)
	{
		// TODO: Enum ([IS|Clan]? Single, [IS|Clan]? Double, Laser, Compact)
		return chars.ToString();
	}

	public static string SetHistory(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetImageFile(ReadOnlySpan<char> chars)
	{
		// TODO: Maybe some validation? Idk, doesn't feel worth.
		return chars.ToString();
	}

	public static int SetJumpMp(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var jumpMp))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return jumpMp;
	}

	public static string SetLam(ReadOnlySpan<char> chars)
	{
		// TODO: Probably an enum - Only standard in existing files.
		return chars.ToString();
	}

	public static string SetManufacturer(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static int SetMass(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var mass))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return mass;
	}

	public static string SetModel(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetMotive(ReadOnlySpan<char> chars)
	{
		// TODO: enum, Track || Wheel
		return chars.ToString();
	}

	public static int SetMulId(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var mulId))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return mulId;
	}

	public static string SetMyomer(ReadOnlySpan<char> chars)
	{
		// TODO: Kinda enum, kinda freeform :\
		return chars.ToString();
	}

	public static (string Name, string Value) SetNoCrit(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Needs the same investigate as SystemManufacturer - seems to have same format.
		// TODO: Both likely enums `nocrit:Standard:None`
		var delimeterIndex = chars.IndexOf(':');
		if (delimeterIndex == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return (chars[..delimeterIndex].ToString(), chars[(delimeterIndex + 1)..].ToString());
	}

	public static string SetNotes(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetOverview(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static string SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static Role SetRole(ReadOnlySpan<char> chars)
	{
		return chars.ToRole();
	}

	public static RulesLevel SetRulesLevel(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var num))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return num.ToRulesLevel();
	}

	public static string SetSource(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing?
		return chars.ToString();
	}

	public static string SetStructure(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing? Maybe below.
		//"Standard",
		//"Industrial",
		//"Endo Steel",
		//"Endo Steel Prototype",
		//"Reinforced",
		//"Composite",
		//"Endo-Composite"
		return chars.ToString();
	}

	public static (string Part, string Name) SetSystemManufacturer(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Enum part? `^systemmanufacturer:[^:]+:[^:]+$` -- Not sure if Name is allowed to have `:`
		var delimeterIndex = chars.IndexOf(':');
		if (delimeterIndex == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return (chars[..delimeterIndex].ToString(), chars[(delimeterIndex + 1)..].ToString());
	}

	public static (string Part, string Name) SetSystemMode(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Needs the same investigate as SystemManufacturer - seems to have same format.
		var delimeterIndex = chars.IndexOf(':');
		if (delimeterIndex == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return (chars[..delimeterIndex].ToString(), chars[(delimeterIndex + 1)..].ToString());
	}

	public static string SetTechBase(ReadOnlySpan<char> chars)
	{
		// TODO: Enum.
		return chars.ToString();
	}

	public static int SetWalkMp(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var walkMp))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return walkMp;
	}

	public static int SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars, out var count))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return count;
	}
}
