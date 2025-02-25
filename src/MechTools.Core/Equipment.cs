using System.Collections.Generic;

namespace MechTools.Core;

public sealed class Equipment
{
	public required string Name { get; set; }
	public bool OmniPod { get; set; }
	public List<string>? Quirks { get; set; }
	public bool RearMounted { get; set; }
	public bool Turreted { get; set; }
}
