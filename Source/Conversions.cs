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
        const double ZETTA = EXA * 1000;
        const double YOTTA = ZETTA * 1000;

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
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Bit), identity);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Byte), (a) => a / 8.0);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Kilobyte), (a) => a / 8.0 / KILO);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Megabyte), (a) => a / 8.0 / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Gigabyte), (a) => a / 8.0 / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Terabyte), (a) => a / 8.0 / TERA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Petabyte), (a) => a / 8.0 / PETA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Exabyte), (a) => a / 8.0 / EXA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Zettabyte), (a) => a / 8.0 / ZETTA);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Yottabyte), (a) => a / 8.0 / YOTTA);

            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Bit), (a) => a * 8.0);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Byte), identity);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Kilobyte), (a) => a / KILO);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Megabyte), (a) => a / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Gigabyte), (a) => a / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Terabyte), (a) => a / TERA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Petabyte), (a) => a / PETA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Exabyte), (a) => a / EXA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Zettabyte), (a) => a / ZETTA);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Yottabyte), (a) => a / YOTTA);

            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Bit), (a) => a * 8.0 * KILO);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Byte), (a) => a * KILO);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Kilobyte), identity);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Megabyte), (a) => a * KILO / MEGA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Gigabyte), (a) => a * KILO / GIGA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Terabyte), (a) => a * KILO / TERA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Petabyte), (a) => a * KILO / PETA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Exabyte), (a) => a * KILO / EXA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Zettabyte), (a) => a * KILO / ZETTA);
            x.Add(Key(UnitGroup.Data, UnitType.Kilobyte, UnitType.Yottabyte), (a) => a * KILO / YOTTA);



            x.Add(Key(UnitGroup.Data, UnitType.Gigabyte, UnitType.Bit), (a) => a * 8.0 * YOTTA);

            x.Add(Key(UnitGroup.Data, UnitType.Yottabyte, UnitType.Bit), (a) => a * 8.0 * YOTTA);
            x.Add(Key(UnitGroup.Data, UnitType.Yottabyte, UnitType.Byte), (a) => a / YOTTA);


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
