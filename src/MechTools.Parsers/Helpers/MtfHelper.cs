using MechTools.Core.Enums;
using MechTools.Parsers.BattleMech;
using System;
using System.Buffers;
using System.Globalization;

namespace MechTools.Parsers.Helpers;

// TODO: Still have to decide if this is throw||try.
// TODO: Consider trimming pattern here, EG: GetWeaponQuirk
// TODO: Throw if Bound + 1 overflows
public static class MtfHelper
{
	private static readonly SearchValues<string> _engineDelimeterSearchValues =
		SearchValues.Create(["(", "ENGINE"], StringComparison.OrdinalIgnoreCase);
	private static readonly SearchValues<string> _innerSphereMarkerSearchValues =
		SearchValues.Create(["(IS)", "(INNER SPHERE)"], StringComparison.OrdinalIgnoreCase);

	public static ArmourData GetArmour(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		ReadOnlySpan<char> armourSlice;
		Origin origin;
		var originBound = trimmedChars.IndexOf('(');
		if (originBound == -1)
		{
			armourSlice = trimmedChars;
			origin = Origin.Unknown;
		}
		else
		{
			armourSlice = trimmedChars[..originBound].TrimEnd();
			var originSlice = trimmedChars[originBound..];

			if (originSlice.ContainsAny(_innerSphereMarkerSearchValues))
			{
				origin = Origin.InnerSphere;
			}
			else if (originSlice.Equals("(CLAN)", StringComparison.OrdinalIgnoreCase))
			{
				origin = Origin.Clan;
			}
			else
			{
				// Should be either "IS/Clan" or "(Unknown Technology Base)" - but all non-standard values are unknown.
				origin = Origin.Unknown;
			}
		}

		if (armourSlice.StartsWith("IS ", StringComparison.OrdinalIgnoreCase)
			|| armourSlice.StartsWith("CLAN ", StringComparison.OrdinalIgnoreCase))
		{
			armourSlice = armourSlice[(armourSlice.IndexOf(' ') + 1)..].TrimStart();
		}

		const string armourDel = " ARMOR";
		if (armourSlice.EndsWith(armourDel, StringComparison.OrdinalIgnoreCase))
		{
			armourSlice = armourSlice[..^armourDel.Length].TrimEnd();
		}

		return new(MtfEnumConversions.GetArmour(armourSlice), origin);
	}

