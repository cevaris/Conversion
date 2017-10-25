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
    }
}
