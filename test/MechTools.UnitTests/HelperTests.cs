using MechTools.Core;
using MechTools.Parsers.BattleMech;
using MechTools.Parsers.Helpers;
using System;

namespace MechTools.UnitTests;

public sealed class HelperTests
{

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
	// TODO: Expand on all the random splattering here.

	[Theory]
	[InlineData(null, "")]
	[InlineData("   ", "   ")]
	[InlineData(" This is a comment. ", " This is a comment. ")]
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
	[InlineData(" QuadVee ", Configuration.QuadVee, false)]
	[InlineData(" Biped Omnimech ", Configuration.Biped, true)]
	[InlineData(" Biped Omnimek ", Configuration.Biped, true)]
	public void GetConfiguration_ValidInput_Works(string input, Configuration expectedConfiguration, bool expectedIsOmniMech)
	{
		// Arrange
		// Act
		(var configuration, var isOmniMech) = MtfHelper.GetConfiguration(input);

		// Assert
		configuration.ShouldBe(expectedConfiguration);
		isOmniMech.ShouldBe(expectedIsOmniMech);
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
	[InlineData(" Rec Guide:ilClan #24 ", "Rec Guide", "ilClan #24")]
	[InlineData(" Battle of Tukayyid ", null, "Battle of Tukayyid")]
	[InlineData(" TRO : 3067 ", "TRO", "3067")]
	public void GetSource_ValidInput_Works(string input, string? expectedType, string expectedName)
	{
		// Arrange
		// Act
		(var type, var name) = MtfHelper.GetSource(input);

		// Assert
		type.ShouldBe(expectedType);
		name.ShouldBe(expectedName);
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

		// Act
		(var name, var isOmniPod, var isRear, var isTurret) = MtfHelper.GetEquipmentAtLocation(input);

		// Assert
		name.ShouldBeSameAs(cachedValue);
		isOmniPod.ShouldBeFalse();
		isRear.ShouldBeFalse();
		isTurret.ShouldBeFalse();
	}

	#endregion Get Equipment
}
