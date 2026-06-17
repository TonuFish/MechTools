#define SYNC

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

		foreach (var filePath in Directory.EnumerateFiles(@"..\..\..\..\..\scratch", "*.mtf"))
		{
			const bool skipKnownDodgyFiles = true;
			if (skipKnownDodgyFiles
				&& (
					// All files happy :)
					false
				))
			{
				// Skip these malformed mechs for now.
				continue;
			}

#if SYNC
			var file = await File.ReadAllTextAsync(filePath, ct).ConfigureAwait(false);
			try
			{
				var mech = MtfBattleMechParser.Parse(file);
				Console.WriteLine($"{mech.Chassis} ({mech.Model}) done.");
			}
#else
			try
			{
				await using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
				var mech = await MtfBattleMechParser.ParseAsync(stream, ct).ConfigureAwait(false);
				Console.WriteLine($"{mech.Chassis} ({mech.Model}) done.");
			}
#endif
			catch (Exception ex)
			{
				Console.WriteLine($"{filePath} failed.");
				brokenList ??= [];
				brokenList.Add($"{filePath[(filePath.LastIndexOf('\\') + 1)..]} ({ex.Message})");
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
