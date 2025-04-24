using MechTools.Core.Enums;
using MechTools.Parsers.BattleMech;
using MechTools.Parsers.Helpers;
using System;

namespace MechTools.UnitTests;

public sealed class HelperTests
{
	// TODO: GetEquipmentAtLocation
	// TODO: GetWeaponForWeaponList

	[Theory]
	[MemberData(nameof(TestData.InvalidArmour), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetArmour_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetArmour(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidArmour), MemberType = typeof(TestData))]
	public void GetArmour_ValidInput_Works(string input, ArmourData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetArmour(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidArmourAtLocation), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetArmourAtLocation_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetArmourAtLocation(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidArmourAtLocation), MemberType = typeof(TestData))]
	public void GetArmourAtLocation_ValidInput_Works(string input, LocationArmourData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetArmourAtLocation(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetBaseChassisHeatSinks_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetBaseChassisHeatSinks(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 10 ", 10)]
	public void GetBaseChassisHeatSinks_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetBaseChassisHeatSinks(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetCapabilities_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetCapabilities(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Capabilities text here. ", "Capabilities text here.")]
	public void GetCapabilities_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetCapabilities(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetChassis_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetChassis(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Crab ", "Crab")]
	[InlineData(" Man O' War ", "Man O' War")]
	public void GetChassis_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetChassis(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetClanName_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetClanName(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Stone Rhino ", "Stone Rhino")]
	[InlineData(" Mad Dog Mk IV ", "Mad Dog Mk IV")]
	public void GetClanName_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetClanName(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetCockpit_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetCockpit(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Standard Cockpit ", Cockpit.StandardCockpit)]
	[InlineData(" Small ", Cockpit.SmallCockpit)]
	public void GetCockpit_ValidInput_Works(string input, Cockpit expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetCockpit(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetComment_AnyInput_Works(string? input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetComment(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetConfiguration_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetConfiguration(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidConfiguration), MemberType = typeof(TestData))]
	public void GetConfiguration_ValidInput_Works(string input, ConfigurationData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetConfiguration(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetDeployment_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetDeployment(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Deployment text here. ", "Deployment text here.")]
	public void GetDeployment_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetDeployment(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEjection_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetEjection(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Full Head Ejection System ", "Full Head Ejection System")]
	public void GetEjection_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetEjection(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidEngine), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEngine_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetEngine(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidEngine), MemberType = typeof(TestData))]
	public void GetEngine_ValidInput_Works(string input, EngineData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetEngine(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetEra_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetEra(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 2719 ", 2719)]
	public void GetEra_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetEra(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetGenerator_AnyInput_Works(string? input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetGenerator(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetGyro_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetGyro(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" standard gyro ", Gyro.Standard)]
	public void GetGyro_ValidInput_Works(string input, Gyro expected)
	{
		// TODO: Consider if the ` gyro` suffix should be optional.

		// Arrange
		// Act
		var result = MtfHelper.GetGyro(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHeatSinkKit_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetHeatSinkKit(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" RISC Heat Sink Override Kit ", "RISC Heat Sink Override Kit")]
	public void GetHeatSinkKit_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetHeatSinkKit(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidHeatSinks), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHeatSinks_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetHeatSinks(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidHeatSinks), MemberType = typeof(TestData))]
	public void GetHeatSinks_ValidInput_Works(string input, HeatSinkData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetHeatSinks(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetHistory_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetHistory(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" History text here. ", "History text here.")]
	public void GetHistory_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetHistory(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.AllowAnyTextStrings), MemberType = typeof(TestData))]
	public void GetImageFile_AnyInput_Works(string? input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetImageFile(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetJumpMp_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetJumpMp(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 0 ", 0)]
	[InlineData(" 1 ", 1)]
	public void GetJumpMp_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetJumpMp(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetLam_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetLam(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" standard ", Lam.Standard)]
	public void GetLam_ValidInput_Works(string input, Lam expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetLam(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetManufacturer_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetManufacturer(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Cosara Weaponries ", "Cosara Weaponries")]
	public void GetManufacturer_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetManufacturer(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData(" 0 ")]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMass_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetMass(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 50 ", 50)]
	public void GetMass_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetMass(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetModel_EmptyInput_ReturnsNull(string input)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetModel(input);

		// Assert
		result.ShouldBeNull();
	}

	[Theory]
	[InlineData(" CRB-27b ", "CRB-27b")]
	[InlineData(" AGT-UA 'Ariel' ", "AGT-UA 'Ariel'")]
	public void GetModel_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetModel(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMotive_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetMotive(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Wheel ", Motive.Wheel)]
	public void GetMotive_ValidInput_Works(string input, Motive expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetMotive(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMulId_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetMulId(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 717 ", 717)]
	public void GetMulId_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetMulId(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.KnownLegacyMyomerStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetMyomer_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetMyomer(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Standard ", Myomer.Standard)]
	public void GetMyomer_ValidInput_Works(string input, Myomer expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetMyomer(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidNoCrit), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetNoCrit_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetNoCrit(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidNoCrit), MemberType = typeof(TestData))]
	public void GetNoCrit_ValidInput_Works(string input, NoCritData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetNoCrit(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetNotes_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetNotes(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Notes text here. ", "Notes text here.")]
	public void GetNotes_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetNotes(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetOverview_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetOverview(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Overview text here. ", "Overview text here.")]
	public void GetOverview_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetOverview(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetPrimaryFactory_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetPrimaryFactory(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Northwind ", "Northwind")]
	public void GetPrimaryFactory_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetPrimaryFactory(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetQuirk_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetQuirk(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" easy_maintain ", "easy_maintain")]
	public void GetQuirk_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetQuirk(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetRole_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetRole(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" sniper ", Role.Sniper)]
	public void GetRole_ValidInput_Works(string input, Role expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetRole(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetRulesLevel_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetRulesLevel(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 2 ", RulesLevel.Standard)]
	public void GetRulesLevel_ValidInput_Works(string input, RulesLevel expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetRulesLevel(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSource_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetSource(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSource), MemberType = typeof(TestData))]
	public void GetSource_ValidInput_Works(string input, SourceData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetSource(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetStructure_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetStructure(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Standard ", Structure.Standard)]
	[InlineData(" IS Composite ", Structure.Composite)]
	[InlineData(" Clan Endo Steel ", Structure.EndoSteel)]
	public void GetStructure_ValidInput_Works(string input, Structure expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetStructure(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidSpecificSystemData), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSystemManufacturer_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetSystemManufacturer(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSpecificSystemData), MemberType = typeof(TestData))]
	public void GetSystemManufacturer_ValidInput_Works(string input, SpecificSystemData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetSystemManufacturer(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidSpecificSystemData), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetSystemModel_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetSystemModel(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidSpecificSystemData), MemberType = typeof(TestData))]
	public void GetSystemModel_ValidInput_Works(string input, SpecificSystemData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetSystemModel(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetTechBase_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetTechBase(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Inner Sphere ", TechBase.InnerSphere)]
	public void GetTechBase_ValidInput_Works(string input, TechBase expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetTechBase(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData(" 0 ")]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWalkMp_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetWalkMp(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 1 ", 1)]
	public void GetWalkMp_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetWalkMp(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.NotSimplePositiveNumberStrings), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWeaponListCount_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetWeaponListCount(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" 4 ", 4)]
	public void GetWeaponListCount_ValidInput_Works(string input, int expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetWeaponListCount(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.InvalidWeaponQuirks), MemberType = typeof(TestData))]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void GetWeaponQuirk_InvalidInput_Throws(string input)
	{
		// Arrange
		Action action = () => MtfHelper.GetWeaponQuirk(input);

		// Act
		// Assert
		_ = action.ShouldThrow<Exception>();
	}

	[Theory]
	[MemberData(nameof(TestData.ValidWeaponQuirks), MemberType = typeof(TestData))]
	public void GetWeaponQuirk_ValidInput_Works(string input, WeaponQuirkData expected)
	{
		// Arrange
		// Act
		var result = MtfHelper.GetWeaponQuirk(input);

		// Assert
		result.ShouldBe(expected);
	}

	#region Get Equipment

	[Fact]
	public void GetEquipmentAtLocation_CachedInput_ReturnsCachedValue()
	{
		// Arrange
		const string input = " -empty- ";
		const string cacheKey = "-Empty-";
		_ = MtfValues.Lookup.CommonEquipmentValues.TryGetValue(cacheKey, out var cachedValue);
		EquipmentData expected = new(false, false, false, cachedValue!);

		// Act
		var result = MtfHelper.GetEquipmentAtLocation(input);

		// Assert
		result.ShouldBe(expected);
	}

	#endregion Get Equipment
}
