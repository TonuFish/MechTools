using System;
using System.Collections.Frozen;

namespace MechTools.Parsers.Mtf;

internal static class MtfValues
{
	public static class Lookup
	{
		public static FrozenSet<string>.AlternateLookup<ReadOnlySpan<char>> CommonEquipmentValues { get; }
			= FrozenSet.Create(
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
				"Heat Sink")
				.GetAlternateLookup<ReadOnlySpan<char>>();
	}
}
