using MechTools.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Helpers;

[StructLayout(LayoutKind.Auto)]
public readonly struct ConfigurationData : IEquatable<ConfigurationData>
{
	public readonly required Configuration Configuration { get; init; }
	public readonly required bool IsOmniMech { get; init; }

	[SetsRequiredMembers]
	public ConfigurationData(Configuration configuration, bool isOmniMech)
	{
		Configuration = configuration;
		IsOmniMech = isOmniMech;
	}

	public void Deconstruct(out Configuration configuration, out bool isOmniMech)
	{
		configuration = Configuration;
		isOmniMech = IsOmniMech;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Configuration}```{IsOmniMech}";
	}

#endif
	#region Equality

	public static bool operator ==(ConfigurationData left, ConfigurationData right) => left.Equals(right);

	public static bool operator !=(ConfigurationData left, ConfigurationData right) => !(left == right);

	public readonly bool Equals(ConfigurationData other)
	{
		return Configuration == other.Configuration && IsOmniMech == other.IsOmniMech;
	}

	public readonly override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is ConfigurationData && Equals((ConfigurationData)obj);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Configuration, IsOmniMech);
	}

	#endregion Equality
}
