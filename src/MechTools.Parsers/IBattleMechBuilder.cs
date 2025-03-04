using MechTools.Core;
using System;

namespace MechTools.Parsers;

public interface IBattleMechBuilder
{
	public void AddComment(ReadOnlySpan<char> chars);
	public void AddEquipmentAtLocation(ReadOnlySpan<char> chars, BattleMechEquipmentLocation location);
	public void AddQuirk(ReadOnlySpan<char> chars);
	public void AddWeaponQuirk(ReadOnlySpan<char> chars);
	public void AddWeaponToWeaponList(ReadOnlySpan<char> chars);
	public void SetArmourType(ReadOnlySpan<char> chars);
	public void SetArmourAtLocation(ReadOnlySpan<char> chars, BattleMechArmourLocation location);
	public void SetBaseChassisHeatSinks(ReadOnlySpan<char> chars);
	public void SetCapabilities(ReadOnlySpan<char> chars);
	public void SetChassis(ReadOnlySpan<char> chars);
	public void SetClanName(ReadOnlySpan<char> chars);
	public void SetCockpit(ReadOnlySpan<char> chars);
	public void SetConfiguration(ReadOnlySpan<char> chars);
	public void SetDeployment(ReadOnlySpan<char> chars);
	public void SetEjection(ReadOnlySpan<char> chars);
	public void SetEngine(ReadOnlySpan<char> chars);
	public void SetEra(ReadOnlySpan<char> chars);
	public void SetGenerator(ReadOnlySpan<char> chars);
	public void SetGyro(ReadOnlySpan<char> chars);
	public void SetHeatSinks(ReadOnlySpan<char> chars);
	public void SetHistory(ReadOnlySpan<char> chars);
	public void SetImageFile(ReadOnlySpan<char> chars);
	public void SetJumpMp(ReadOnlySpan<char> chars);
	public void SetLam(ReadOnlySpan<char> chars);
	public void SetManufacturer(ReadOnlySpan<char> chars);
	public void SetMass(ReadOnlySpan<char> chars);
	public void SetModel(ReadOnlySpan<char> chars);
	public void SetMotive(ReadOnlySpan<char> chars);
	public void SetMulId(ReadOnlySpan<char> chars);
	public void SetMyomer(ReadOnlySpan<char> chars);
	public void SetNoCrit(ReadOnlySpan<char> chars);
	public void SetNotes(ReadOnlySpan<char> chars);
	public void SetOverview(ReadOnlySpan<char> chars);
	public void SetPrimaryFactory(ReadOnlySpan<char> chars);
	public void SetRole(ReadOnlySpan<char> chars);
	public void SetRulesLevel(ReadOnlySpan<char> chars);
	public void SetSource(ReadOnlySpan<char> chars);
	public void SetStructure(ReadOnlySpan<char> chars);
	public void SetSystemManufacturer(ReadOnlySpan<char> chars);
	public void SetSystemMode(ReadOnlySpan<char> chars);
	public void SetTechBase(ReadOnlySpan<char> chars);
	public void SetWalkMp(ReadOnlySpan<char> chars);
	public void SetWeaponListCount(ReadOnlySpan<char> chars);
}
