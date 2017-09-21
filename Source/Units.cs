using System;
using System.Collections.Generic;

namespace Conversion
{
    public enum UnitGroup
    {
        Distance,
        Temperature
    }

    public enum UnitType
    {
        Celsius,
        Fahrenheit,
        Kelvin,
        Reaumur,
        Newton,
        Rankine
    }

    class Units
    {
        public static List<UnitType> TemperatureOptions = new List<UnitType>
        {
            UnitType.Celsius,
            UnitType.Fahrenheit,
            UnitType.Kelvin,
            UnitType.Reaumur,
            UnitType.Newton,
            UnitType.Rankine
        };

    }
}
