using MechTools.Core.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct ArmourData
{
	public readonly required Armour Armour { get; init; }
	public readonly required Origin? Origin { get; init; }

	[SetsRequiredMembers]
	public ArmourData(Armour armour, Origin? origin)
	{
		Armour = armour;
		Origin = origin;
	}

	public void Deconstruct(out Armour armour, out Origin? origin)
	{
		armour = Armour;
		origin = Origin;
	}
}
