using MechTools.Parsers.Enums;
using MechTools.Parsers.Mtf;
using System;

namespace MechTools.Parsers.BattleMech;

public sealed class DefaultBattleMechBuilder : IBattleMechBuilder<DefaultBattleMech>
{
	private readonly DefaultBattleMech _mech = new();

	public void AddComment(ReadOnlySpan<char> chars)
	{
		_mech.Comments.Add(MtfHelpers.GetComment(chars));
	}

	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location)
	{
		_mech.EquipmentAtLocation[location] = MtfHelpers.GetEquipmentAtLocation(chars);
	}

	public void AddQuirk(ReadOnlySpan<char> chars)
	{
		_mech.Quirks.Add(MtfHelpers.GetQuirk(chars));
	}

	public void AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		_mech.WeaponQuirks.Add(MtfHelpers.GetWeaponQuirk(chars));
	}

	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		_mech.WeaponList.Add(MtfHelpers.GetWeaponForWeaponList(chars));
	}

	public DefaultBattleMech Build()
	{
		return _mech;
	}

	public void SetArmour(ReadOnlySpan<char> chars)
	{
		_mech.Armour = MtfHelpers.GetArmour(chars);
	}

	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location)
	{
		_mech.ArmourAtLocation[location] = MtfHelpers.GetArmourAtLocation(chars);
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		_mech.BaseChassisHeatSinks = MtfHelpers.GetBaseChassisHeatSinks(chars);
	}

	public void SetCapabilities(ReadOnlySpan<char> chars)
	{
		_mech.Capabilities = MtfHelpers.GetCapabilities(chars);
	}

	public void SetChassis(ReadOnlySpan<char> chars)
	{
		_mech.Chassis = MtfHelpers.GetChassis(chars);
	}

	public void SetClanName(ReadOnlySpan<char> chars)
	{
		_mech.ClanName = MtfHelpers.GetClanName(chars);
	}

	public void SetCockpit(ReadOnlySpan<char> chars)
	{
		_mech.Cockpit = MtfHelpers.GetCockpit(chars);
	}

	public void SetConfiguration(ReadOnlySpan<char> chars)
	{
		_mech.Configuration = MtfHelpers.GetConfiguration(chars);
	}

	public void SetDeployment(ReadOnlySpan<char> chars)
	{
		_mech.Deployment = MtfHelpers.GetDeployment(chars);
	}

	public void SetEjection(ReadOnlySpan<char> chars)
	{
		_mech.Ejection = MtfHelpers.GetEjection(chars);
	}

	public void SetEngine(ReadOnlySpan<char> chars)
	{
		_mech.Engine = MtfHelpers.GetEngine(chars);
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		_mech.Era = MtfHelpers.GetEra(chars);
	}

	public void SetGenerator(ReadOnlySpan<char> chars)
	{
		_mech.Generator = MtfHelpers.GetGenerator(chars);
	}

	public void SetGyro(ReadOnlySpan<char> chars)
	{
		_mech.Gyro = MtfHelpers.GetGyro(chars);
	}

	public void SetHeatSinkKit(ReadOnlySpan<char> chars)
	{
		_mech.HeatSinkKit = MtfHelpers.GetHeatSinkKit(chars);
	}

	public void SetHeatSinks(ReadOnlySpan<char> chars)
	{
		_mech.HeatSinks = MtfHelpers.GetHeatSinks(chars);
	}

	public void SetHistory(ReadOnlySpan<char> chars)
	{
		_mech.History = MtfHelpers.GetHistory(chars);
	}

	public void SetImageFile(ReadOnlySpan<char> chars)
	{
		_mech.ImageFile = MtfHelpers.GetImageFile(chars);
	}

	public void SetJumpMp(ReadOnlySpan<char> chars)
	{
		_mech.JumpMp = MtfHelpers.GetJumpMp(chars);
	}

	public void SetLam(ReadOnlySpan<char> chars)
	{
		_mech.Lam = MtfHelpers.GetLam(chars);
	}

	public void SetManufacturer(ReadOnlySpan<char> chars)
	{
		_mech.Manufacturer = MtfHelpers.GetManufacturer(chars);
	}

	public void SetMass(ReadOnlySpan<char> chars)
	{
		_mech.Mass = MtfHelpers.GetMass(chars);
	}

	public void SetModel(ReadOnlySpan<char> chars)
	{
		_mech.Model = MtfHelpers.GetModel(chars);
	}

	public void SetMotive(ReadOnlySpan<char> chars)
	{
		_mech.Motive = MtfHelpers.GetMotive(chars);
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		_mech.MulId = MtfHelpers.GetMulId(chars);
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		_mech.Myomer = MtfHelpers.GetMyomer(chars);
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		_mech.NoCrits.Add(MtfHelpers.GetNoCrit(chars));
	}

	public void SetNotes(ReadOnlySpan<char> chars)
	{
		_mech.Notes = MtfHelpers.GetNotes(chars);
	}

	public void SetOverview(ReadOnlySpan<char> chars)
	{
		_mech.Overview = MtfHelpers.GetOverview(chars);
	}

	public void SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		_mech.PrimaryFactory = MtfHelpers.GetPrimaryFactory(chars);
	}

	public void SetRole(ReadOnlySpan<char> chars)
	{
		_mech.Role = MtfHelpers.GetRole(chars);
	}

	public void SetRulesLevel(ReadOnlySpan<char> chars)
	{
		_mech.RulesLevel = MtfHelpers.GetRulesLevel(chars);
	}

	public void SetSource(ReadOnlySpan<char> chars)
	{
		_mech.Source = MtfHelpers.GetSource(chars);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		_mech.Structure = MtfHelpers.GetStructure(chars);
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		_mech.SystemManufacturers.Add(MtfHelpers.GetSystemManufacturer(chars));
	}

	public void SetSystemModel(ReadOnlySpan<char> chars)
	{
		_mech.SystemModels.Add(MtfHelpers.GetSystemModel(chars));
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		_mech.TechBase = MtfHelpers.GetTechBase(chars);
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		_mech.WalkMp = MtfHelpers.GetWalkMp(chars);
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		_mech.WeaponListCount = MtfHelpers.GetWeaponListCount(chars);
	}
}
