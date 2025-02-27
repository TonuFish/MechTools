using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct EquipmentData
{
	public readonly required string Name { get; init; }
	public readonly required bool IsOmniPod { get; init; }
	public readonly required bool IsRear { get; init; }
	public readonly required bool IsTurret { get; init; }

	[SetsRequiredMembers]
	public EquipmentData(string name, bool isOmniPod, bool isRear, bool isTurret)
	{
		Name = name;
		IsOmniPod = isOmniPod;
		IsRear = isRear;
		IsTurret = isTurret;
	}

	public readonly void Deconstruct(out string name, out bool isOmniPod, out bool isRear, out bool isTurret)
	{
		name = Name;
		isOmniPod = IsOmniPod;
		isRear = IsRear;
		isTurret = IsTurret;
	}
}
