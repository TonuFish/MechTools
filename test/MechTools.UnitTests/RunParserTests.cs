using MechTools.Core;
using MechTools.Parsers;
using MechTools.Parsers.BattleMech;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.UnitTests;
public sealed class RunParserTests
{
	[Fact(Skip = "For running purposes.")]
	public async Task Test1()
	{
		List<string> brokenList = [];
		var excitedCount = 0;

		foreach (var filePath in Directory.EnumerateFiles(@"..\..\..\..\..\scratch"))
		{
			if (filePath.EndsWith("Hussar HSR-200-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Hussar HSR-300-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Hussar HSR-400-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Hussar HSR-500-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Hussar HSR-900-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Hussar HSR-950-D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Antlion LK-3D.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Anubis ABS-4C.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Poseidon PSD-V2.mtf", StringComparison.Ordinal)
				|| filePath.EndsWith("Spartan SPT-N3.mtf", StringComparison.Ordinal))
			{
				// Skip the malformed text blob mechs for now.
				continue;
			}

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
				excitedCount++;
			}
			catch (Exception ex)
			{
				brokenList.Add($"{filePath}```{ex}");
			}
		}

		var asdf = 5;
	}
}
