using MechTools.Core;
using MechTools.Parsers.BattleMech;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MechTools.Parsers.Extensions;

// TODO: Move, these aren't extensions anymore.
// TODO: Naming of all of these, for now they just match the caller.
// TODO: Still have to decide if this is throw||try.
public static class HelperExtensions
{
	public static string AddComment(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static EquipmentData AddEquipmentAtLocation(ReadOnlySpan<char> chars)
	{
		// TODO: Real return object.

		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (MtfValues.Lookup.CommonEquipmentValues.TryGetValue(trimmedChars, out var cachedValue))
		{
			return new(cachedValue, false, false, false);
		}

		const string omnipodDel = " (OMNIPOD)";
		const string rearDel = " (R)";
		const string turretDel = " (T)";

		var omnipodBound = trimmedChars.LastIndexOf(omnipodDel, StringComparison.OrdinalIgnoreCase);
		var rearBound = trimmedChars.LastIndexOf(rearDel, StringComparison.OrdinalIgnoreCase);
		var turretBound = trimmedChars.LastIndexOf(turretDel, StringComparison.OrdinalIgnoreCase);

		return rearBound == -1 || turretBound == -1 || omnipodBound == -1
			? new(chars.ToString(), false, false, false)
			: AddAnnotatedEquipmentAtLocation(trimmedChars, omnipodBound, rearBound, turretBound);
	}

	public static string AddQuirk(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static WeaponQuirkData AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

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
		var name = chars[enumerator.Current].Trim();
		_ = enumerator.MoveNext();
		var location = chars[enumerator.Current].Trim().FromAbbreviationToEquipmentLocation();
		_ = enumerator.MoveNext();
		// TODO: TryParse
		var slot = ParseSimpleNumber(chars[enumerator.Current]);
		_ = enumerator.MoveNext();
		var weapon = chars[enumerator.Current].Trim();

		return new(location, name.ToString(), slot, weapon.ToString());
	}

	public static WeaponListData AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		// TODO: This needs a proper return object at this point, whether you're dropping ValueTuple or not.

		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (trimmedChars.Length < 4)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		const string stdDel = ", ";
		const char nameDel = ' ';

		var lastBound = trimmedChars.LastIndexOf(stdDel, StringComparison.Ordinal);
		if (lastBound == -1 || lastBound == trimmedChars.Length - stdDel.Length)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		int? count;
		string name;
		ReadOnlySpan<char> locationSlice;
		int? ammo;

		if (char.IsNumber(trimmedChars[0]))
		{
			var nameBound = trimmedChars.IndexOf(nameDel);
			count = int.Parse(trimmedChars[..nameBound]);

			// TODO: Be more whitespace tolerant.
			if (char.ToUpperInvariant(trimmedChars[lastBound + stdDel.Length]) == 'A')
			{
				//! `1 ISLBXAC10, Right Torso, Ammo:20`
				ammo = int.Parse(trimmedChars[(lastBound + stdDel.Length + "Ammo:".Length)..]);
				var locationBound = trimmedChars[..lastBound].LastIndexOf(stdDel, StringComparison.Ordinal);
				name = trimmedChars[(nameBound + 1)..locationBound].ToString();
				locationSlice = trimmedChars[(locationBound + stdDel.Length)..lastBound];
			}
			else
			{
				//! `1 ISC3SlaveUnit, Center Torso`
				ammo = null;
				name = trimmedChars[(nameBound + 1)..lastBound].ToString();
				locationSlice = trimmedChars[(lastBound + stdDel.Length)..];
			}
		}
		else
		{
			//! `Medium Pulse Laser, Center Torso`
			count = null;
			name = trimmedChars[..lastBound].ToString();
			locationSlice = trimmedChars[(lastBound + stdDel.Length)..];
			ammo = null;
		}

		var isRear = false;

		const string rearDel = " (R)";
		if (locationSlice.EndsWith(rearDel, StringComparison.OrdinalIgnoreCase))
		{
			isRear = true;
			locationSlice = locationSlice[..^rearDel.Length];
		}

		var location = locationSlice.ToEquipmentLocation();
		return new(ammo, count, location, name, isRear);
	}

	public static string SetArmourType(ReadOnlySpan<char> chars)
	{
		// TODO: Enum-ify.
		// Brackets = origin
		return chars.Trim().ToString();
	}

	public static (string? Name, int Value) SetArmourAtLocation(ReadOnlySpan<char> chars)
	{
		// TODO: Ugh. This.
		// TODO: This will fail on weirder values - See ` armor:[^\d]`
		// Mix armour mech things.
		// IS Hardened(Inner Sphere):12

		const char del = ':';

		string? name;
		int value;
		var bound = chars.LastIndexOf(del);
		if (bound != -1)
		{
			name = chars[..bound].ToString();
			value = int.Parse(chars[(bound + 1)..]);
		}
		else
		{
			name = null;
			value = int.Parse(chars);
		}

		return (name, value);
	}

