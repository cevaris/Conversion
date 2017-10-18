using System;
using System.Collections.Generic;

namespace Conversion.Source.Groups
{
    public class Speed : Converter
    {
        static private ILogger logger = new ConsoleLogger(nameof(Speed));

        private static Speed instance;
        public static Speed Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Speed();
                    instance.Load();
                }
                return instance;
            }
        }

        protected override UnitGroup group()
        {
            return UnitGroup.speed;
        }

        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<double, double>> Load()
        {
            foreach (UnitType a in types())
            {
                foreach (UnitType b in types())
                {
                    if (!funcs.ContainsKey(Key(group(), a, b)))
                    {
                        funcs.Add(Key(group(), a, b), Convert(a, b));
                    }
                }
            }
            return funcs;
        }

        protected override List<UnitType> types()
        {
            return new List<UnitType>{
                UnitType.metersPerSecond,
                UnitType.kilometersPerHour,
                UnitType.milePerHour,
                UnitType.knot,
                UnitType.feetPerSecond,
            };
        }

        private Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            return identity;
        }
    }
}
