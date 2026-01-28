using MechTools.Parsers.Data;
using MechTools.Parsers.Enums;
using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Mtf;

// TODO: Still have to decide if this is throw||try.
// TODO: Consider trimming pattern here, EG: GetWeaponQuirk
// TODO: Throw if Bound + 1 overflows
public static class MtfHelpers
{
	private static readonly SearchValues<string> _engineDelimiterSearchValues =
		SearchValues.Create(["(", " ENGINE"], StringComparison.OrdinalIgnoreCase);
	private static readonly SearchValues<string> _innerSphereMarkerSearchValues =
		SearchValues.Create(["(IS)", "(INNER SPHERE)"], StringComparison.OrdinalIgnoreCase);

	public static ArmourData GetArmour(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

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

		return new(EnumConversions.GetArmour(armourSlice), origin);
	}

	public static LocationArmourData GetArmourAtLocation(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Contains(':')
			? GetPatchworkArmourAtLocation(chars)
			: new(ParseSimpleNumber(chars), null, null);

		static LocationArmourData GetPatchworkArmourAtLocation(ReadOnlySpan<char> chars)
		{
			var trimmedChars = chars.Trim();
			var valueBound = trimmedChars.LastIndexOf(':');
			(var armour, var origin) = GetArmour(trimmedChars[..valueBound]);
			return new(ParseSimpleNumber(trimmedChars[(valueBound + 1)..]), armour, origin);
		}
	}

