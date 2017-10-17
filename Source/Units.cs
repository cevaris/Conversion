using System;
using System.Collections.Generic;
using System.Linq;
using Conversion.Resx;

namespace Conversion
{
    public enum UnitGroup
    {
        data,
        distance,
        speed,
        temperature,
        time,
        weight
    }

    public enum UnitType
    {
        // Data
        bit,      // 1 bit
        _byte,    // 8 bits

        kilobit,  // 1000 bits
        megabit,
        gigabit,
        terabit,
        petabit,
        exabit,

        kibibit,  // 1024 bits
        mebibit,
        gibibit,
        tebibit,
        pebibit,
        exbibit,

        kilobyte, // 1000 bytes
        megabyte,
        gibibyte,
        terabyte,
        petabyte,
        exabyte,

        kibibyte, // 1024 bytes
        mebibyte,
        gigabyte,
        tebibyte,
        pebibyte,
        exbibyte,


        // Distance
        kilometer,
        meter,
        kecimeter,
        centimeter,
        millimeter,
        mile,
        inch,
        foot,
        yard,
        nautical_mile,

        // Temperature
        celsius,
        fahrenheit,
        kelvin,
        reaumur,
        newton,
        rankine,

        // Time
        year,
        quarter,
        month,
        week,
        day,
        hour,
        minute,
        second,
        millisecond,
        microsecond,
        picosecond,
        femtosecond,
        attosecond
    }

    class Units
    {
        public static List<UnitGroup> UnitGroups = Enum.GetValues(typeof(UnitGroup))
                                                       .Cast<UnitGroup>()
                                                       .ToList();

        //public static List<UnitType> DataOpts = new List<UnitType>()
        //{
        //    UnitType.bit,      // 1 bit
        //    UnitType._byte,    // 8 bits

        //    UnitType.kilobit,  // 1000 bits
        //    UnitType.megabit,
        //    UnitType.gigabit,
        //    UnitType.terabit,
        //    UnitType.petabit,
        //    UnitType.exabit,

        //    UnitType.kibibit,  // 1024 bits
        //    UnitType.mebibit,
        //    UnitType.gibibit,
        //    UnitType.tebibit,
        //    UnitType.pebibit,
        //    UnitType.exbibit,

        //    UnitType.kilobyte, // 1000 bytes
        //    UnitType.megabyte,
        //    UnitType.gibibyte,
        //    UnitType.terabyte,
        //    UnitType.petabyte,
        //    UnitType.exabyte,

        //    UnitType.kibibyte, // 1024 bytes
        //    UnitType.mebibyte,
        //    UnitType.gigabyte,
        //    UnitType.tebibyte,
        //    UnitType.pebibyte,
        //    UnitType.exbibyte,
        //};

        public static List<UnitType> TemperatureOpts = new List<UnitType>
        {
            UnitType.celsius,
            UnitType.fahrenheit,
            UnitType.kelvin,
            UnitType.reaumur,
            UnitType.newton,
            UnitType.rankine
        };

        public static List<UnitType> DistanceOpts = new List<UnitType>
        {
            UnitType.kilometer,
            UnitType.meter,
            UnitType.kecimeter,
            UnitType.centimeter,
            UnitType.millimeter,
            UnitType.mile,
            UnitType.inch,
            UnitType.foot,
            UnitType.yard,
            UnitType.nautical_mile
        };

        public static List<UnitType> TimeOpts = new List<UnitType>
        {
            UnitType.year,
            UnitType.quarter,
            UnitType.month,
            UnitType.week,
            UnitType.day,
            UnitType.hour,
            UnitType.minute,
            UnitType.second,
            UnitType.millisecond,
            UnitType.microsecond,
            UnitType.picosecond,
            UnitType.femtosecond,
            UnitType.attosecond
        };

        public static IDictionary<UnitGroup, string> UnitGroupNameMap = new Dictionary<UnitGroup, string>()
        {
            {UnitGroup.data, AppResources.data},
            {UnitGroup.distance, AppResources.distance},
            {UnitGroup.speed, AppResources.speed},
            {UnitGroup.temperature, AppResources.temperature},
            {UnitGroup.time, AppResources.time},
            {UnitGroup.weight, AppResources.weight},
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

        //public static IDictionary<UnitGroup, List<UnitType>> UnitMap = new SortedDictionary<UnitGroup, List<UnitType>>()
        //{
        //    {UnitGroup.data, DataOpts},
        //    {UnitGroup.distance, DistanceOpts},
        //    {UnitGroup.speed, TemperatureOpts},
        //    {UnitGroup.temperature, TemperatureOpts},
        //    {UnitGroup.time, TimeOpts},
        //    {UnitGroup.weight, TemperatureOpts},
        //};

        public static IDictionary<UnitGroup, Converter> ConverterMap = new Dictionary<UnitGroup, Converter>()
        {
            {UnitGroup.data, Source.Groups.Data.Instance},
            {UnitGroup.distance, Source.Groups.Data.Instance},
            {UnitGroup.speed, Source.Groups.Data.Instance},
            {UnitGroup.temperature, Source.Groups.Data.Instance},
            {UnitGroup.time, Source.Groups.Data.Instance},
            {UnitGroup.weight, Source.Groups.Data.Instance},
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
