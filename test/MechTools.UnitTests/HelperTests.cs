using MechTools.Parsers.Data;
using MechTools.Parsers.Enums;
using MechTools.Parsers.Mtf;
using System;

namespace MechTools.UnitTests;

public sealed class HelperTests
{
	[Theory]
	[MemberData(nameof(TestData.InvalidArmour), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetArmour_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetArmour(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidArmour), MemberType = typeof(TestData))]
	public void GetArmour_ValidInput_Works(string input, ArmourData expected)
	{
		var result = MtfHelpers.GetArmour(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidArmourAtLocation), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetArmourAtLocation_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetArmourAtLocation(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidArmourAtLocation), MemberType = typeof(TestData))]
	public void GetArmourAtLocation_ValidInput_Works(string input, LocationArmourData expected)
	{
		var result = MtfHelpers.GetArmourAtLocation(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetBaseChassisHeatSinks_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetBaseChassisHeatSinks(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 10 ", 10)]
	public void GetBaseChassisHeatSinks_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetBaseChassisHeatSinks(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetCapabilities_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetCapabilities(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Capabilities text here. ", "Capabilities text here.")]
	public void GetCapabilities_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetCapabilities(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetChassis_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetChassis(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Crab ", "Crab")]
	[InlineData(" Man O' War ", "Man O' War")]
	public void GetChassis_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetChassis(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetClanName_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetClanName(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Stone Rhino ", "Stone Rhino")]
	[InlineData(" Mad Dog Mk IV ", "Mad Dog Mk IV")]
	public void GetClanName_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetClanName(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetCockpit_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetCockpit(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Standard Cockpit ", Cockpit.StandardCockpit)]
	[InlineData(" Small ", Cockpit.SmallCockpit)]
	public void GetCockpit_ValidInput_Works(string input, Cockpit expected)
	{
		var result = MtfHelpers.GetCockpit(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetComment_AnyInput_Works(string? input, string expected)
	{
		var result = MtfHelpers.GetComment(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetConfiguration_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetConfiguration(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidConfiguration), MemberType = typeof(TestData))]
	public void GetConfiguration_ValidInput_Works(string input, ConfigurationData expected)
	{
		var result = MtfHelpers.GetConfiguration(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetDeployment_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetDeployment(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Deployment text here. ", "Deployment text here.")]
	public void GetDeployment_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetDeployment(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEjection_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetEjection(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Full Head Ejection System ", "Full Head Ejection System")]
	public void GetEjection_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetEjection(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidEngine), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEngine_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetEngine(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidEngine), MemberType = typeof(TestData))]
	public void GetEngine_ValidInput_Works(string input, EngineData expected)
	{
		var result = MtfHelpers.GetEngine(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidEquipmentAtLocation), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEquipmentAtLocation_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetEquipmentAtLocation(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidEquipmentAtLocation), MemberType = typeof(TestData))]
	public void GetEquipmentAtLocation_ValidInput_Works(string input, EquipmentData expected)
	{
		var result = MtfHelpers.GetEquipmentAtLocation(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEra_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetEra(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 2719 ", 2719)]
	public void GetEra_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetEra(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetGenerator_AnyInput_Works(string? input, string expected)
	{
		var result = MtfHelpers.GetGenerator(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetGyro_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetGyro(input);
		action.ShouldThrow<MtfEnumException>().EnumType.ShouldBe(typeof(Gyro));
	}

	[Theory]
	[InlineData(" standard gyro ", Gyro.Standard)]
	public void GetGyro_ValidInput_Works(string input, Gyro expected)
	{
		var result = MtfHelpers.GetGyro(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHeatSinkKit_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetHeatSinkKit(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" RISC Heat Sink Override Kit ", "RISC Heat Sink Override Kit")]
	public void GetHeatSinkKit_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetHeatSinkKit(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidHeatSinks), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHeatSinks_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetHeatSinks(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidHeatSinks), MemberType = typeof(TestData))]
	public void GetHeatSinks_ValidInput_Works(string input, HeatSinkData expected)
	{
		var result = MtfHelpers.GetHeatSinks(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHistory_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetHistory(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" History text here. ", "History text here.")]
	public void GetHistory_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetHistory(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetImageFile_AnyInput_Works(string? input, string expected)
	{
		var result = MtfHelpers.GetImageFile(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetJumpMp_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetJumpMp(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 0 ", 0)]
	[InlineData(" 1 ", 1)]
	public void GetJumpMp_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetJumpMp(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetLam_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetLam(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" standard ", Lam.Standard)]
	public void GetLam_ValidInput_Works(string input, Lam expected)
	{
		var result = MtfHelpers.GetLam(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetManufacturer_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetManufacturer(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Cosara Weaponries ", "Cosara Weaponries")]
	public void GetManufacturer_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetManufacturer(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData(" 0 ")]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMass_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetMass(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 50 ", 50)]
	public void GetMass_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetMass(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetModel_EmptyInput_ReturnsNull(string input)
	{
		var result = MtfHelpers.GetModel(input);
		result.ShouldBeNull();
	}

	[Theory]
	[InlineData(" CRB-27b ", "CRB-27b")]
	[InlineData(" AGT-UA 'Ariel' ", "AGT-UA 'Ariel'")]
	public void GetModel_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetModel(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMotive_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetMotive(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Wheel ", Motive.Wheel)]
	public void GetMotive_ValidInput_Works(string input, Motive expected)
	{
		var result = MtfHelpers.GetMotive(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMulId_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetMulId(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 717 ", 717)]
	public void GetMulId_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetMulId(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMyomer_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetMyomer(input);
		action.ShouldThrow<MtfEnumException>().EnumType.ShouldBe(typeof(Myomer));
	}

	[Theory]
	[InlineData(" Standard ", Myomer.Standard)]
	[MemberData(nameof(TestData.KnownLegacyMyomerStrings), MemberType = typeof(TestData))]
	public void GetMyomer_ValidInput_Works(string input, Myomer expected)
	{
		var result = MtfHelpers.GetMyomer(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidNoCrit), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetNoCrit_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetNoCrit(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidNoCrit), MemberType = typeof(TestData))]
	public void GetNoCrit_ValidInput_Works(string input, NoCritData expected)
	{
		var result = MtfHelpers.GetNoCrit(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetNotes_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetNotes(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Notes text here. ", "Notes text here.")]
	public void GetNotes_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetNotes(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetOverview_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetOverview(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Overview text here. ", "Overview text here.")]
	public void GetOverview_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetOverview(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetPrimaryFactory_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetPrimaryFactory(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Northwind ", "Northwind")]
	public void GetPrimaryFactory_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetPrimaryFactory(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetQuirk_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetQuirk(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" easy_maintain ", "easy_maintain")]
	public void GetQuirk_ValidInput_Works(string input, string expected)
	{
		var result = MtfHelpers.GetQuirk(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetRole_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetRole(input);
		action.ShouldThrow<MtfEnumException>().EnumType.ShouldBe(typeof(Role));
	}

	[Theory]
	[InlineData(" sniper ", Role.Sniper)]
	public void GetRole_ValidInput_Works(string input, Role expected)
	{
		var result = MtfHelpers.GetRole(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetRulesLevel_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetRulesLevel(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 2 ", RulesLevel.Standard)]
	public void GetRulesLevel_ValidInput_Works(string input, RulesLevel expected)
	{
		var result = MtfHelpers.GetRulesLevel(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSource_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetSource(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSource), MemberType = typeof(TestData))]
	public void GetSource_ValidInput_Works(string input, SourceData expected)
	{
		var result = MtfHelpers.GetSource(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetStructure_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetStructure(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Standard ", Structure.Standard)]
	[InlineData(" IS Composite ", Structure.Composite)]
	[InlineData(" Clan Endo Steel ", Structure.EndoSteel)]
	public void GetStructure_ValidInput_Works(string input, Structure expected)
	{
		var result = MtfHelpers.GetStructure(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidSpecificSystemData), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSystemManufacturer_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetSystemManufacturer(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSpecificSystemData), MemberType = typeof(TestData))]
	public void GetSystemManufacturer_ValidInput_Works(string input, SpecificSystemData expected)
	{
		var result = MtfHelpers.GetSystemManufacturer(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidSpecificSystemData), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSystemModel_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetSystemModel(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSpecificSystemData), MemberType = typeof(TestData))]
	public void GetSystemModel_ValidInput_Works(string input, SpecificSystemData expected)
	{
		var result = MtfHelpers.GetSystemModel(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetTechBase_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetTechBase(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" Inner Sphere ", TechBase.InnerSphere)]
	public void GetTechBase_ValidInput_Works(string input, TechBase expected)
	{
		var result = MtfHelpers.GetTechBase(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData(" 0 ")]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWalkMp_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetWalkMp(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 1 ", 1)]
	public void GetWalkMp_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetWalkMp(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidWeaponForWeaponList), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWeaponForWeaponList_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetWeaponForWeaponList(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidWeaponForWeaponList), MemberType = typeof(TestData))]
	public void GetWeaponForWeaponList_ValidInput_Works(string input, WeaponListData expected)
	{
		var result = MtfHelpers.GetWeaponForWeaponList(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWeaponListCount_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetWeaponListCount(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[InlineData(" 4 ", 4)]
	public void GetWeaponListCount_ValidInput_Works(string input, int expected)
	{
		var result = MtfHelpers.GetWeaponListCount(input);
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidWeaponQuirks), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWeaponQuirk_InvalidInput_Throws(string input)
	{
		Action action = () => MtfHelpers.GetWeaponQuirk(input);
		_ = action.ShouldThrow<MtfException>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidWeaponQuirks), MemberType = typeof(TestData))]
	public void GetWeaponQuirk_ValidInput_Works(string input, WeaponQuirkData expected)
	{
		var result = MtfHelpers.GetWeaponQuirk(input);
		result.ShouldBe(expected);
	}

	#region Caching

	[Fact]
	public void GetEquipmentAtLocation_CachedInput_ReturnsCachedValue()
	{
		const string input = " -empty- ";
		const string cacheKey = "-Empty-";
		_ = CommonValues.EquipmentLookup.TryGetValue(cacheKey, out var cachedValue);
		EquipmentData expected = new(false, false, false, cachedValue!);
		var result = MtfHelpers.GetEquipmentAtLocation(input);
		result.ShouldBe(expected);
	}

	#endregion Caching
}