	public static LocationArmourData GetArmourAtLocation(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Contains(':')
			? GetPatchworkArmourAtLocation(chars)
			: new(ParseSimpleNumber(chars), null, null);

		static LocationArmourData GetPatchworkArmourAtLocation(ReadOnlySpan<char> chars)
		{
			var trimmedChars = chars.Trim();
			var valueBound = trimmedChars.LastIndexOf(':');
			(var armour, var origin) = GetArmour(trimmedChars[..valueBound]);
			return new(ParseSimpleNumber(trimmedChars[(valueBound + 1)..].TrimStart()), armour, origin);
		}
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
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static EngineData GetEngine(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (trimmedChars.Equals("(NONE)", StringComparison.OrdinalIgnoreCase))
		{
			return new(Engine.None, false, false, 0);
		}
		else if (!trimmedChars.Contains(" ENGINE", StringComparison.OrdinalIgnoreCase))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		var sizeBound = trimmedChars.IndexOf(' ');
		if (sizeBound == -1 || !int.TryParse(trimmedChars[..sizeBound], NumberStyles.None, null, out var size))
		{
			return ThrowHelper.ExceptionToSpecifyLater<EngineData>();
		}

		// TODO: Consider none handling... (none)
		var engineBound = trimmedChars.IndexOfAny(_engineDelimeterSearchValues);
		if (engineBound == -1)
		{
			engineBound = trimmedChars.Length;
		}
		var engine = MtfEnumConversions.GetEngine(trimmedChars[(sizeBound + 1)..engineBound].Trim());

		var hasClanFlag = trimmedChars.Contains("(CLAN)", StringComparison.OrdinalIgnoreCase);
		var hasInnerSphereFlag = trimmedChars.ContainsAny(_innerSphereMarkerSearchValues);

		return new(engine, hasClanFlag, hasInnerSphereFlag, size);
	}

	public static EquipmentData GetEquipmentAtLocation(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (MtfValues.Lookup.CommonEquipmentValues.TryGetValue(trimmedChars, out var cachedValue))
		{
			return new(false, false, false, cachedValue);
		}

		var omnipodBound = trimmedChars.LastIndexOf(" (OMNIPOD)", StringComparison.OrdinalIgnoreCase);
		var rearBound = trimmedChars.LastIndexOf(" (R)", StringComparison.OrdinalIgnoreCase);
		var turretBound = trimmedChars.LastIndexOf(" (T)", StringComparison.OrdinalIgnoreCase);

		return rearBound == -1 || turretBound == -1 || omnipodBound == -1
			? new(false, false, false, chars.ToString())
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
		return chars.ToString();
	}

	public static Gyro GetGyro(ReadOnlySpan<char> chars)
	{
		return MtfEnumConversions.GetGyro(chars.Trim());
	}

	public static string GetHeatSinkKit(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static HeatSinkData GetHeatSinks(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		var countBound = trimmedChars.IndexOf(' ');
		if (countBound == -1 || !int.TryParse(trimmedChars[..countBound], NumberStyles.None, null, out var count))
		{
			return ThrowHelper.ExceptionToSpecifyLater<HeatSinkData>();
		}

		Origin origin;
		HeatSink heatSink;

		var workingChars = trimmedChars[(countBound + 1)..].TrimStart();

		var legacyBound = workingChars.IndexOf('(');
		if (legacyBound != -1)
		{
			// Legacy format `Double \(((Clan)|(Inner Sphere))\)`
			var originChars = workingChars[(legacyBound + 1)..];
			if (originChars.Equals("CLAN)", StringComparison.OrdinalIgnoreCase))
			{
				origin = Origin.Clan;
			}
			else if (originChars.Equals("INNER SPHERE)", StringComparison.OrdinalIgnoreCase))
			{
				origin = Origin.InnerSphere;
			}
			else
			{
				origin = Origin.Unknown;
			}

			heatSink = MtfEnumConversions.GetHeatSinks(workingChars[..legacyBound].TrimEnd());
		}
		else
		{
			// Modern format `((Clan )|(IS ))?Double`
			const string clanDel = "CLAN ";
			const string innerSphereDel = "IS ";

			if (workingChars.StartsWith(clanDel, StringComparison.OrdinalIgnoreCase))
			{
				origin = Origin.Clan;
				workingChars = workingChars[clanDel.Length..].TrimStart();
			}
			else if (workingChars.StartsWith(innerSphereDel, StringComparison.OrdinalIgnoreCase))
			{
				origin = Origin.InnerSphere;
				workingChars = workingChars[innerSphereDel.Length..].TrimStart();
			}
			else
			{
				origin = Origin.Unknown;
			}

			heatSink = MtfEnumConversions.GetHeatSinks(workingChars);
		}

		return new(count, heatSink, origin);
	}

	public static string GetHistory(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetImageFile(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
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
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
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

	public static Motive GetMotive(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return MtfEnumConversions.GetMotive(chars.Trim());
	}

	public static int GetMulId(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static Myomer GetMyomer(ReadOnlySpan<char> chars)
	{
		return MtfEnumConversions.GetMyomer(chars.Trim());
	}

	public static NoCritData GetNoCrit(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		const char del = ':';

		var bound = trimmedChars.IndexOf(del);
		if (bound < 1 || bound + 1 == trimmedChars.Length)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return new(
			MtfEnumConversions.GetEquipmentLocationFromAbbreviation(trimmedChars[(bound + 1)..].TrimStart()),
			trimmedChars[..bound].TrimEnd().ToString());
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
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
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

	public static SourceData GetSource(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		// First ':' is type delimeter, remaining text assumed to be name.
		// If a typeless name contains a colon... It shouldn't.

		var bound = chars.IndexOf(':');
		return bound != -1 && bound != chars.Length - 1
			? new(chars[(bound + 1)..].Trim().ToString(), chars[..bound].Trim().ToString())
			: new(chars.Trim().ToString(), null);
	}

	public static Structure GetStructure(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		const string clanDel = "CLAN ";
		const string innerSphereDel = "IS ";

		var trimmedChars = chars.Trim();
		if (trimmedChars.StartsWith(clanDel, StringComparison.OrdinalIgnoreCase))
		{
			trimmedChars = trimmedChars[clanDel.Length..].TrimStart();
		}
		else if (trimmedChars.StartsWith(innerSphereDel, StringComparison.OrdinalIgnoreCase))
		{
			trimmedChars = trimmedChars[innerSphereDel.Length..].TrimStart();
		}

		return MtfEnumConversions.GetStructure(trimmedChars);
	}

	public static SpecificSystemData GetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return GetSpecificSystem(chars.Trim());
	}

	public static SpecificSystemData GetSystemModel(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return GetSpecificSystem(chars.Trim());
	}

	public static TechBase GetTechBase(ReadOnlySpan<char> chars)
	{
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return MtfEnumConversions.GetTechBase(chars.Trim());
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
		ThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		const string stdDel = ", ";

		var lastBound = trimmedChars.LastIndexOf(stdDel, StringComparison.Ordinal);
		if (lastBound == -1 || lastBound == trimmedChars.Length - stdDel.Length)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		int? count;
		ReadOnlySpan<char> nameSlice;
		ReadOnlySpan<char> locationSlice;
		int? ammo;

		if (char.IsNumber(trimmedChars[0]))
		{
			var nameBound = trimmedChars.IndexOf(' ');
			count = int.Parse(trimmedChars[..nameBound].TrimEnd(), NumberStyles.None, CultureInfo.InvariantCulture);
			if (count < 1)
			{
				return ThrowHelper.ExceptionToSpecifyLater<WeaponListData>();
			}

			const string ammoDel = "Ammo:";

			if (trimmedChars[(lastBound + stdDel.Length)..].TrimStart().StartsWith(ammoDel, StringComparison.OrdinalIgnoreCase))
			{
				//! `1 ISLBXAC10, Right Torso, Ammo:20`
				ammo = int.Parse(
					trimmedChars[(lastBound + stdDel.Length + ammoDel.Length)..].TrimStart(), // TODO: Clean up
					NumberStyles.None,
					CultureInfo.InvariantCulture);
				var locationBound = trimmedChars[..lastBound].LastIndexOf(stdDel, StringComparison.Ordinal);
				nameSlice = trimmedChars[(nameBound + 1)..locationBound].Trim();
				locationSlice = trimmedChars[(locationBound + stdDel.Length)..lastBound].Trim();
			}
			else
			{
				//! `1 ISC3SlaveUnit, Center Torso`
				ammo = null;
				nameSlice = trimmedChars[(nameBound + 1)..lastBound].Trim();
				locationSlice = trimmedChars[(lastBound + stdDel.Length)..].TrimStart();
			}
		}
		else if (trimmedChars[0] == '-')
		{
			return ThrowHelper.ExceptionToSpecifyLater<WeaponListData>();
		}
		else
		{
			//! `Medium Pulse Laser, Center Torso`
			count = null;
			ammo = null;
			nameSlice = trimmedChars[..lastBound];
			locationSlice = trimmedChars[(lastBound + stdDel.Length)..];
		}

		if (nameSlice.IsWhiteSpace())
		{
			return ThrowHelper.ExceptionToSpecifyLater<WeaponListData>();
		}

		var isRear = false;

		const string rearDel = " (R)";
		if (locationSlice.EndsWith(rearDel, StringComparison.OrdinalIgnoreCase))
		{
			isRear = true;
			locationSlice = locationSlice[..^rearDel.Length];
		}

		if (nameSlice.EndsWith(rearDel, StringComparison.OrdinalIgnoreCase))
		{
			nameSlice = nameSlice[..^rearDel.Length];
		}

		var location = MtfEnumConversions.GetEquipmentLocation(locationSlice);
		return new(ammo, count, isRear, location, nameSlice.ToString());
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

	private static SpecificSystemData GetSpecificSystem(ReadOnlySpan<char> trimmedChars)
	{
		const char del = ':';

		var bound = trimmedChars.IndexOf(del);
		if (bound < 1 || bound + 1 == trimmedChars.Length)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		var nameSlice = trimmedChars[(bound + 1)..].TrimStart();
		return new(nameSlice.ToString(), MtfEnumConversions.GetSpecificSystem(trimmedChars[..bound].TrimEnd()));
	}

	private static int ParseSimpleNumber(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out var number))
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		return number;
	}
}
