using System;
using System.Collections.Generic;

namespace Conversion
{
    public class Conversions
    {
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
            if (Funcs.TryGetValue(Conversions.Key(UnitGroup.Temperature, a, b), out Func<Double, Double> conversion))
            {
                return conversion(x);
            }
            else
            {
                throw new Exception($"no conversion func found  for {g}:{a}:{b}");
            }
        }

        private Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Funcs = init();

        private static Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> init()
        {
            Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> x = new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();

            x.Add(Key(UnitGroup.Temperature, UnitType.Celsius, UnitType.Fahrenheit), (a) => a * (9.0 / 5.0) + 32);
            x.Add(Key(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Celsius), (a) => (a - 32) * (5.0 / 9.0));

            return x;
        }

        private static Tuple<UnitGroup, UnitType, UnitType> Key(UnitGroup g, UnitType a, UnitType b)
        {
            return new Tuple<UnitGroup, UnitType, UnitType>(g, a, b);
        }
    }
}
