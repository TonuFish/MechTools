namespace MechTools.UnitTests;

internal static class TestData
{
	public static TheoryData<string> EmptyAndWhiteSpaceStrings()
	{
		return new(
			"",
			"   ");
	}
}
