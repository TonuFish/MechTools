using MechTools.Core.Enums;
using MechTools.Parsers.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MechTools.Parsers.BattleMech;

internal sealed class RawBattleMechBuilder : IBattleMechBuilder<List<string>>
{
	private readonly List<string> _lines = new(capacity: 190); // TODO: Better number.

	public void AddComment(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetComment(chars);
		_lines.Add(value);
	}

	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location)
	{
		(var isOmniPod, var isRear, var isTurret, var name) = MtfHelper.GetEquipmentAtLocation(chars);
		if (!isOmniPod && !isRear && !isTurret)
		{
			_lines.Add(name);
			return;
		}

		const string rearSuffix = " (R)";
		const string turretSuffix = " (T)";
		const string omnipodSuffix = " (OMNIPOD)";

		StringBuilder sb = new(
			value: name,
			capacity: name.Length + rearSuffix.Length + turretSuffix.Length + omnipodSuffix.Length);
		if (isRear)
		{
			_ = sb.Append(rearSuffix);
		}
		if (isTurret)
		{
			_ = sb.Append(turretSuffix);
		}
		if (isOmniPod)
		{
			_ = sb.Append(omnipodSuffix);
		}
		_lines.Add(sb.ToString());
	}

	public void AddQuirk(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetQuirk(chars);
		_lines.Add(value);
	}

	public void AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		(var location, var name, var slot, var weapon) = MtfHelper.GetWeaponQuirk(chars);
		_lines.Add($"{name}:{location}:{slot}:{weapon}");
	}

	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		(var ammo, var count, var isRear, var location, var name) = MtfHelper.GetWeaponForWeaponList(chars);
		if (count.HasValue)
		{
			_lines.Add($"{count} {name}, {location}{(isRear ? " (R)" : "")}{(ammo.HasValue ? $", ammo:{ammo}" : "")}");
		}
		else
		{
			_lines.Add($"{name}, {location}");
		}
	}

	public List<string> Build()
	{
		// TODO: Check if this is actually better than new(_lines) at runtime; may skip the typeof optimisation.
		return [.. _lines];
	}

	public void SetArmour(ReadOnlySpan<char> chars)
	{
		(var armour, var origin) = MtfHelper.GetArmour(chars);
		_lines.Add($"{armour}{(origin != Origin.Unknown ? $" ({origin})" : "")}");
	}

	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location)
	{
		(var value, var armour, var origin) = MtfHelper.GetArmourAtLocation(chars);
		if (armour is not null)
		{
			_lines.Add($"{armour.Value}({origin!.Value}):{value}");
		}
		else
		{
			_lines.Add(value.ToString(CultureInfo.InvariantCulture));
		}
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetBaseChassisHeatSinks(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetCapabilities(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetCapabilities(chars);
		_lines.Add(value);
	}

	public void SetChassis(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetChassis(chars);
		_lines.Add(value);
	}

	public void SetClanName(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetClanName(chars);
		_lines.Add(value);
	}

	public void SetCockpit(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetCockpit(chars);
		_lines.Add(value.ToString());
	}

	public void SetConfiguration(ReadOnlySpan<char> chars)
	{
		(var configuration, var isOmniMech) = MtfHelper.GetConfiguration(chars);
		_lines.Add($"{configuration}{(isOmniMech ? " OmniMek" : "")}");
	}

	public void SetDeployment(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetDeployment(chars);
		_lines.Add(value);
	}

	public void SetEjection(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetEjection(chars);
		_lines.Add(value);
	}

	public void SetEngine(ReadOnlySpan<char> chars)
	{
		(var engine, var hasClanFlag, var hasInnerSphereFlag, var size) = MtfHelper.GetEngine(chars);
		_lines.Add($"{size} {engine} {(hasClanFlag ? "(Clan)" : "")} {(hasInnerSphereFlag ? "(IS)" : "")}");
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetEra(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetGenerator(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetGenerator(chars);
		_lines.Add(value);
	}

	public void SetGyro(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetGyro(chars);
		_lines.Add(value.ToString());
	}

	public void SetHeatSinkKit(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetHeatSinkKit(chars);
		_lines.Add(value);
	}

	public void SetHeatSinks(ReadOnlySpan<char> chars)
	{
		(var count, var heatSink, var origin) = MtfHelper.GetHeatSinks(chars);
		_lines.Add($"{count} {origin} {heatSink}");
	}

	public void SetHistory(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetHistory(chars);
		_lines.Add(value);
	}

	public void SetImageFile(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetImageFile(chars);
		_lines.Add(value);
	}

	public void SetJumpMp(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetJumpMp(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetLam(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetLam(chars);
		_lines.Add(value.ToString());
	}

	public void SetManufacturer(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetManufacturer(chars);
		_lines.Add(value);
	}

	public void SetMass(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMass(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetModel(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetModel(chars);
		if (value is not null)
		{
			_lines.Add(value);
		}
	}

	public void SetMotive(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMotive(chars);
		_lines.Add(value.ToString());
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMulId(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMyomer(chars);
		_lines.Add(value.ToString());
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		(var location, var name) = MtfHelper.GetNoCrit(chars);
		_lines.Add($"{name}:{location}");
	}

	public void SetNotes(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetNotes(chars);
		_lines.Add(value);
	}

	public void SetOverview(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetOverview(chars);
		_lines.Add(value);
	}

	public void SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetPrimaryFactory(chars);
		_lines.Add(value);
	}

	public void SetRole(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetRole(chars);
		_lines.Add(value.ToString());
	}

	public void SetRulesLevel(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetRulesLevel(chars);
		_lines.Add(value.ToString());
	}

	public void SetSource(ReadOnlySpan<char> chars)
	{
		(var name, var type) = MtfHelper.GetSource(chars);
		_lines.Add((type is not null ? $"{type}:" : "") + name);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetStructure(chars);
		_lines.Add(value.ToString());
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		(var specificSystem, var name) = MtfHelper.GetSystemManufacturer(chars);
		_lines.Add($"{specificSystem}:{name}");
	}

	public void SetSystemModel(ReadOnlySpan<char> chars)
	{
		(var specificSystem, var name) = MtfHelper.GetSystemModel(chars);
		_lines.Add($"{specificSystem}:{name}");
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetTechBase(chars);
		_lines.Add(value.ToString());
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetWalkMp(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetWeaponListCount(chars);
		_lines.Add(value.ToString(CultureInfo.InvariantCulture));
	}
}
