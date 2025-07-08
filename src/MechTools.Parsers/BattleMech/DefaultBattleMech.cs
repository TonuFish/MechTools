using MechTools.Core.Enums;
using MechTools.Parsers.Helpers;
using System.Collections.Generic;

namespace MechTools.Parsers.BattleMech;

public sealed class DefaultBattleMech
{
	public ArmourData Armour { get; set; }
	public Dictionary<BattleMechArmourLocation, LocationArmourData> ArmourAtLocation { get; } = [];
	public int BaseChassisHeatSinks { get; set; }
	public string? Capabilities { get; set; }
	public string Chassis { get; set; } = string.Empty;
	public string? ClanName { get; set; }
	public Cockpit? Cockpit { get; set; }
	public List<string> Comments { get; } = [];
	public ConfigurationData Configuration { get; set; }
	public string? Deployment { get; set; }
	public string? Ejection { get; set; }
	public EngineData Engine { get; set; }
	public Dictionary<BattleMechEquipmentLocation, EquipmentData> EquipmentAtLocation { get; } = [];
	public int Era { get; set; }
	public string? Generator { get; set; }
	public Gyro? Gyro { get; set; }
	public string? HeatSinkKit { get; set; }
	public HeatSinkData HeatSinks { get; set; }
	public string? History { get; set; }
	public string? ImageFile { get; set; }
	public int JumpMp { get; set; }
	public Lam? Lam { get; set; }
	public string? Manufacturer { get; set; }
	public int Mass { get; set; }
	public string? Model { get; set; } // TODO: Shouldn't allow null.
	public Motive? Motive { get; set; }
	public int? MulId { get; set; }
	public Myomer Myomer { get; set; }
	public List<NoCritData> NoCrits { get; } = [];
	public string? Notes { get; set; }
	public List<string> Quirks { get; } = [];
	public string? Overview { get; set; }
	public string? PrimaryFactory { get; set; }
	public Role? Role { get; set; }
	public SourceData Source { get; set; }
	public Structure Structure { get; set; }
	public List<SpecificSystemData> SystemManufacturers { get; } = [];
	public List<SpecificSystemData> SystemModels { get; } = [];
	public RulesLevel RulesLevel { get; set; }
	public TechBase TechBase { get; set; }
	public int WalkMp { get; set; }
	public List<WeaponQuirkData> WeaponQuirks { get; } = [];
	public List<WeaponListData> WeaponList { get; } = [];
	public int WeaponListCount { get; set; }
}
