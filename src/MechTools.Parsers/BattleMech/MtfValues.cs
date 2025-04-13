using System;
using System.Collections.Frozen;

namespace MechTools.Parsers.BattleMech;

internal static class MtfValues
{
	private static readonly FrozenSet<string> _commonEquipmentValues = FrozenSet.Create(
		StringComparer.OrdinalIgnoreCase,
		"-Empty-",
		"Fusion Engine",
		"Gyro",
		"Upper Leg Actuator",
		"Lower Leg Actuator",
		"Foot Actuator",
		"Hip",
		"Sensors",
		"IS Endo Steel",
		"Life Support",
		"Upper Arm Actuator",
		"Shoulder",
		"ISDoubleHeatSink",
		"Endo Steel",
		"Jump Jet",
		"Clan Endo Steel",
		"Lower Arm Actuator",
		"Clan Ferro-Fibrous",
		"Ferro-Fibrous",
		"Cockpit",
		"CLDoubleHeatSink",
		"Hand Actuator",
		"Heat Sink");

	public static class Lookup
	{
		public static FrozenSet<string>.AlternateLookup<ReadOnlySpan<char>> CommonEquipmentValues { get; }
			= _commonEquipmentValues.GetAlternateLookup<ReadOnlySpan<char>>();
	}
}