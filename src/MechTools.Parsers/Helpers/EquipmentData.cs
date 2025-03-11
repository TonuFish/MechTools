using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct EquipmentData : IEquatable<EquipmentData>
{
	public readonly required bool IsOmniPod { get; init; }
	public readonly required bool IsRear { get; init; }
	public readonly required bool IsTurret { get; init; }
	public readonly required string Name { get; init; }

	[SetsRequiredMembers]
	public EquipmentData(bool isOmniPod, bool isRear, bool isTurret, string name)
	{
		Name = name;
		IsOmniPod = isOmniPod;
		IsRear = isRear;
		IsTurret = isTurret;
	}

	public readonly void Deconstruct(out bool isOmniPod, out bool isRear, out bool isTurret, out string name)
	{
		name = Name;
		isOmniPod = IsOmniPod;
		isRear = IsRear;
		isTurret = IsTurret;
	}

	#region Equality

	public static bool operator ==(EquipmentData left, EquipmentData right) => left.Equals(right);

	public static bool operator !=(EquipmentData left, EquipmentData right) => !(left == right);

	public bool Equals(EquipmentData other)
	{
		return Name.Equals(other.Name, StringComparison.Ordinal)
			&& IsOmniPod == other.IsOmniPod
			&& IsRear == other.IsRear
			&& IsTurret == other.IsTurret;
	}

	public override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is EquipmentData && Equals((EquipmentData)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name, IsOmniPod, IsRear, IsTurret);
	}

	#endregion Equality
}
