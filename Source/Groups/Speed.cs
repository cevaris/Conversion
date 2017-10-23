using System;
using System.Collections.Generic;

namespace Conversion.Source.Groups
{
    public class Speed : Converter
    {
        private static readonly double FEET = BASE * 1200 / 3937;
        private static readonly double MILE = FEET * 5280; // mile -> meter
        private static readonly double NAUTICAL_MILE = 1852; // nautical mile -> meter

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
                UnitType.metersPerSecond,   // meters seconds
                UnitType.kilometersPerHour, // 1000 meters * 3600 seconds
                UnitType.milePerHour,       // 1200/3937 * 5280 meters * 3600 seconds
                UnitType.knot,              // 1852 meters * 3600 seconds
                UnitType.feetPerSecond,     // 1200/3937 * 3600 seconds
            };
        }

        private Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            SpeedConfig fromType = speedConfigs.Find(x => x.Type == from);
            SpeedConfig toType = speedConfigs.Find(x => x.Type == to);

            return (Double x) =>
            {
                double timeNumerator = toType.TimeConverter;
                double timeDenumerator = fromType.TimeConverter;

                double unitNumerator = x * fromType.BaseConverter * fromType.Scale;
                double unitDenumerator = toType.BaseConverter * toType.Scale;

                logger.Info($"({x}) => ({x} * {unitNumerator})/{unitDenumerator} * {timeNumerator}/{timeDenumerator}");
                return (unitNumerator / unitDenumerator) * (timeNumerator / timeDenumerator);
            };
        }

        private List<SpeedConfig> speedConfigs = new List<SpeedConfig>
        {
            SpeedConfig.Create(UnitType.metersPerSecond, BASE, BASE, BASE),
            SpeedConfig.Create(UnitType.kilometersPerHour, KILO, BASE, HOUR),
            SpeedConfig.Create(UnitType.milePerHour, BASE, MILE, HOUR),
            SpeedConfig.Create(UnitType.knot, BASE, NAUTICAL_MILE, HOUR),
            SpeedConfig.Create(UnitType.feetPerSecond, BASE, FEET, BASE),
        };

    }

    class SpeedConfig
    {
        public UnitType Type { get; set; }
        public double BaseConverter { get; set; } // * -> meter
        public double TimeConverter { get; set; } // * -> seconds
        public double Scale { get; set; } // Kilo

        public static SpeedConfig Create(UnitType type, double scale, double baseConverter, double timeConverter)
        {
            return new SpeedConfig
            {
                Type = type,
                BaseConverter = baseConverter,
                TimeConverter = timeConverter,
                Scale = scale,
            };
        }

    }
}
