#define SYNC

using MechTools.Parsers.BattleMech;
using MechTools.Parsers.Mtf;
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
		await EnumerateScratchAsync(CancellationToken.None).ConfigureAwait(true);
	}

	private static async Task EnumerateScratchAsync(CancellationToken ct)
	{
		List<string>? brokenList = null;

		foreach (var filePath in Directory.EnumerateFiles(@"..\..\..\..\..\scratch"))
		{
			const bool skipKnownDodgyFiles = true;
			if (skipKnownDodgyFiles
				&& (
					// Bad text dumps
					filePath.EndsWith("Antlion LK-3D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Anubis ABS-4C.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-200-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-300-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-400-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-500-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-900-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Hussar HSR-950-D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Poseidon PSD-V2.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Spartan SPT-N3.mtf", StringComparison.Ordinal)
					// Bad text dumps (new)
					|| filePath.EndsWith("Falconer FLC-8R.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Falconer FLC-9R.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Fox CS-1.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-10D.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-9FC.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-9J (Holt).mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-9J Brubaker.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-9J.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Nightstar NSR-9SS.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Rampage RMP-2G.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Rampage RMP-4G (Benboudaoud).mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Rampage RMP-4G.mtf", StringComparison.Ordinal)
					|| filePath.EndsWith("Rampage RMP-5G.mtf", StringComparison.Ordinal)
					// Bad cockpit
					|| filePath.EndsWith("Arbiter ARB-001.mtf", StringComparison.Ordinal)
					// Empty deployment
					|| filePath.EndsWith("Seraph C-SRP-O Caelestis.mtf", StringComparison.Ordinal)
				))
			{
				// Skip these malformed mechs for now.
				continue;
			}

#if SYNC
			var file = await File.ReadAllTextAsync(filePath, ct).ConfigureAwait(false);
			try
			{
				using MtfBattleMechParser<DefaultBattleMech> parser = new(new DefaultBattleMechBuilder());
				var mech = parser.Parse(file);
				if (mech is not null)
				{
					Console.WriteLine($"{mech.Chassis} ({mech.Model}) done.");
				}
				else
				{
					Console.WriteLine($"{filePath} failed.");
				}
			}
#else
			try
			{
				using MtfBattleMechParser<DefaultBattleMech> parser = new(new DefaultBattleMechBuilder());
				var mech = await parser.ParseAsync(file, ct).ConfigureAwait(false);
				if (mech is not null)
				{
					Console.WriteLine($"{mech.Chassis} ({mech.Model}) done.");
				}
				else
				{
					Console.WriteLine($"{filePath} failed.");
				}
			}
#endif
			catch (Exception ex)
			{
				brokenList ??= [];
				brokenList.Add($"{filePath} ({ex.Message})");
			}
		}

		if (brokenList is not null)
		{
			Console.WriteLine();

			foreach (var broken in brokenList)
			{
				Console.WriteLine(broken);
			}
		}
	}
}
