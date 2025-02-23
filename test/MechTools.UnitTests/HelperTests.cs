using MechTools.Parsers.Extensions;

namespace MechTools.UnitTests;

public sealed class HelperTests
{
	// TODO: Expand on all the random splattering here.

	[Theory]
	[InlineData("Rec Guide:ilClan #24", "Rec Guide", "ilClan #24")]
	[InlineData("Battle of Tukayyid", null, "Battle of Tukayyid")]
	[InlineData("TRO : 3067", "TRO", "3067")]
	public void SetSource_ValidInput_Works(string input, string? expectedType, string expectedName)
	{
		// Arrange
		// Act
		(var type, var name) = HelperExtensions.SetSource(input);

		// Assert
		type.ShouldBe(expectedType);
		name.ShouldBe(expectedName);
	}
}
