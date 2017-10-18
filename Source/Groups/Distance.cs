using System;
using System.Collections.Generic;

namespace Conversion.Source.Groups
{
    public class Distance : Converter
    {
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
    }
}
