using MechTools.Core.Enums;
using MechTools.Parsers.Helpers;
using System;
using System.Collections.Generic;
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
		(var name, var isOmniPod, var isRear, var isTurret) = MtfHelper.GetEquipmentAtLocation(chars);

		if (!isOmniPod && !isRear && !isTurret)
		{
			_lines.Add(name);
			return;
		}

		StringBuilder sb = new(value: name, capacity: name.Length + 18);
		if (isRear)
		{
			sb.Append(" (R)");
		}
		if (isTurret)
		{
			sb.Append(" (T)");
		}
		if (isOmniPod)
		{
			sb.Append(" (OMNIPOD)");
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
		(var ammo, var count, var location, var name, var isRear) = MtfHelper.GetWeaponForWeaponList(chars);
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

	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location)
	{
		(var name, var value) = MtfHelper.GetArmourAtLocation(chars);
		_lines.Add($"{location}{(name is not null ? $":{name}" : "")}:{value}");
	}

	public void SetArmourType(ReadOnlySpan<char> chars)
	{
		(var armour, var origin) = MtfHelper.GetArmourType(chars);
		_lines.Add($"{armour}{(origin.HasValue ? $" ({origin})" : "")}");
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetBaseChassisHeatSinks(chars);
		_lines.Add(value.ToString());
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
		var value = MtfHelper.GetEngine(chars);
		_lines.Add(value);
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetEra(chars);
		_lines.Add(value.ToString());
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
		var value = MtfHelper.GetHeatSinks(chars);
		_lines.Add(value);
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
		_lines.Add(value.ToString());
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
		_lines.Add(value.ToString());
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
		_lines.Add(value);
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMulId(chars);
		_lines.Add(value.ToString());
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetMyomer(chars);
		_lines.Add(value.ToString());
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		(var name, var value) = MtfHelper.GetNoCrit(chars);
		_lines.Add($"{name}:{value}");
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
		(var type, var name) = MtfHelper.GetSource(chars);
		_lines.Add((type is not null ? $"{type}:" : "") + name);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetStructure(chars);
		_lines.Add(value);
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		(var part, var name) = MtfHelper.GetSystemManufacturer(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetSystemMode(ReadOnlySpan<char> chars)
	{
		(var part, var name) = MtfHelper.GetSystemMode(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetTechBase(chars);
		_lines.Add(value);
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetWalkMp(chars);
		_lines.Add(value.ToString());
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		var value = MtfHelper.GetWeaponListCount(chars);
		_lines.Add(value.ToString());
	}
}
