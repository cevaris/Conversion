using System;
using System.Collections.Generic;

namespace Conversion.Source.Groups
{
    public class Distance : Converter
    {
        static private ILogger logger = new ConsoleLogger(nameof(Data));

        const double METER = 1;
        const double INCH = 0.0254; // inches in 1 meter

        const double FOOT = BASE * 12;
        const double YARD = FOOT * 3;
        const double MILE = FOOT * 5280;
        const double NAUTICAL_MILE = BASE * 1852;

        private static Distance instance;
        public static Distance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Distance();
                    instance.Load();
                }
                return instance;
            }
        }

        protected override UnitGroup group()
        {
            return UnitGroup.distance;
        }

        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<double, double>> Load()
        {
            foreach (UnitType a in types())
            {
                foreach (UnitType b in types())
                {
                    funcs.Add(Key(UnitGroup.distance, a, b), Convert(a, b));
                }
            }
            return funcs;
        }

        protected override List<UnitType> types()
        {
            return DistanceOpts;
        }

        public static List<UnitType> DistanceOpts = new List<UnitType>
        {
            UnitType.femtometer,
            UnitType.picometer,
            UnitType.nanometer,
            UnitType.micrometer,
            UnitType.millimeter,
            UnitType.centimeter,
            UnitType.decimeter,
            UnitType.meter,
            UnitType.decameter,
            UnitType.hectometer,
            UnitType.kilometer,
            UnitType.megameter,
            UnitType.gigameter,

            UnitType.inch,
            UnitType.foot,
            UnitType.yard,
            UnitType.mile,
            UnitType.nautical_mile,
        };

        private static List<DistanceConfig> distancConfigs = new List<DistanceConfig>
        {
            DistanceConfig.Create(UnitType.femtometer, FEMTO, METER),
            DistanceConfig.Create(UnitType.picometer, PICO, METER),
            DistanceConfig.Create(UnitType.nanometer, NANO, METER),
            DistanceConfig.Create(UnitType.micrometer, MICRO, METER),
            DistanceConfig.Create(UnitType.millimeter, MILLI, METER),
            DistanceConfig.Create(UnitType.picometer, PICO, METER),
            DistanceConfig.Create(UnitType.centimeter, CENTI, METER),
            DistanceConfig.Create(UnitType.decimeter, DECI, METER),
            DistanceConfig.Create(UnitType.meter, BASE, METER),
            DistanceConfig.Create(UnitType.decameter, DECA, METER),
            DistanceConfig.Create(UnitType.hectometer, HECTO, METER),
            DistanceConfig.Create(UnitType.kilometer, KILO, METER),
            DistanceConfig.Create(UnitType.megameter, MEGA, METER),
            DistanceConfig.Create(UnitType.gigameter, GIGA, METER),
            DistanceConfig.Create(UnitType.nautical_mile, NAUTICAL_MILE, METER),

            DistanceConfig.Create(UnitType.inch, BASE, INCH),
            DistanceConfig.Create(UnitType.foot, FOOT, INCH),
            DistanceConfig.Create(UnitType.yard, YARD, INCH),
            DistanceConfig.Create(UnitType.mile, MILE, INCH),
        };

        private static Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            DistanceConfig fromType = distancConfigs.Find(x => x.Type == from);
            DistanceConfig toType = distancConfigs.Find(x => x.Type == to);

            return (Double x) =>
            {
                double numerator = x * fromType.MeterOrInch * fromType.Scale;
                double denominator = toType.MeterOrInch * toType.Scale;

                logger.Info($"({x}) => {numerator} to {denominator}");
                return numerator / denominator;
            };
        }
    }

    class DistanceConfig
    {
        public UnitType Type { get; set; }
        public double MeterOrInch { get; set; } // meter/inch
        public double Scale { get; set; } // femto/pico/giga

        public static DistanceConfig Create(UnitType type, double scale, double meterOrInch)
        {
            return new DistanceConfig
            {
                Type = type,
                MeterOrInch = meterOrInch,
                Scale = scale
            };
        }

    }
}
