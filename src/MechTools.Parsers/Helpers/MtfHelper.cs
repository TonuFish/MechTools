using MechTools.Core.Enums;
using MechTools.Parsers.BattleMech;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MechTools.Parsers.Helpers;

// TODO: Still have to decide if this is throw||try.
public static class MtfHelper
{
	public static (string? Name, int Value) GetArmourAtLocation(ReadOnlySpan<char> chars)
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

	public static (ArmourType Armour, Origin? Origin) GetArmourType(ReadOnlySpan<char> chars)
	{
		// TODO: Enum-ify.
		// Brackets = origin
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return (default, default);
	}

	public static int GetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string GetCapabilities(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetChassis(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetClanName(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static Cockpit GetCockpit(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return MtfEnumConversions.GetCockpit(chars.Trim());
	}
	public static string GetComment(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static ConfigurationData GetConfiguration(ReadOnlySpan<char> chars)
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

		var configuration = MtfEnumConversions.GetConfiguration(configurationSlice);
		return new(configuration, isOmniMech);
	}

	public static string GetDeployment(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetEjection(ReadOnlySpan<char> chars)
	{
		// TODO: bool - `Full Head Ejection System` IC or not.
		return chars.Trim().ToString();
	}

	public static string GetEngine(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static EquipmentData GetEquipmentAtLocation(ReadOnlySpan<char> chars)
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
			: GetAnnotatedEquipmentAtLocation(trimmedChars, omnipodBound, rearBound, turretBound);
	}

	public static int GetEra(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string GetGenerator(ReadOnlySpan<char> chars)
	{
		// TODO: More parsing and better return object - Special handle the popular generators
		return chars.Trim().ToString();
	}

	public static Gyro GetGyro(ReadOnlySpan<char> chars)
	{
		return MtfEnumConversions.GetGyro(chars.Trim());
	}

	public static string GetHeatSinks(ReadOnlySpan<char> chars)
	{
		// TODO: Enum ([IS|Clan]? Single, [IS|Clan]? Double, Laser, Compact)
		return chars.Trim().ToString();
	}

	public static string GetHistory(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetImageFile(ReadOnlySpan<char> chars)
	{
		// TODO: Maybe some validation? Idk, doesn't feel worth.
		return chars.Trim().ToString();
	}

	public static int GetJumpMp(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static Lam GetLam(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return MtfEnumConversions.GetLam(chars.Trim());
	}

	public static string GetManufacturer(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static int GetMass(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		var mass = ParseSimpleNumber(chars);
		if (mass < 1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}
		return mass;
	}

	public static string? GetModel(ReadOnlySpan<char> chars)
	{
		return chars.IsWhiteSpace() ? null : chars.Trim().ToString();
	}

	public static string GetMotive(ReadOnlySpan<char> chars)
	{
		// TODO: enum, Track || Wheel
		return chars.Trim().ToString();
	}

	public static int GetMulId(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string GetMyomer(ReadOnlySpan<char> chars)
	{
		// TODO: Kinda enum, kinda freeform :\
		return chars.Trim().ToString();
	}

	public static (string Name, string Value) GetNoCrit(ReadOnlySpan<char> chars) //! TODO
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

		return (chars[..bound].Trim().ToString(), chars[(bound + 1)..].Trim().ToString());
	}

	public static string GetNotes(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetOverview(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		return chars.Trim().ToString();
	}

	public static string GetQuirk(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static Role GetRole(ReadOnlySpan<char> chars)
	{
		return MtfEnumConversions.GetRole(chars.Trim());
	}

	public static RulesLevel GetRulesLevel(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return MtfEnumConversions.GetRulesLevel(ParseSimpleNumber(chars));
	}

	public static (string? Type, string Name) GetSource(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		// First ':' is type delimeter, remaining text assumed to be name.
		// If a typeless name contains a colon... It shouldn't.

		var bound = chars.IndexOf(':');
		return bound != -1 && bound != chars.Length - 1
			? (chars[..bound].Trim().ToString(), chars[(bound + 1)..].Trim().ToString())
			: (null, chars.Trim().ToString());
	}

	public static string GetStructure(ReadOnlySpan<char> chars)
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

	public static (string Part, string Name) GetSystemManufacturer(ReadOnlySpan<char> chars) //! TODO
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

	public static (string Part, string Name) GetSystemMode(ReadOnlySpan<char> chars) //! TODO
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

	public static string GetTechBase(ReadOnlySpan<char> chars)
	{
		// TODO: Enum.
		return chars.Trim().ToString();
	}

	public static int GetWalkMp(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		var walkMp = ParseSimpleNumber(chars);
		if (walkMp < 1)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}
		return walkMp;
	}

	public static WeaponListData GetWeaponForWeaponList(ReadOnlySpan<char> chars)
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

		var location = MtfEnumConversions.GetEquipmentLocation(locationSlice);
		return new(ammo, count, location, name, isRear);
	}

	public static int GetWeaponListCount(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static WeaponQuirkData GetWeaponQuirk(ReadOnlySpan<char> chars)
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
		var location = MtfEnumConversions.GetEquipmentLocationFromAbbreviation(chars[enumerator.Current].Trim());
		_ = enumerator.MoveNext();
		var slot = ParseSimpleNumber(chars[enumerator.Current]);
		_ = enumerator.MoveNext();
		var weapon = chars[enumerator.Current].Trim().ToString();

		return new(location, name.ToString(), slot, weapon);
	}

	private static EquipmentData GetAnnotatedEquipmentAtLocation(
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
