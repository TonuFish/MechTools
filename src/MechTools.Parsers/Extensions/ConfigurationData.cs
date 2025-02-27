using MechTools.Core;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MechTools.Parsers.Extensions;

[StructLayout(LayoutKind.Auto)]
public readonly struct ConfigurationData
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
}
