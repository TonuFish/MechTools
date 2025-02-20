using MechTools.Parsers;
using MechTools.Parsers.BattleMech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.UnitTests;

public sealed class UnitTest1
{
	[Fact]
	public async Task Test1()
	{
		List<string> brokenList = [];

		foreach (var filePath in Directory.EnumerateFiles(@"..\..\..\..\..\scratch"))
		{
			await using var file = File.OpenRead(filePath);
			try
			{
				MtfBattleMechParser<List<string>> parser = new(new RawBattleMechBuilder());
				var lines = await parser.ParseAsync(file, CancellationToken.None);
				var qqq = string.Join('\n', lines!);
			}
			catch (ImExcitedException)
			{
				// I don't care right now.
			}
			catch (Exception ex)
			{
				brokenList.Add($"{filePath}```{ex}");
			}
		}

		var asdf = 5;
	}
}
