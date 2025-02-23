using MechTools.Parsers.Extensions;
using System;

namespace MechTools.UnitTests;

public sealed class HelperTests
{
	// TODO: Expand on all the random splattering here.

	[Theory]
	[InlineData("Crab", "Crab")]
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
	[InlineData("")]
	[InlineData("   ")]
	public void SetChassis_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetChassis(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData("Stone Rhino", "Stone Rhino")]
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
	[InlineData("")]
	[InlineData("   ")]
	public void SetClanName_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetClanName(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData("CRB-27b", "CRB-27b")]
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
	[InlineData("")]
	[InlineData("   ")]
	public void SetModel_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetModel(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}

	[Theory]
	[InlineData("Rec Guide:ilClan #24", "Rec Guide", "ilClan #24")]
	[InlineData("Battle of Tukayyid", null, "Battle of Tukayyid")]
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
	[InlineData("")]
	[InlineData("   ")]
	public void SetSource_InvalidInput_Throws(string input)
	{
		// Arrange
		Func<object> func = () => HelperExtensions.SetSource(input);

		// Act
		// Assert
		_ = func.ShouldThrow<Exception>();
	}
}
