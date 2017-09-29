using System;
using System.Collections.Generic;
using System.Linq;
using Conversion.Resx;

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
        // Data
        Bit,
        Byte,
        Kilobyte,
        Megabyte,
        Gigabyte,
        Terabyte,
        Petabyte,
        Exabyte,
        Zettabyte,
        Yottabyte,


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
        public static List<UnitType> DataOpts = new List<UnitType>()
        {
            UnitType.Bit,
            UnitType.Byte,
            UnitType.Kilobyte,
            UnitType.Megabyte,
            UnitType.Gigabyte,
            UnitType.Terabyte,
            UnitType.Petabyte,
            UnitType.Exabyte,
            UnitType.Zettabyte,
            UnitType.Yottabyte,
        };

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

        public static IDictionary<UnitGroup, string> UnitGroupNameMap = new Dictionary<UnitGroup, string>()
        {
            {UnitGroup.Data, AppResources.data},
            {UnitGroup.Distance, AppResources.distance},
            {UnitGroup.Speed, AppResources.speed},
            {UnitGroup.Temperature, AppResources.temperature},
            {UnitGroup.Time, AppResources.time},
            {UnitGroup.Weight, AppResources.weight},
        };

        public static IDictionary<UnitType, string> UnitTypeNameMap = new Dictionary<UnitType, string>()
        {
            {UnitType.Bit, AppResources.bit},
            {UnitType.Byte, AppResources._byte},
            {UnitType.Kilobyte, AppResources.kilobyte},
            {UnitType.Megabyte, AppResources.megabyte},
            {UnitType.Gigabyte, AppResources.gigabyte},
            {UnitType.Terabyte, AppResources.terabyte},
            {UnitType.Petabyte, AppResources.petabyte},
            {UnitType.Exabyte, AppResources.exabyte},
            {UnitType.Zettabyte, AppResources.zettabyte},
            {UnitType.Yottabyte, AppResources.yottabyte},
        };

        public static IDictionary<UnitType, string> UnitTypeAbbrNameMap = new Dictionary<UnitType, string>()
        {
            {UnitType.Bit, "b"},
            {UnitType.Byte, "B"},
            {UnitType.Kilobyte, "kB"},
            {UnitType.Megabyte, "MB"},
            {UnitType.Gigabyte, "GB"},
            {UnitType.Terabyte, "TB"},
            {UnitType.Petabyte, "PB"},
            {UnitType.Exabyte, "EB"},
            {UnitType.Zettabyte, "ZB"},
            {UnitType.Yottabyte, "YB"},
        };

        public static IDictionary<UnitGroup, List<UnitType>> UnitMap = new SortedDictionary<UnitGroup, List<UnitType>>()
        {
            {UnitGroup.Data, DataOpts},
            {UnitGroup.Distance, DistanceOpts},
            {UnitGroup.Speed, TemperatureOpts},
            {UnitGroup.Temperature, TemperatureOpts},
            {UnitGroup.Time, TimeOpts},
            {UnitGroup.Weight, TemperatureOpts},
        };



        public static string T(UnitGroup unitGroup)
        {
            string result;
            if (UnitGroupNameMap.TryGetValue(unitGroup, out result))
            {
                return result;
            }
            else
            {
                return unitGroup.ToString();
            }
        }

        public static string T(UnitType unitType)
        {
            string result;
            if (UnitTypeNameMap.TryGetValue(unitType, out result))
            {
                return result;
            }
            else
            {
                return unitType.ToString();
            }
        }

        public static string TAbbr(UnitType unitType)
        {
            string result;
            if (UnitTypeAbbrNameMap.TryGetValue(unitType, out result))
            {
                return result;
            }
            else
            {
                return unitType.ToString();
            }
        }
    }
}
