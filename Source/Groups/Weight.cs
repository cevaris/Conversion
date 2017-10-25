using System;
using System.Collections.Generic;
using System.Linq;
namespace Conversion.Source.Groups
{
    public class Weight : Converter
    {
        static private ILogger logger = new ConsoleLogger(nameof(Weight));

        private const double POUND = BASE * 453.592909436;
        private const double CARAT = BASE / 5;
        private const double GRAIN = POUND / 7000;
        private const double DRAM = POUND / 256;
        private const double OUNCE = POUND / 16;
        private const double STONE = POUND * 14;
        private const double US_TON = POUND * 2000;
        private const double IMPERIAL_TON = POUND * 2240;

        private static Weight instance;
        public static Weight Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Weight();
                    instance.Load();
                }
                return instance;
            }
        }

        protected override UnitGroup group()
        {
            return UnitGroup.weight;
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
            return configs.Select(x => x.Type).ToList();
        }

        private static List<Config> configs = new List<Config>()
        {
            Config.Create(UnitType.microgram, MICRO),
            Config.Create(UnitType.milligram, MILLI),
            Config.Create(UnitType.centigram, CENTI),
            Config.Create(UnitType.decigram, DECI),
            Config.Create(UnitType.carat, CARAT),
            Config.Create(UnitType.gram, BASE),
            Config.Create(UnitType.decagram, DECA),
            Config.Create(UnitType.hectogram, HECTO),
            Config.Create(UnitType.kilogram, KILO),
            Config.Create(UnitType.megagram_metric_ton, MEGA),

            Config.Create(UnitType.grain, GRAIN),
            Config.Create(UnitType.dram, DRAM),
            Config.Create(UnitType.ounce, OUNCE),
            Config.Create(UnitType.pound, POUND),
            Config.Create(UnitType.stone, STONE),
            Config.Create(UnitType.us_ton, US_TON),
            Config.Create(UnitType.imperial_ton, IMPERIAL_TON),
        };

        private Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            Config fromType = configs.Find(x => x.Type == from);
            Config toType = configs.Find(x => x.Type == to);
            return (Double x) =>
            {
                double numerator = x * fromType.ToGram;
                double denominator = toType.ToGram;

                logger.Info($"({x}) => {numerator} to {denominator}");
                return numerator / denominator;
            };
        }
    }

    class Config
    {
        public UnitType Type { get; set; }
        public double ToGram { get; set; }

        public static Config Create(UnitType type, double toGram)
        {
            return new Config()
            {
                Type = type,
                ToGram = toGram
            };
        }
    }
}
