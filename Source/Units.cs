using System;
using System.Collections.Generic;
using System.Linq;

namespace Conversion
{
    public enum UnitGroup
    {
        Data,
        Distance,
        Speed,
        Temperature,
        Time,
        Weight
    }

    public enum UnitType
    {
        // Distance
        Kilometer,
        Meter,
        Kecimeter,
        Centimeter,
        Millimeter,
        Mile,
        Inch,
        Foot,
        Yard,
        NauticalMile,

        // Temperature
        Celsius,
        Fahrenheit,
        Kelvin,
        Reaumur,
        Newton,
        Rankine,

        // Time
        Year,
        Quarter,
        Month,
        Week,
        Day,
        Hour,
        Minute,
        Second,
        Millisecond,
        Microsecond,
        Picosecond,
        Femtosecond,
        Attosecond
    }

    class Units
    {
        public static List<UnitGroup> UnitGroups = Enum.GetValues(typeof(UnitGroup))
                                                       .Cast<UnitGroup>()
                                                       .ToList();

        public static List<UnitType> TemperatureOpts = new List<UnitType>
        {
            UnitType.Celsius,
            UnitType.Fahrenheit,
            UnitType.Kelvin,
            UnitType.Reaumur,
            UnitType.Newton,
            UnitType.Rankine
        };

        public static List<UnitType> DistanceOpts = new List<UnitType>
        {
            UnitType.Kilometer,
            UnitType.Meter,
            UnitType.Kecimeter,
            UnitType.Centimeter,
            UnitType.Millimeter,
            UnitType.Mile,
            UnitType.Inch,
            UnitType.Foot,
            UnitType.Yard,
            UnitType.NauticalMile
        };

        public static List<UnitType> TimeOpts = new List<UnitType>
        {
            UnitType.Year,
            UnitType.Quarter,
            UnitType.Month,
            UnitType.Week,
            UnitType.Day,
            UnitType.Hour,
            UnitType.Minute,
            UnitType.Second,
            UnitType.Millisecond,
            UnitType.Microsecond,
            UnitType.Picosecond,
            UnitType.Femtosecond,
            UnitType.Attosecond
        };

        public static IDictionary<UnitGroup, List<UnitType>> UnitMap = new SortedDictionary<UnitGroup, List<UnitType>>()
        {
            {UnitGroup.Data, TemperatureOpts},
            {UnitGroup.Distance, DistanceOpts},
            {UnitGroup.Speed, TemperatureOpts},
            {UnitGroup.Temperature, TemperatureOpts},
            {UnitGroup.Time, TimeOpts},
            {UnitGroup.Weight, TemperatureOpts},
        };

    }
}
