using MechTools.Core;
using MechTools.Parsers.Extensions;
using System;
using System.Collections.Generic;

namespace MechTools.Parsers.BattleMech;

internal sealed class RawBattleMechBuilder : IBattleMechBuilder<List<string>>
{
	private readonly List<string> _lines = [];

	public void AddComment(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.AddComment(chars);
		_lines.Add(value);
	}

	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location)
	{
		var value = HelperExtensions.AddEquipmentAtLocation(chars);
		_lines.Add(value);
	}

	public void AddQuirk(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.AddQuirk(chars);
		_lines.Add(value);
	}

	public void AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		(var name, var location, var slot, var weapon) = HelperExtensions.AddWeaponQuirk(chars);
		_lines.Add($"{name}:{location}:{slot}:{weapon}");
	}

	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		(var count, var name, var location, var isRear, var ammo) = HelperExtensions.AddWeaponToWeaponList(chars);
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
		(var name, var value) = HelperExtensions.SetArmourAtLocation(chars);
		_lines.Add($"{location}{(name is not null ? $":{name}" : "")}:{value}");
	}

	public void SetArmourType(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetArmourType(chars);
		_lines.Add(value);
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetBaseChassisHeatSinks(chars);
		_lines.Add(value.ToString());
	}

	public void SetCapabilities(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetCapabilities(chars);
		_lines.Add(value);
	}

	public void SetChassis(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetChassis(chars);
		_lines.Add(value);
	}

	public void SetClanName(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetClanName(chars);
		_lines.Add(value);
	}

	public void SetCockpit(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetCockpit(chars);
		_lines.Add(value);
	}

	public void SetConfig(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetConfig(chars);
		_lines.Add(value);
	}

	public void SetDeployment(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetDeployment(chars);
		_lines.Add(value);
	}

	public void SetEjection(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetEjection(chars);
		_lines.Add(value);
	}

	public void SetEngine(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetEngine(chars);
		_lines.Add(value);
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetEra(chars);
		_lines.Add(value.ToString());
	}

	public void SetGenerator(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetGenerator(chars);
		_lines.Add(value);
	}

	public void SetGyro(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetGyro(chars);
		_lines.Add(value);
	}

	public void SetHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetHeatSinks(chars);
		_lines.Add(value);
	}

	public void SetHistory(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetHistory(chars);
		_lines.Add(value);
	}

	public void SetImageFile(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetImageFile(chars);
		_lines.Add(value);
	}

	public void SetJumpMp(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetJumpMp(chars);
		_lines.Add(value.ToString());
	}

	public void SetLam(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetLam(chars);
		_lines.Add(value);
	}

	public void SetManufacturer(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetManufacturer(chars);
		_lines.Add(value);
	}

	public void SetMass(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetMass(chars);
		_lines.Add(value.ToString());
	}

	public void SetModel(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetModel(chars);
		_lines.Add(value);
	}

	public void SetMotive(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetMotive(chars);
		_lines.Add(value);
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetMulId(chars);
		_lines.Add(value.ToString());
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetMyomer(chars);
		_lines.Add(value);
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		(var name, var value) = HelperExtensions.SetNoCrit(chars);
		_lines.Add($"{name}:{value}");
	}

	public void SetNotes(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetNotes(chars);
		_lines.Add(value);
	}

	public void SetOverview(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetOverview(chars);
		_lines.Add(value);
	}

	public void SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetPrimaryFactory(chars);
		_lines.Add(value);
	}

	public void SetRole(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetRole(chars);
		_lines.Add(value.ToString());
	}

	public void SetRulesLevel(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetRulesLevel(chars);
		_lines.Add(value.ToString());
	}

	public void SetSource(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetSource(chars);
		_lines.Add(value);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetStructure(chars);
		_lines.Add(value);
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		(var part, var name) = HelperExtensions.SetSystemManufacturer(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetSystemMode(ReadOnlySpan<char> chars)
	{
		(var part, var name) = HelperExtensions.SetSystemMode(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetTechBase(chars);
		_lines.Add(value);
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetWalkMp(chars);
		_lines.Add(value.ToString());
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		var value = HelperExtensions.SetWeaponListCount(chars);
		_lines.Add(value.ToString());
	}
}
