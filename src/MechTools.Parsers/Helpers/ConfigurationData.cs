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

	#region Equality

	public static bool operator ==(ConfigurationData left, ConfigurationData right) => left.Equals(right);

	public static bool operator !=(ConfigurationData left, ConfigurationData right) => !(left == right);

	public bool Equals(ConfigurationData other)
	{
		return Configuration == other.Configuration && IsOmniMech == other.IsOmniMech;
	}

	public override bool Equals([MaybeNullWhen(false)] object? obj)
	{
		return obj is ConfigurationData && Equals((ConfigurationData)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Configuration, IsOmniMech);
	}

	#endregion Equality
}
