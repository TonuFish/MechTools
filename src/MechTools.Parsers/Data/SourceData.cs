using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct SourceData : IEquatable<SourceData>
{
	public readonly required string Name { get; init; }
	public readonly required string? Type { get; init; }

	[SetsRequiredMembers]
	public SourceData(string name, string? type)
	{
		Name = name;
		Type = type;
	}

	public readonly void Deconstruct(out string name, out string? type)
	{
		name = Name;
		type = Type;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Name}```{Type}";
	}

#endif
	#region Equality

	public static bool operator ==(SourceData left, SourceData right) => left.Equals(right);

	public static bool operator !=(SourceData left, SourceData right) => !(left == right);

	public readonly bool Equals(SourceData other)
	{
		return Name.Equals(other.Name, StringComparison.Ordinal)
			&& string.Equals(Type, other.Type, StringComparison.Ordinal);
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is SourceData sourceData && Equals(sourceData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Name, Type);
	}

	#endregion Equality
}
