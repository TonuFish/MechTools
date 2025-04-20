using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct HeatSinkData : IEquatable<HeatSinkData>
{
	public readonly int Count { get; init; }
	public readonly HeatSink HeatSink { get; init; }
	public readonly Origin Origin { get; init; }

	public HeatSinkData(int count, HeatSink heatSink, Origin name)
	{
		Count = count;
		HeatSink = heatSink;
		Origin = name;
	}

	public readonly void Deconstruct(out int count, out HeatSink heatSink, out Origin origin)
	{
		count = Count;
		heatSink = HeatSink;
		origin = Origin;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Count}```{HeatSink}```{Origin}";
	}

#endif
	#region Equality

	public static bool operator ==(HeatSinkData left, HeatSinkData right) => left.Equals(right);

	public static bool operator !=(HeatSinkData left, HeatSinkData right) => !(left == right);

	public readonly bool Equals(HeatSinkData other)
	{
		return Count == other.Count && HeatSink == other.HeatSink && Origin == other.Origin;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is HeatSinkData && Equals((HeatSinkData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Count, HeatSink, Origin);
	}

	#endregion Equality
}
