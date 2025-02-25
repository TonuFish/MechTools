using MechTools.Core;
using MechTools.Parsers.BattleMech;
using MechTools.Parsers.Extensions;
using System;

namespace MechTools.UnitTests;

public sealed class HelperTests
{
	// TODO: Expand on all the random splattering here.

	[Theory]
	[InlineData(null, "")]
	[InlineData("   ", "   ")]
	[InlineData(" This is a comment. ", " This is a comment. ")]
	public void AddComment_AnyInput_Works(string? input, string expected)
	{
		// Arrange
		// Act
		var result = HelperExtensions.AddComment(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData(" Crab ", "Crab")]
	[InlineData(" Man O' War ", "Man O' War")]
	public void SetChassis_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = HelperExtensions.SetChassis(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetChassis_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetChassis(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Standard Cockpit ", Cockpit.StandardCockpit)]
	[InlineData(" Small ", Cockpit.SmallCockpit)]
	public void SetCockpit_ValidInput_Works(string input, Cockpit expected)
	{
		// Arrange
		// Act
		var result = HelperExtensions.SetCockpit(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetCockpit_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetCockpit(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" QuadVee ", Configuration.QuadVee, false)]
	[InlineData(" Biped Omnimech ", Configuration.Biped, true)]
	[InlineData(" Biped Omnimek ", Configuration.Biped, true)]
	public void SetConfig_ValidInput_Works(string input, Configuration expectedConfiguration, bool expectedIsOmniMech)
	{
		// Arrange
		// Act
		(var configuration, var isOmniMech) = HelperExtensions.SetConfig(input);

		// Assert
		configuration.ShouldBe(expectedConfiguration);
		isOmniMech.ShouldBe(expectedIsOmniMech);
	}

	[Theory]
	[InlineData("OtherValue")]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetConfig_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetConfig(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" Stone Rhino ", "Stone Rhino")]
	[InlineData(" Mad Dog Mk IV ", "Mad Dog Mk IV")]
	public void SetClanName_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = HelperExtensions.SetClanName(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetClanName_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetClanName(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData(" CRB-27b ", "CRB-27b")]
	[InlineData(" AGT-UA 'Ariel' ", "AGT-UA 'Ariel'")]
	public void SetModel_ValidInput_Works(string input, string expected)
	{
		// Arrange
		// Act
		var result = HelperExtensions.SetModel(input);

		// Assert
		result.ShouldBe(expected);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetModel_EmptyInput_ReturnsNull(string input)
	{
		// Arrange
		// Act
		var result = HelperExtensions.SetModel(input);

		// Assert
		result.ShouldBeNull();
	}

	[Theory]
	[InlineData(" Rec Guide:ilClan #24 ", "Rec Guide", "ilClan #24")]
	[InlineData(" Battle of Tukayyid ", null, "Battle of Tukayyid")]
	[InlineData(" TRO : 3067 ", "TRO", "3067")]
	public void SetSource_ValidInput_Works(string input, string? expectedType, string expectedName)
	{
		// Arrange
		// Act
		(var type, var name) = HelperExtensions.SetSource(input);

		// Assert
		type.ShouldBe(expectedType);
		name.ShouldBe(expectedName);
	}

	[Theory]
	[MemberData(nameof(TestData.EmptyAndWhiteSpaceStrings), MemberType = typeof(TestData))]
	public void SetSource_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetSource(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	#region Add Equipment

	[Fact]
	public void AddEquipmentAtLocation_CachedInput_ReturnsCachedValue()
	{
		// Arrange
		const string input = " -empty- ";
		const string cacheKey = "-Empty-";
		_ = MtfValues.Lookup.CommonEquipmentValues.TryGetValue(cacheKey, out var cachedValue);

		// Act
		(var name, var isOmniPod, var isRear, var isTurret) = HelperExtensions.AddEquipmentAtLocation(input);

		// Assert
		name.ShouldBeSameAs(cachedValue);
		isOmniPod.ShouldBeFalse();
		isRear.ShouldBeFalse();
		isTurret.ShouldBeFalse();
	}

	#endregion Add Equipment
}
