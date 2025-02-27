using MechTools.Parsers.BattleMech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers.Runner;

internal static class Program
{
	private static async Task Main()
	{
		List<string> brokenList = [];
		var excitedCount = 0;

		foreach (var filePath in Directory.EnumerateFiles(@"..\..\..\..\..\scratch"))
		{
			if (// Bad text dumps
			    filePath.EndsWith("Hussar HSR-200-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Hussar HSR-300-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Hussar HSR-400-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Hussar HSR-500-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Hussar HSR-900-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Hussar HSR-950-D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Antlion LK-3D.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Anubis ABS-4C.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Poseidon PSD-V2.mtf", StringComparison.Ordinal)
			    || filePath.EndsWith("Spartan SPT-N3.mtf", StringComparison.Ordinal)

			    // Bad cockpit
			    || filePath.EndsWith("Arbiter ARB-001.mtf", StringComparison.Ordinal))
			{
				// Skip the malformed text blob mechs for now.
				continue;
			}

			await using var file = File.OpenRead(filePath);
			try
			{
				MtfBattleMechParser<List<string>> parser = new(new RawBattleMechBuilder());
				var lines = await parser.ParseAsync(file, CancellationToken.None).ConfigureAwait(false);
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
