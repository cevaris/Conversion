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
        bit,      // 1 bit
        _byte,     // 8 bits
        kilobit,  // 1000 bits
        kibibit,  // 1024 bits
        kilobyte, // 1000 bytes
        kibibyte, // 1024 bytes
        megabit,
        mebibit,
        megabyte,
        mebibyte,
        gigabit,
        gibibit,
        gigabyte,
        gibibyte,
        terabit,
        tebibit,
        terabyte,
        tebibyte,
        petabit,
        pebibit,
        petabyte,
        pebibyte,
        exabit,
        exbibit,
        exabyte,
        exbibyte,

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
            UnitType.bit,
            UnitType._byte,

            UnitType.kilobit,
            UnitType.megabit,
            UnitType.gigabit,
            UnitType.terabit,
            UnitType.petabit,
            UnitType.exabit,

            UnitType.kibibit,
            UnitType.mebibit,
            UnitType.gibibit,
            UnitType.tebibit,
            UnitType.pebibit,
            UnitType.exbibit,

            UnitType.kilobyte,
            UnitType.megabyte,
            UnitType.gigabyte,
            UnitType.terabyte,
            UnitType.petabyte,
            UnitType.exabyte,

            UnitType.kibibyte,
            UnitType.mebibyte,
            UnitType.gibibyte,
            UnitType.tebibyte,
            UnitType.pebibyte,
            UnitType.exbibyte,
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
            {UnitType.bit, AppResources.bit},
            {UnitType._byte, AppResources._byte},
            {UnitType.kilobyte, AppResources.kilobyte},
            {UnitType.megabyte, AppResources.megabyte},
            {UnitType.gigabyte, AppResources.gigabyte},
            {UnitType.terabyte, AppResources.terabyte},
            {UnitType.petabyte, AppResources.petabyte},
            {UnitType.exabyte, AppResources.exabyte},
        };

        public static IDictionary<UnitType, string> UnitTypeAbbrNameMap = new Dictionary<UnitType, string>()
        {
            {UnitType.bit, "b"},
            {UnitType._byte, "B"},

            {UnitType.kilobit, "kib"},
            {UnitType.megabit, "Mb"},
            {UnitType.gigabit, "Gb"},
            {UnitType.terabit, "Tb"},
            {UnitType.petabit, "Pb"},
            {UnitType.exabit, "Eb"},

            {UnitType.kibibit, "Kib"},
            {UnitType.mebibit, "Mib"},
            {UnitType.gibibit, "Gib"},
            {UnitType.tebibit, "Tib"},
            {UnitType.pebibit, "Pib"},
            {UnitType.exbibit, "Exb"},

            {UnitType.kilobyte, "kB"},
            {UnitType.megabyte, "MB"},
            {UnitType.gigabyte, "GB"},
            {UnitType.terabyte, "TB"},
            {UnitType.petabyte, "PB"},
            {UnitType.exabyte, "EB"},

            {UnitType.kibibyte, "KiB"},
            {UnitType.mebibyte, "MiB"},
            {UnitType.gibibyte, "GiB"},
            {UnitType.tebibyte, "TiB"},
            {UnitType.pebibyte, "PiB"},
            {UnitType.exbibyte, "EiB"},
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
