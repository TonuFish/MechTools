using MechTools.Parsers.Enums;
using MechTools.Parsers.Helpers;
using System;

namespace MechTools.Parsers.BattleMech;

public sealed class DefaultBattleMechBuilder : IBattleMechBuilder<DefaultBattleMech>
{
	private readonly DefaultBattleMech _mech = new();

	public void AddComment(ReadOnlySpan<char> chars)
	{
		_mech.Comments.Add(MtfHelper.GetComment(chars));
	}

	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location)
	{
		_mech.EquipmentAtLocation[location] = MtfHelper.GetEquipmentAtLocation(chars);
	}

	public void AddQuirk(ReadOnlySpan<char> chars)
	{
		_mech.Quirks.Add(MtfHelper.GetQuirk(chars));
	}

	public void AddWeaponQuirk(ReadOnlySpan<char> chars)
	{
		_mech.WeaponQuirks.Add(MtfHelper.GetWeaponQuirk(chars));
	}

	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars)
	{
		_mech.WeaponList.Add(MtfHelper.GetWeaponForWeaponList(chars));
	}

	public DefaultBattleMech Build()
	{
		return _mech;
	}

	public void SetArmour(ReadOnlySpan<char> chars)
	{
		_mech.Armour = MtfHelper.GetArmour(chars);
	}

	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location)
	{
		_mech.ArmourAtLocation[location] = MtfHelper.GetArmourAtLocation(chars);
	}

	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars)
	{
		_mech.BaseChassisHeatSinks = MtfHelper.GetBaseChassisHeatSinks(chars);
	}

	public void SetCapabilities(ReadOnlySpan<char> chars)
	{
		_mech.Capabilities = MtfHelper.GetCapabilities(chars);
	}

	public void SetChassis(ReadOnlySpan<char> chars)
	{
		_mech.Chassis = MtfHelper.GetChassis(chars);
	}

	public void SetClanName(ReadOnlySpan<char> chars)
	{
		_mech.ClanName = MtfHelper.GetClanName(chars);
	}

	public void SetCockpit(ReadOnlySpan<char> chars)
	{
		_mech.Cockpit = MtfHelper.GetCockpit(chars);
	}

	public void SetConfiguration(ReadOnlySpan<char> chars)
	{
		_mech.Configuration = MtfHelper.GetConfiguration(chars);
	}

	public void SetDeployment(ReadOnlySpan<char> chars)
	{
		_mech.Deployment = MtfHelper.GetDeployment(chars);
	}

	public void SetEjection(ReadOnlySpan<char> chars)
	{
		_mech.Ejection = MtfHelper.GetEjection(chars);
	}

	public void SetEngine(ReadOnlySpan<char> chars)
	{
		_mech.Engine = MtfHelper.GetEngine(chars);
	}

	public void SetEra(ReadOnlySpan<char> chars)
	{
		_mech.Era = MtfHelper.GetEra(chars);
	}

	public void SetGenerator(ReadOnlySpan<char> chars)
	{
		_mech.Generator = MtfHelper.GetGenerator(chars);
	}

	public void SetGyro(ReadOnlySpan<char> chars)
	{
		_mech.Gyro = MtfHelper.GetGyro(chars);
	}

	public void SetHeatSinkKit(ReadOnlySpan<char> chars)
	{
		_mech.HeatSinkKit = MtfHelper.GetHeatSinkKit(chars);
	}

	public void SetHeatSinks(ReadOnlySpan<char> chars)
	{
		_mech.HeatSinks = MtfHelper.GetHeatSinks(chars);
	}

	public void SetHistory(ReadOnlySpan<char> chars)
	{
		_mech.History = MtfHelper.GetHistory(chars);
	}

	public void SetImageFile(ReadOnlySpan<char> chars)
	{
		_mech.ImageFile = MtfHelper.GetImageFile(chars);
	}

	public void SetJumpMp(ReadOnlySpan<char> chars)
	{
		_mech.JumpMp = MtfHelper.GetJumpMp(chars);
	}

	public void SetLam(ReadOnlySpan<char> chars)
	{
		_mech.Lam = MtfHelper.GetLam(chars);
	}

	public void SetManufacturer(ReadOnlySpan<char> chars)
	{
		_mech.Manufacturer = MtfHelper.GetManufacturer(chars);
	}

	public void SetMass(ReadOnlySpan<char> chars)
	{
		_mech.Mass = MtfHelper.GetMass(chars);
	}

	public void SetModel(ReadOnlySpan<char> chars)
	{
		_mech.Model = MtfHelper.GetModel(chars);
	}

	public void SetMotive(ReadOnlySpan<char> chars)
	{
		_mech.Motive = MtfHelper.GetMotive(chars);
	}

	public void SetMulId(ReadOnlySpan<char> chars)
	{
		_mech.MulId = MtfHelper.GetMulId(chars);
	}

	public void SetMyomer(ReadOnlySpan<char> chars)
	{
		_mech.Myomer = MtfHelper.GetMyomer(chars);
	}

	public void SetNoCrit(ReadOnlySpan<char> chars)
	{
		_mech.NoCrits.Add(MtfHelper.GetNoCrit(chars));
	}

	public void SetNotes(ReadOnlySpan<char> chars)
	{
		_mech.Notes = MtfHelper.GetNotes(chars);
	}

	public void SetOverview(ReadOnlySpan<char> chars)
	{
		_mech.Overview = MtfHelper.GetOverview(chars);
	}

	public void SetPrimaryFactory(ReadOnlySpan<char> chars)
	{
		_mech.PrimaryFactory = MtfHelper.GetPrimaryFactory(chars);
	}

	public void SetRole(ReadOnlySpan<char> chars)
	{
		_mech.Role = MtfHelper.GetRole(chars);
	}

	public void SetRulesLevel(ReadOnlySpan<char> chars)
	{
		_mech.RulesLevel = MtfHelper.GetRulesLevel(chars);
	}

	public void SetSource(ReadOnlySpan<char> chars)
	{
		_mech.Source = MtfHelper.GetSource(chars);
	}

	public void SetStructure(ReadOnlySpan<char> chars)
	{
		_mech.Structure = MtfHelper.GetStructure(chars);
	}

	public void SetSystemManufacturer(ReadOnlySpan<char> chars)
	{
		_mech.SystemManufacturers.Add(MtfHelper.GetSystemManufacturer(chars));
	}

	public void SetSystemModel(ReadOnlySpan<char> chars)
	{
		_mech.SystemModels.Add(MtfHelper.GetSystemModel(chars));
	}

	public void SetTechBase(ReadOnlySpan<char> chars)
	{
		_mech.TechBase = MtfHelper.GetTechBase(chars);
	}

	public void SetWalkMp(ReadOnlySpan<char> chars)
	{
		_mech.WalkMp = MtfHelper.GetWalkMp(chars);
	}

	public void SetWeaponListCount(ReadOnlySpan<char> chars)
	{
		_mech.WeaponListCount = MtfHelper.GetWeaponListCount(chars);
	}
}
