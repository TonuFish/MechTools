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
		var value = ParserExtensions.AddComment(chars);
		_lines.Add(value);
	}

	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location)
	{
		var value = ParserExtensions.AddEquipmentAtLocation(chars);
		_lines.Add(value);
	}

	public void AddQuirk(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.AddQuirk(chars);
		_lines.Add(value);
	}

	public void AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		// TODO: Further parsing
		(var name, var location, var slot, var weapon) = ParserExtensions.AddWeaponQuirk(chars);
		_lines.Add($"{name}:{location}:{slot}:{weapon}");
	}

	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		(var weapon, var location) = ParserExtensions.AddWeaponToWeaponList(chars);
		_lines.Add($"{weapon}, {location}");
	}

	public List<string> Build()
	{
		// TODO: Check if this is actually better than new(_lines) at runtime; may skip the typeof optimisation.
		return [.. _lines];
	}

	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location)
	{
		var value = ParserExtensions.SetArmourAtLocation(chars);
		_lines.Add($"{location}:{value}");
	}

	public void SetArmourType(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetArmourType(chars);
		_lines.Add(value);
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetBaseChassisHeatSinks(chars);
		_lines.Add(value.ToString());
	}

	public void SetCapabilities(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetCapabilities(chars);
		_lines.Add(value);
	}

	public void SetChassis(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetChassis(chars);
		_lines.Add(value);
	}

	public void SetClanName(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetClanName(chars);
		_lines.Add(value);
	}

	public void SetCockpit(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetCockpit(chars);
		_lines.Add(value);
	}

	public void SetConfig(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetConfig(chars);
		_lines.Add(value);
	}

	public void SetDeployment(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetDeployment(chars);
		_lines.Add(value);
	}

	public void SetEjection(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetEjection(chars);
		_lines.Add(value);
	}

	public void SetEngine(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetEngine(chars);
		_lines.Add(value);
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetEra(chars);
		_lines.Add(value.ToString());
	}

	public void SetGenerator(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetGenerator(chars);
		_lines.Add(value);
	}

	public void SetGyro(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetGyro(chars);
		_lines.Add(value);
	}

	public void SetHeatSinks(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetHeatSinks(chars);
		_lines.Add(value);
	}

	public void SetHistory(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetHistory(chars);
		_lines.Add(value);
	}

	public void SetImageFile(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetImageFile(chars);
		_lines.Add(value);
	}

	public void SetJumpMp(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetJumpMp(chars);
		_lines.Add(value.ToString());
	}

	public void SetManufacturer(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetManufacturer(chars);
		_lines.Add(value);
	}

	public void SetMass(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetMass(chars);
		_lines.Add(value.ToString());
	}

	public void SetModel(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetModel(chars);
		_lines.Add(value);
	}

	public void SetMotive(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetMotive(chars);
		_lines.Add(value);
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetMulId(chars);
		_lines.Add(value.ToString());
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetMyomer(chars);
		_lines.Add(value);
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		(var name, var value) = ParserExtensions.SetNoCrit(chars);
		_lines.Add($"{name}:{value}");
	}

	public void SetNotes(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetNotes(chars);
		_lines.Add(value);
	}

	public void SetOverview(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetOverview(chars);
		_lines.Add(value);
	}

	public void SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetPrimaryFactory(chars);
		_lines.Add(value);
	}

	public void SetRole(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetRole(chars);
		_lines.Add(value.ToString());
	}

	public void SetRulesLevel(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetRulesLevel(chars);
		_lines.Add(value.ToString());
	}

	public void SetSource(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetSource(chars);
		_lines.Add(value);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetStructure(chars);
		_lines.Add(value);
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		(var part, var name) = ParserExtensions.SetSystemManufacturer(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetSystemMode(ReadOnlySpan<char> chars)
	{
		(var part, var name) = ParserExtensions.SetSystemMode(chars);
		_lines.Add($"{part}:{name}");
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetTechBase(chars);
		_lines.Add(value);
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetWalkMp(chars);
		_lines.Add(value.ToString());
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		var value = ParserExtensions.SetWeaponListCount(chars);
		_lines.Add(value.ToString());
	}
}
