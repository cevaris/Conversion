using System;
using System.Collections.Generic;

namespace Conversion
{
    public abstract class Converter
    {
        public const double BASE = 1;

        public static readonly double FEMTO = Math.Pow(10, -15);
        public static readonly double PICO = Math.Pow(10, -12);
        public static readonly double NANO = Math.Pow(10, -9);
        public static readonly double MICRO = Math.Pow(10, -6);
        public static readonly double MILLI = Math.Pow(10, -3);
        public static readonly double CENTI = Math.Pow(10, -2);
        public static readonly double DECI = Math.Pow(10, -1);

        public static readonly double DECA = Math.Pow(10, 1);
        public static readonly double HECTO = Math.Pow(10, 2);
        public static readonly double KILO = Math.Pow(10, 3);
        public static readonly double MEGA = Math.Pow(10, 6);
        public static readonly double GIGA = Math.Pow(10, 9);
        public static readonly double TERA = Math.Pow(10, 12);
        public static readonly double PETA = Math.Pow(10, 15);
        public static readonly double EXA = Math.Pow(10, 18);

        public const double MINUTE = BASE * 60;
        public const double HOUR = MINUTE * 60;

        static private ILogger logger = new ConsoleLogger(nameof(Converter));

        protected Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> funcs = new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();
        public Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Funcs
        {
            get
            {
                if (funcs == null)
                {
                    Load();
                }
                return funcs;
            }
        }

        public UnitGroup Group
        {
            get
            {
                return group();
            }
        }

        public List<UnitType> Types
        {
            get
            {
                return types();
            }
        }

        public Double Convert(UnitGroup g, UnitType inUnit, UnitType outUnit, Double x)
        {
            if (Funcs.TryGetValue(Converter.Key(g, inUnit, outUnit), out Func<Double, Double> conversion))
            {
                logger.Info($"looking up conversion: {g}:{inUnit}:{outUnit}");
                return conversion(x);
            }
            else
            {
                logger.Error($"no conversion func found for: {g}:{inUnit}:{outUnit}");
                return identity(x);
            }
        }

        protected static Tuple<UnitGroup, UnitType, UnitType> Key(UnitGroup g, UnitType a, UnitType b)
        {
            return new Tuple<UnitGroup, UnitType, UnitType>(g, a, b);
        }

        protected static Double identity(Double x)
        {
            return x;
        }

        protected abstract Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Load();
        protected abstract List<UnitType> types();
        protected abstract UnitGroup group();

        //private static Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> init();
        //{
        //    Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> x = new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();

        //    // Temperature
        //    x.Add(Key(UnitGroup.temperature, UnitType.celsius, UnitType.fahrenheit), (a) => a * (9.0 / 5.0) + 32);

        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.celsius), (a) => (a - 32) * (5.0 / 9.0));
        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.fahrenheit), identity);
        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.kelvin), (a) => (a + 459.67) * (5.0 / 9.0));
        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.newton), (a) => (a - 32) * (11.0 / 60.0));
        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.rankine), (a) => a + 459.67);
        //    x.Add(Key(UnitGroup.temperature, UnitType.fahrenheit, UnitType.reaumur), (a) => (a - 32) * (4.0 / 9.0));

        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.celsius), (a) => a - 273.15);
        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.fahrenheit), (a) => (a * (9.0 / 5.0)) - 459.67);
        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.newton), (a) => (a - 273.15) * (33.0 / 100.0));
        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.kelvin), identity);
        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.rankine), (a) => a * (9.0 / 5.0));
        //    x.Add(Key(UnitGroup.temperature, UnitType.kelvin, UnitType.reaumur), (a) => (a - 273.15) * (4.0 / 5.0));


        //    ///*
        //    //UnitType.bit,
        //    //UnitType._byte,

        //    //UnitType.kilobit,
        //    //UnitType.megabit,
        //    //UnitType.gigabit,
        //    //UnitType.terabit,
        //    //UnitType.petabit,
        //    //UnitType.exabit,

        //    //UnitType.kibibit,
        //    //UnitType.mebibit,
        //    //UnitType.gibibit,
        //    //UnitType.tebibit,
        //    //UnitType.pebibit,
        //    //UnitType.exbibit,

        //    //UnitType.kilobyte,
        //    //UnitType.megabyte,
        //    //UnitType.gigabyte,
        //    //UnitType.terabyte,
        //    //UnitType.petabyte,
        //    //UnitType.exabyte,

        //    //UnitType.kibibyte,
        //    //UnitType.mebibyte,
        //    //UnitType.gibibyte,
        //    //UnitType.tebibyte,
        //    //UnitType.pebibyte,
        //    //UnitType.exbibyte,
        //    // */
        //    //// Data
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.bit), identity);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType._byte), (a) => a / 8.0);

        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.kilobit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.megabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.gigabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.terabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.petabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.exabit), (a) => a);

        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.kibibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.megabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.gigabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.tebibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.pebibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.exabit), (a) => a);

        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.kilobyte), (a) => a / 8.0 / KILO);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.megabyte), (a) => a / 8.0 / MEGA);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.gigabyte), (a) => a / 8.0 / GIGA);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.terabyte), (a) => a / 8.0 / TERA);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.petabyte), (a) => a / 8.0 / PETA);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.exabyte), (a) => a / 8.0 / EXA);

        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.kibibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.megabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.gigabit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.tebibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.pebibit), (a) => a);
        //    //x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.exabit), (a) => a);

        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.bit), (a) => a * 8.0);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType._byte), identity);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.kilobyte), (a) => a / KILO);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.megabyte), (a) => a / MEGA);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.gigabyte), (a) => a / GIGA);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.terabyte), (a) => a / TERA);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.petabyte), (a) => a / PETA);
        //    //x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.exabyte), (a) => a / EXA);

        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.bit), (a) => a * 8.0 * KILO);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType._byte), (a) => a * KILO);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.kilobyte), identity);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.megabyte), (a) => a * KILO / MEGA);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.gigabyte), (a) => a * KILO / GIGA);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.terabyte), (a) => a * KILO / TERA);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.petabyte), (a) => a * KILO / PETA);
        //    //x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.exabyte), (a) => a * KILO / EXA);

        //    return x;
        //}

    }
}
