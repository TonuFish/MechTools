using MechTools.Core.Enums;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct SpecificSystemData
{
	public readonly string Name { get; init; }
	public readonly SpecificSystem SpecificSystem { get; init; }

	public SpecificSystemData(string name, SpecificSystem system)
	{
		Name = name;
		SpecificSystem = system;
	}

	public readonly void Deconstruct(out string name, out SpecificSystem specificSystem)
	{
		name = Name;
		specificSystem = SpecificSystem;
	}
}
