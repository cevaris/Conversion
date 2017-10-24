using System;
using System.Collections.Generic;
using System.Linq;
namespace Conversion.Source.Groups
{
    public class Time : Converter
    {
        private const double DAY = HOUR * 24;
        private const double WEEK = DAY * 7;
        // Days in Year taken from https://pumas.gsfc.nasa.gov/files/04_21_97_1.pdf
        private static readonly double MONTH = DAY * 365.2422 / 12; 
        private static readonly double YEAR = MONTH * 12;
        private static readonly double DECADE = YEAR * 10;
        private static readonly double CENTERY = YEAR * 100;
        private static readonly double MILLENNIUM = YEAR * 100;

        static private ILogger logger = new ConsoleLogger(nameof(Time));

        private static Time instance;
        public static Time Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Time();
                    instance.Load();
                }
                return instance;
            }
        }
        protected override UnitGroup group()
        {
            return UnitGroup.time;
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

        private Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            TimeConfig fromType = configs.Find(x => x.Type == from);
            TimeConfig toType = configs.Find(x => x.Type == to);
            return (Double x) =>
            {
                double numerator = x * fromType.ToSeconds;
                double denominator = toType.ToSeconds;

                logger.Info($"({x}) => {numerator} to {denominator}");
                return numerator / denominator;
            };
        }

        protected override List<UnitType> types()
        {
            return configs.Select(x => x.Type).ToList();
        }

        private static List<TimeConfig> configs = new List<TimeConfig>()
        {
            TimeConfig.Create(UnitType.femtosecond, FEMTO),
            TimeConfig.Create(UnitType.picosecond, PICO),
            TimeConfig.Create(UnitType.nanosecond, NANO),
            TimeConfig.Create(UnitType.microsecond, MICRO),
            TimeConfig.Create(UnitType.millisecond, MILLI),
            TimeConfig.Create(UnitType.second, BASE),
            TimeConfig.Create(UnitType.minute, MINUTE),
            TimeConfig.Create(UnitType.hour, HOUR),
            TimeConfig.Create(UnitType.day, DAY),
            TimeConfig.Create(UnitType.week, WEEK),
            TimeConfig.Create(UnitType.fortnight, WEEK * 2),
            TimeConfig.Create(UnitType.month, MONTH),
            TimeConfig.Create(UnitType.year, YEAR),
            TimeConfig.Create(UnitType.decade, DECADE),
            TimeConfig.Create(UnitType.century, CENTERY),
            TimeConfig.Create(UnitType.millennium, MILLENNIUM),
        };
    }

    class TimeConfig
    {
        public UnitType Type { get; set; }
        public double ToSeconds { get; set; }

        public static TimeConfig Create(UnitType type, double toSeconds)
        {
            return new TimeConfig()
            {
                Type = type,
                ToSeconds = toSeconds
            };
        }
    }
}
