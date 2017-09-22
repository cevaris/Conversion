using System;
using System.Collections.Generic;

namespace Conversion
{
    public enum UnitGroup
    {
        Distance,
        Data,
        Speed,
        Temperature,
        Time,
        Weight
    }

    public enum UnitType
    {
        Celsius,
        Fahrenheit,
        Kelvin,
        Reaumur,
        Newton,
        Rankine,

        Kilometer,
        Meter,
        Kecimeter,
        Centimeter,
        Millimeter,
        Mile,
        Inch,
        Foot,
        Yard,
        NauticalMile

    }

    class Units
    {
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

    }
}
