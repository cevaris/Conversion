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
        femtometer,
        picometer,
        nanometer,
        micrometer,
        millimeter,
        centimeter,
        decimeter,
        meter,
        decameter,
        hectometer,
        kilometer,
        megameter,
        gigameter,

        inch,
        foot,
        yard,
        mile,
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

        public static List<UnitType> TemperatureOpts = new List<UnitType>
        {
            UnitType.celsius,
            UnitType.fahrenheit,
            UnitType.kelvin,
            UnitType.reaumur,
            UnitType.newton,
            UnitType.rankine
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

            {UnitType.femtometer, "fm"},
            {UnitType.picometer, "pm"},
            {UnitType.nanometer, "nm"},
            {UnitType.micrometer, "µm"},
            {UnitType.millimeter, "mm"},
            {UnitType.centimeter, "cm"},
            {UnitType.decimeter, "dm"},
            {UnitType.meter, "m"},
            {UnitType.decameter, "dam"},
            {UnitType.hectometer, "hm"},
            {UnitType.kilometer, "km"},
            {UnitType.megameter, "Mm"},
            {UnitType.gigameter, "Gm"},

            {UnitType.inch, "inf"},
            {UnitType.foot, "ft"},
            {UnitType.yard, "yd"},
            {UnitType.mile, "mi"},
            {UnitType.nautical_mile, "n.m."},
        };

        public static IDictionary<UnitGroup, Converter> ConverterMap = new Dictionary<UnitGroup, Converter>()
        {
            {UnitGroup.data, Source.Groups.Data.Instance},
            {UnitGroup.distance, Source.Groups.Distance.Instance},
            {UnitGroup.speed, Source.Groups.Data.Instance},
            {UnitGroup.temperature, Source.Groups.Data.Instance},
            {UnitGroup.time, Source.Groups.Data.Instance},
            {UnitGroup.weight, Source.Groups.Data.Instance},
        };

        public static string T(UnitGroup unitGroup)
        {
            string result = AppResources.ResourceManager.GetString(unitGroup.ToString(), App.CurrentCultureInfo);
            if (!string.IsNullOrWhiteSpace(result))
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
            string result = AppResources.ResourceManager.GetString(unitType.ToString(), App.CurrentCultureInfo);
            if (!string.IsNullOrWhiteSpace(result))
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
