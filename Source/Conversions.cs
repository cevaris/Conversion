using System;
using System.Collections.Generic;

namespace Conversion
{
    public class Conversions
    {
        ILogger logger = new ConsoleLogger(nameof(Conversions));

        const double KILO = 1000;
        const double MEGA = KILO * 1000;
        const double GIGA = MEGA * 1000;
        const double TERA = GIGA * 1000;
        const double PETA = TERA * 1000;
        const double EXA = PETA * 1000;

        const double KIBI = 1024;
        const double MEBI = KIBI * 1024;
        const double GIBI = MEBI * 1024;
        const double TEBI = GIBI * 1024;
        const double PEBI = TEBI * 1024;
        const double EXBI = PEBI * 1024;

        private static Conversions instance;
        public static Conversions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Conversions();
                }
                return instance;
            }
        }

        public Double Convert(UnitGroup g, UnitType inUnit, UnitType outUnit, Double x)
        {
            if (Funcs.TryGetValue(Conversions.Key(g, inUnit, outUnit), out Func<Double, Double> conversion))
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

        private Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Funcs = init();

        private static Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> init()
        {
            Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> x = new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();

            // Temperature
            x.Add(Key(UnitGroup.Temperature, UnitType.Celsius, UnitType.Fahrenheit), (a) => a * (9.0 / 5.0) + 32);

            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Celsius), (a) => (a - 32) * (5.0 / 9.0));
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Fahrenheit), identity);
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Kelvin), (a) => (a + 459.67) * (5.0 / 9.0));
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Newton), (a) => (a - 32) * (11.0 / 60.0));
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Rankine), (a) => a + 459.67);
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Reaumur), (a) => (a - 32) * (4.0 / 9.0));

            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Celsius), (a) => a - 273.15);
            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Fahrenheit), (a) => (a * (9.0 / 5.0)) - 459.67);
            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Newton), (a) => (a - 273.15) * (33.0 / 100.0));
            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Kelvin), identity);
            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Rankine), (a) => a * (9.0 / 5.0));
            x.Add(Key(UnitGroup.Temperature, UnitType.Kelvin, UnitType.Reaumur), (a) => (a - 273.15) * (4.0 / 5.0));

            // Data
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.bit), identity);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType._byte), (a) => a / 8.0);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.kilobyte), (a) => a / 8.0 / KILO);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.megabyte), (a) => a / 8.0 / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.gigabyte), (a) => a / 8.0 / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.terabyte), (a) => a / 8.0 / TERA);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.petabyte), (a) => a / 8.0 / PETA);
            x.Add(Key(UnitGroup.Data, UnitType.bit, UnitType.exabyte), (a) => a / 8.0 / EXA);

            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.bit), (a) => a * 8.0);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType._byte), identity);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.kilobyte), (a) => a / KILO);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.megabyte), (a) => a / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.gigabyte), (a) => a / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.terabyte), (a) => a / TERA);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.petabyte), (a) => a / PETA);
            x.Add(Key(UnitGroup.Data, UnitType._byte, UnitType.exabyte), (a) => a / EXA);

            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.bit), (a) => a * 8.0 * KILO);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType._byte), (a) => a * KILO);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.kilobyte), identity);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.megabyte), (a) => a * KILO / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.gigabyte), (a) => a * KILO / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.terabyte), (a) => a * KILO / TERA);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.petabyte), (a) => a * KILO / PETA);
            x.Add(Key(UnitGroup.Data, UnitType.kilobyte, UnitType.exabyte), (a) => a * KILO / EXA);

            return x;
        }

        private static Tuple<UnitGroup, UnitType, UnitType> Key(UnitGroup g, UnitType a, UnitType b)
        {
            return new Tuple<UnitGroup, UnitType, UnitType>(g, a, b);
        }

        private static Double identity(Double x)
        {
            return x;
        }
    }
}
