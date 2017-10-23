using System;
using System.Linq;
using System.Collections.Generic;

namespace Conversion.Source.Groups

{
    public class Tempurature : Converter
    {
        private static ILogger logger = new ConsoleLogger(nameof(Tempurature));

        const double K = 273.15;

        private static Tempurature instance;
        public static Tempurature Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tempurature();
                    instance.Load();
                }
                return instance;
            }
        }

        protected override UnitGroup group()
        {
            return UnitGroup.temperature;
        }

        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<double, double>> Load()
        {
            funcs.Add(Key(group(), UnitType.celsius, UnitType.celsius), identity);
            funcs.Add(Key(group(), UnitType.celsius, UnitType.fahrenheit), x => x * 9.0 / 5.0 + 32);
            funcs.Add(Key(group(), UnitType.celsius, UnitType.kelvin), x => x + K);

            funcs.Add(Key(group(), UnitType.fahrenheit, UnitType.celsius), x => (x - 32) / (9.0 / 5.0));
            funcs.Add(Key(group(), UnitType.fahrenheit, UnitType.fahrenheit), identity);
            funcs.Add(Key(group(), UnitType.fahrenheit, UnitType.kelvin), x => (x - 32) * 5.0 / 9.0 + K);

            funcs.Add(Key(group(), UnitType.kelvin, UnitType.celsius), x => x - K);
            funcs.Add(Key(group(), UnitType.kelvin, UnitType.fahrenheit), x => (x - K) * 1.8 + 32);
            funcs.Add(Key(group(), UnitType.kelvin, UnitType.kelvin), identity);

            return funcs;
        }

        protected override List<UnitType> types()
        {
            return new List<UnitType>() {
                UnitType.celsius,
                UnitType.fahrenheit,
                UnitType.kelvin,
            };
        }

    }
}