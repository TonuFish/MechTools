using MechTools.Parsers.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Data;

[StructLayout(LayoutKind.Auto)]
public readonly struct ConfigurationData : IEquatable<ConfigurationData>
{
	public readonly required Configuration Configuration { get; init; }
	public readonly required bool IsFrankenMech { get; init; }
	public readonly required bool IsOmniMech { get; init; }

	[SetsRequiredMembers]
	public ConfigurationData(Configuration configuration, bool isFrankenMech, bool isOmniMech)
	{
		Configuration = configuration;
		IsFrankenMech = isFrankenMech;
		IsOmniMech = isOmniMech;
	}

	public readonly void Deconstruct(out Configuration configuration, out bool isFrankenMech, out bool isOmniMech)
	{
		configuration = Configuration;
		isFrankenMech = IsFrankenMech;
		isOmniMech = IsOmniMech;
	}

#if DEBUG
	public readonly override string ToString()
	{
		return $"{Configuration}```{IsFrankenMech}```{IsOmniMech}";
	}

#endif
	#region Equality

	public static bool operator ==(ConfigurationData left, ConfigurationData right) => left.Equals(right);

	public static bool operator !=(ConfigurationData left, ConfigurationData right) => !(left == right);

	public readonly bool Equals(ConfigurationData other)
	{
		return Configuration == other.Configuration
			&& IsFrankenMech == other.IsFrankenMech
			&& IsOmniMech == other.IsOmniMech;
	}

	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ConfigurationData configurationData && Equals(configurationData);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Configuration, IsFrankenMech, IsOmniMech);
	}

	#endregion Equality
}
