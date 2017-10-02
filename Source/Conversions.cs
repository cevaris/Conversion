using System;
using System.Collections.Generic;

namespace Conversion
{
    public class Conversions
    {
        ILogger logger = new ConsoleLogger(nameof(Conversions));

        readonly static double giga = Math.Pow(1000, 3);
        readonly static double yotta = Math.Pow(1000, 8);

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

        public Double Convert(UnitGroup g, UnitType a, UnitType b, Double x)
        {
            if (Funcs.TryGetValue(Conversions.Key(g, a, b), out Func<Double, Double> conversion))
            {
                return conversion(x);
            }
            else
            {
                logger.Error($"no conversion func found  for {g}:{a}:{b}");
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
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Byte), (a) => a / 8.0);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Gigabyte), (a) => a / 8.0 / giga);
            x.Add(Key(UnitGroup.Data, UnitType.Bit, UnitType.Yottabyte), (a) => a / 8.0 / yotta);

            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Bit), (a) => a * 8.0);
            x.Add(Key(UnitGroup.Data, UnitType.Byte, UnitType.Yottabyte), (a) => a / yotta);

            x.Add(Key(UnitGroup.Data, UnitType.Gigabyte, UnitType.Bit), (a) => a * 8.0 * giga);

            x.Add(Key(UnitGroup.Data, UnitType.Yottabyte, UnitType.Bit), (a) => a * 8.0 * yotta);
            x.Add(Key(UnitGroup.Data, UnitType.Yottabyte, UnitType.Byte), (a) => a / yotta);


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
