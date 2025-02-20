namespace MechTools.Parsers.BattleMech;

// TODO: Sections kept in value alphabetical order --- Test this is worthwhile RE: Switch in parser default branch.
internal static class MtfSections
{
	public const string Armour = "ARMOR";
	public const string BaseChassisHeatSinks = "BASE CHASSIS HEAT SINKS";
	public const string Capabilities = "CAPABILITIES";
	public const string Chassis = "CHASSIS";
	public const string ClanName = "CLANNAME";
	public const char Comment = '#';
	public const string Cockpit = "COCKPIT";
	public const string Config = "CONFIG";
	public const string Deployment = "DEPLOYMENT";
	public const string Ejection = "EJECTION";
	public const string Engine = "ENGINE";
	public const string Era = "ERA";
	public const string Generator = "GENERATOR";
	public const string Gyro = "GYRO";
	public const string HeatSinks = "HEAT SINKS";
	public const string History = "HISTORY";
	public const string ImageFile = "IMAGEFILE";
	public const string JumpMp = "JUMP MP";
	public const string Manufacturer = "MANUFACTURER";
	public const string Mass = "MASS";
	public const string Model = "MODEL";
	public const string Motive = "MOTIVE";
	public const string MulId = "MUL ID";
	public const string Myomer = "MYOMER";
	public const string NoCrit = "NOCRIT";
	public const string Notes = "NOTES";
	public const string Overview = "OVERVIEW";
	public const string PrimaryFactory = "PRIMARYFACTORY";
	public const string Quirk = "QUIRK";
	public const string Role = "ROLE";
	public const string RulesLevel = "RULES LEVEL";
	public const string Source = "SOURCE";
	public const string Structure = "STRUCTURE";
	public const string SystemManufacturer = "SYSTEMMANUFACTURER";
	public const string SystemMode = "SYSTEMMODE";
	public const string TechBase = "TECHBASE";
	public const string WalkMp = "WALK MP";
	public const string WeaponQuirk = "WEAPONQUIRK";
	public const string Weapons = "WEAPONS";

	public static class ArmourLocation
	{
		public const string CentreLeg = "CL ARMOR";
		public const string CentreTorso = "CT ARMOR";
		public const string FrontLeftLeg = "FLL ARMOR";
		public const string FrontRightLeg = "FRL ARMOR";
		public const string Head = "HD ARMOR";
		public const string LeftArm = "LA ARMOR";
		public const string LeftLeg = "LL ARMOR";
		public const string LeftTorso = "LT ARMOR";
		public const string RightArm = "RA ARMOR";
		public const string RightLeg = "RL ARMOR";
		public const string RearLeftLeg = "RLL ARMOR";
		public const string RearRightLeg = "RRL ARMOR";
		public const string RightTorso = "RT ARMOR";
		public const string RearCentreTorso = "RTC ARMOR";
		public const string RearLeftTorso = "RTL ARMOR";
		public const string RearRightTorso = "RTR ARMOR";
	}

	public static class EquipmentLocation
	{
		public const string CentreLeg = "CENTER LEG";
		public const string CentreTorso = "CENTER TORSO";
		public const string FrontLeftLeg = "FRONT LEFT LEG";
		public const string FrontRightLeg = "FRONT RIGHT LEG";
		public const string Head = "HEAD";
		public const string LeftArm = "LEFT ARM";
		public const string LeftLeg = "LEFT LEG";
		public const string LeftTorso = "LEFT TORSO";
		public const string RearLeftLeg = "REAR LEFT LEG";
		public const string RearRightLeg = "REAR RIGHT LEG";
		public const string RightArm = "RIGHT ARM";
		public const string RightLeg = "RIGHT LEG";
		public const string RightTorso = "RIGHT TORSO";
	}
}
