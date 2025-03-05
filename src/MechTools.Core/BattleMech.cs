using MechTools.Core.Enums;
using System.Collections.Generic;

namespace MechTools.Core;

public sealed class BattleMech
{
	public int BaseChassisHeatSinks { get; set; }
	public Cockpit Cockpit { get; set; }
	public Configuration Configuration { get; set; }
	public List<string>? Comments { get; set; }
	public bool FullHeadEjection { get; set; }
	public Gyro Gyro { get; set; }
	public int JumpMp { get; set; }
	public List<string>? Quirks { get; set; }
	public int WalkMp { get; set; }

	// TODO: The rest of it.

	// TODO: Armour
	// TODO: Equipment

	#region Base Flavour

	public string? ClanName { get; set; }

	#endregion Base Flavour

	#region Extra Flavour

	public string? Deployment { get; set; }
	public string? Capabilities { get; set; }
	public string? History { get; set; }
	public string? Manufacturer { get; set; }
	public string? Overview { get; set; }
	public string? PrimaryFactory { get; set; }

	// TODO: System manufacturer[s]

	#endregion Extra Flavour
}
