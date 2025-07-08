using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct SpecificSystemData : IEquatable<SpecificSystemData>
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

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Name}```{SpecificSystem}";
	}

#endif
	#region Equality

	public static bool operator ==(SpecificSystemData left, SpecificSystemData right) => left.Equals(right);

	public static bool operator !=(SpecificSystemData left, SpecificSystemData right) => !(left == right);

	public readonly bool Equals(SpecificSystemData other)
	{
		return Name.Equals(other.Name, StringComparison.Ordinal) && SpecificSystem == other.SpecificSystem;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is SpecificSystemData && Equals((SpecificSystemData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Name, SpecificSystem);
	}

	#endregion Equality
}
