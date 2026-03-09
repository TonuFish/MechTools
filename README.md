# MechTools

A small library for parsing BattleTech BattleMech `mtf` files.

## Basic Usage

```csharp
public DefaultBattleMech ParseMtfFile(string mtfPath)
{
	// Synchronously
	var mtfFile = File.ReadAllText(mtfPath);
	return MtfBattleMechParser.Parse(mtfFile);
}

public async Task<DefaultBattleMech> ParseMtfFileAsync(string mtfPath, CancellationToken ct)
{
	// Asynchronously
	await using FileStream mtfStream = new(mtfPath, FileMode.Open, FileAccess.Read);
	return await MtfBattleMechParser.ParseAsync(mtfStream, ct);
}
```

## Extended Usage

`IBattleMechBuilder<TMech>` may be implemented to allow the parser to assemble your type directly. `MechTools.Parsers.Mtf.MtfHelpers` contains default parsers for each mtf tag type to save some string manipulation.

## Future Work

- Record sheet designer
  - Build your own 'Mech record sheets
  - Emit as PDF