	public static int GetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string GetCapabilities(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetChassis(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetClanName(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static Cockpit GetCockpit(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return EnumConversions.GetCockpit(chars.Trim());
	}
	public static string GetComment(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static ConfigurationData GetConfiguration(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

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

		var configuration = EnumConversions.GetConfiguration(configurationSlice);
		return new(configuration, isOmniMech);
	}

	public static string GetDeployment(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetEjection(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static EngineData GetEngine(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (trimmedChars.Equals("(NONE)", StringComparison.OrdinalIgnoreCase))
		{
			return new(Engine.None, false, false, 0);
		}
		else if (!trimmedChars.Contains(" ENGINE", StringComparison.OrdinalIgnoreCase))
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		var sizeBound = trimmedChars.IndexOf(' ');
		if (sizeBound == -1
			|| !int.TryParse(trimmedChars[..sizeBound], NumberStyles.None, CultureInfo.InvariantCulture, out var size))
		{
			return MtfThrowHelper.ThrowInvalidValueException<EngineData>(chars);
		}

		// TODO: Consider none handling... (none)
		var engineBound = trimmedChars.IndexOfAny(_engineDelimiterSearchValues);
		if (engineBound == -1)
		{
			engineBound = trimmedChars.Length;
		}
		var engine = EnumConversions.GetEngine(trimmedChars[(sizeBound + 1)..engineBound].Trim());

		var hasClanFlag = trimmedChars.Contains("(CLAN)", StringComparison.OrdinalIgnoreCase);
		var hasInnerSphereFlag = trimmedChars.ContainsAny(_innerSphereMarkerSearchValues);

		return new(engine, hasClanFlag, hasInnerSphereFlag, size);
	}

	public static EquipmentData GetEquipmentAtLocation(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();
		if (CommonValues.EquipmentLookup.TryGetValue(trimmedChars, out var cachedValue))
		{
			return new(false, false, false, cachedValue);
		}

		var bound = trimmedChars.IndexOf('(');
		if (bound < 0)
		{
			return new(false, false, false, trimmedChars.ToString());
		}
		else if (bound == 0)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		return GetAnnotatedEquipmentAtLocation(trimmedChars);

		static EquipmentData GetAnnotatedEquipmentAtLocation(ReadOnlySpan<char> trimmedChars)
		{
			var nameBound = (uint)trimmedChars.Length;
			var isOmniPod = ProcessFlag(
				trimmedChars.LastIndexOf(" (OMNIPOD)", StringComparison.OrdinalIgnoreCase),
				ref nameBound);
			var isRear = ProcessFlag(
				trimmedChars.LastIndexOf(" (R)", StringComparison.OrdinalIgnoreCase),
				ref nameBound);
			var isTurret = ProcessFlag(
				trimmedChars.LastIndexOf(" (T)", StringComparison.OrdinalIgnoreCase),
				ref nameBound);

			//! Convincing the JIT to elide bounds check.
			var name = MemoryMarshal.CreateReadOnlySpan(in MemoryMarshal.GetReference(trimmedChars), (int)nameBound)
				.TrimEnd()
				.ToString();

			return new(isOmniPod, isRear, isTurret, name);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static bool ProcessFlag(int target, ref uint minBound)
			{
				if (target < 0)
				{
					return false;
				}

				minBound = uint.Min(minBound, (uint)target);
				return true;
			}
		}
	}

	public static int GetEra(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static string GetGenerator(ReadOnlySpan<char> chars)
	{
		// TODO: More parsing and better return object - Special handle the popular generators
		return chars.ToString();
	}

	public static Gyro GetGyro(ReadOnlySpan<char> chars)
	{
		return EnumConversions.GetGyro(chars.Trim());
	}

	public static string GetHeatSinkKit(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static HeatSinkData GetHeatSinks(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		var countBound = trimmedChars.IndexOf(' ');
		if (countBound == -1
			|| !int.TryParse(trimmedChars[..countBound], NumberStyles.None, CultureInfo.InvariantCulture, out var count))
		{
			return MtfThrowHelper.ThrowInvalidValueException<HeatSinkData>(chars);
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

			heatSink = EnumConversions.GetHeatSinks(workingChars[..legacyBound].TrimEnd());
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

			heatSink = EnumConversions.GetHeatSinks(workingChars);
		}

		return new(count, heatSink, origin);
	}

	public static string GetHistory(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetImageFile(ReadOnlySpan<char> chars)
	{
		return chars.ToString();
	}

	public static int GetJumpMp(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static Lam GetLam(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return EnumConversions.GetLam(chars.Trim());
	}

	public static string GetManufacturer(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static int GetMass(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		var mass = ParseSimpleNumber(chars);
		if (mass < 1)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}
		return mass;
	}

	public static string? GetModel(ReadOnlySpan<char> chars)
	{
		return chars.IsWhiteSpace() ? null : chars.Trim().ToString();
	}

	public static Motive GetMotive(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return EnumConversions.GetMotive(chars.Trim());
	}

	public static int GetMulId(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static Myomer GetMyomer(ReadOnlySpan<char> chars)
	{
		return EnumConversions.GetMyomer(chars.Trim());
	}

	public static NoCritData GetNoCrit(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		const char del = ':';

		var bound = trimmedChars.IndexOf(del);
		if (bound < 1 || bound + 1 == trimmedChars.Length)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		return new(
			EnumConversions.GetEquipmentLocationFromAbbreviation(trimmedChars[(bound + 1)..].TrimStart()),
			trimmedChars[..bound].TrimEnd().ToString());
	}

	public static string GetNotes(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetOverview(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static string GetQuirk(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return chars.Trim().ToString();
	}

	public static Role GetRole(ReadOnlySpan<char> chars)
	{
		return EnumConversions.GetRole(chars.Trim());
	}

	public static RulesLevel GetRulesLevel(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return EnumConversions.GetRulesLevel(ParseSimpleNumber(chars));
	}

	public static SourceData GetSource(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		// First ':' is type delimiter, remaining text assumed to be name.
		// If a typeless name contains a colon... It shouldn't.

		var bound = chars.IndexOf(':');
		return bound != -1 && bound != chars.Length - 1
			? new(chars[(bound + 1)..].Trim().ToString(), chars[..bound].Trim().ToString())
			: new(chars.Trim().ToString(), null);
	}

	public static Structure GetStructure(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

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

		return EnumConversions.GetStructure(trimmedChars);
	}

	public static SpecificSystemData GetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return GetSpecificSystem(chars);
	}

	public static SpecificSystemData GetSystemModel(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return GetSpecificSystem(chars);
	}

	public static TechBase GetTechBase(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return EnumConversions.GetTechBase(chars.Trim());
	}

	public static int GetWalkMp(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		var walkMp = ParseSimpleNumber(chars);
		if (walkMp < 1)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}
		return walkMp;
	}

	public static WeaponListData GetWeaponForWeaponList(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		var trimmedChars = chars.Trim();

		const string stdDel = ", ";

		var lastBound = trimmedChars.LastIndexOf(stdDel, StringComparison.Ordinal);
		if (lastBound == -1 || lastBound == trimmedChars.Length - stdDel.Length)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
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
				MtfThrowHelper.ThrowInvalidValueException(chars);
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
			return MtfThrowHelper.ThrowInvalidValueException<WeaponListData>(chars);
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
			MtfThrowHelper.ThrowInvalidValueException(chars);
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

		var location = EnumConversions.GetEquipmentLocation(locationSlice);
		return new(ammo, count, isRear, location, nameSlice.ToString());
	}

	public static int GetWeaponListCount(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);
		return ParseSimpleNumber(chars);
	}

	public static WeaponQuirkData GetWeaponQuirk(ReadOnlySpan<char> chars)
	{
		MtfThrowHelper.ThrowIfEmptyOrWhiteSpace(chars);

		if (chars.Length < 10
			|| chars[0] == ':'
			|| chars[^1] == ':'
			|| chars.Count(':') != 3
			|| chars.Contains([':', ':'], StringComparison.Ordinal))
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		var enumerator = chars.Split(':');
		_ = enumerator.MoveNext();
		var name = chars[enumerator.Current].Trim();
		_ = enumerator.MoveNext();
		var location = EnumConversions.GetEquipmentLocationFromAbbreviation(chars[enumerator.Current].Trim());
		_ = enumerator.MoveNext();
		var slot = ParseSimpleNumber(chars[enumerator.Current]);
		_ = enumerator.MoveNext();
		var weapon = chars[enumerator.Current].Trim().ToString();

		return new(location, name.ToString(), slot, weapon);
	}

	private static SpecificSystemData GetSpecificSystem(ReadOnlySpan<char> chars)
	{
		const char del = ':';

		var trimmedChars = chars.Trim();
		var bound = trimmedChars.IndexOf(del);
		if (bound < 1 || bound + 1 == trimmedChars.Length)
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		var nameSlice = trimmedChars[(bound + 1)..].TrimStart();
		return new(nameSlice.ToString(), EnumConversions.GetSpecificSystem(trimmedChars[..bound].TrimEnd()));
	}

	private static int ParseSimpleNumber(ReadOnlySpan<char> chars)
	{
		if (!int.TryParse(chars.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out var number))
		{
			MtfThrowHelper.ThrowInvalidValueException(chars);
		}

		return number;
	}
}