	public static int SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string SetCapabilities(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static string SetChassis(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string SetClanName(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static Cockpit SetCockpit(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToCockpit();
	}

	public static ConfigurationData SetConfig(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		const string omniMechDel = " OmniMech";
		const string omniMekDel = " OmniMek";

		var configurationSlice = trimmedChars;
		var isOmniMech = false;
		if (trimmedChars.EndsWith(omniMechDel, StringComparison.OrdinalIgnoreCase))
		{
			isOmniMech = true;
			configurationSlice = trimmedChars[..^omniMechDel.Length].TrimEnd();
		}
		else if (trimmedChars.EndsWith(omniMekDel, StringComparison.OrdinalIgnoreCase))
		{
			isOmniMech = true;
			configurationSlice = trimmedChars[..^omniMekDel.Length].TrimEnd();
		}

		var configuration = configurationSlice.ToConfiguration();
		return new(configuration, isOmniMech);
	}

	public static string SetDeployment(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string SetEjection(ReadOnlySpan<char> chars)
	{
		// TODO: bool - `Full Head Ejection System` IC or not.
		return chars.Trim().ToString();
	}

	public static string SetEngine(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static int SetEra(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string SetGenerator(ReadOnlySpan<char> chars)
	{
		// TODO: More parsing and better return object - Special handle the popular generators
		return chars.Trim().ToString();
	}

	public static string SetGyro(ReadOnlySpan<char> chars)
	{
		// TODO: Enum?
		return chars.Trim().ToString();
	}

	public static string SetHeatSinks(ReadOnlySpan<char> chars)
	{
		// TODO: Enum ([IS|Clan]? Single, [IS|Clan]? Double, Laser, Compact)
		return chars.Trim().ToString();
	}

	public static string SetHistory(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static string SetImageFile(ReadOnlySpan<char> chars)
	{
		// TODO: Maybe some validation? Idk, doesn't feel worth.
		return chars.Trim().ToString();
	}

	public static int SetJumpMp(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string SetLam(ReadOnlySpan<char> chars)
	{
		// TODO: Probably an enum - Only standard in existing files.
		return chars.Trim().ToString();
	}

	public static string SetManufacturer(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static int SetMass(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string? SetModel(ReadOnlySpan<char> chars)
	{
		return chars.IsWhiteSpace() ? null : chars.Trim().ToString();
	}

	public static string SetMotive(ReadOnlySpan<char> chars)
	{
		// TODO: enum, Track || Wheel
		return chars.Trim().ToString();
	}

	public static int SetMulId(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string SetMyomer(ReadOnlySpan<char> chars)
	{
		// TODO: Kinda enum, kinda freeform :\
		return chars.Trim().ToString();
	}

	public static (string Name, string Value) SetNoCrit(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Needs the same investigate as SystemManufacturer - seems to have same format.
		// TODO: Both likely enums `nocrit:Standard:None`

		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var bound = chars.IndexOf(':');
		if (bound == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		// TODO: Test bound isn't too far along in chars.

		return (chars[..bound].ToString().Trim(), chars[(bound + 1)..].Trim().ToString());
	}

	public static string SetNotes(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static string SetOverview(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static string SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static Role SetRole(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToRole();
	}

	public static RulesLevel SetRulesLevel(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars).ToRulesLevel();
	}

	public static (string? Type, string Name) SetSource(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		// First ':' is type delimeter, remaining text assumed to be name.
		// If a typeless name contains a colon... It shouldn't.

		var bound = chars.IndexOf(':');
		return bound != -1 && bound != chars.Length - 1
			? (chars[..bound].Trim().ToString(), chars[(bound + 1)..].Trim().ToString())
			: (null, chars.Trim().ToString());
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
		return chars.Trim().ToString();
	}

	public static (string Part, string Name) SetSystemManufacturer(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Enum part? `^systemmanufacturer:[^:]+:[^:]+$` -- Not sure if Name is allowed to have `:`
		var bound = chars.IndexOf(':');
		if (bound == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		// TODO: Test bound isn't too far along in chars.

		return (chars[..bound].Trim().ToString(), chars[(bound + 1)..].Trim().ToString());
	}

	public static (string Part, string Name) SetSystemMode(ReadOnlySpan<char> chars) //! TODO
	{
		// TODO: Needs the same investigate as SystemManufacturer - seems to have same format.
		var bound = chars.IndexOf(':');
		if (bound == -1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		// TODO: Test bound isn't too far along in chars.

		return (chars[..bound].Trim().ToString(), chars[(bound + 1)..].Trim().ToString());
	}

	public static string SetTechBase(ReadOnlySpan<char> chars)
	{
		// TODO: Enum.
		return chars.Trim().ToString();
	}

	public static int SetWalkMp(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static int SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	private static EquipmentData AddAnnotatedEquipmentAtLocation(
		ReadOnlySpan<char> trimmedChars,
		int omnipodBound,
		int rearBound,
		int turretBound)
	{
		// TODO: Come back to this when you feel like it.

		// Let's be order independent.
		// R ; T ; omni ; R omni ; T omni
		// TODO: Omnipod ` (omnipod)`
		// TODO: Rear location ` (R)`
		// TODO: Turret ` (T)`

		throw new NotImplementedException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int ParseSimpleNumber(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars.Trim(), NumberStyles.None, null, out var number))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return number;
	}
}
