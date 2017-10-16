using System;
using System.Collections.Generic;

namespace Conversion.Source.Groups
{
    public class Data : Converter
    {
        private static Data instance;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }

        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Load() {
            Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> x = new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();
            /*
          UnitType.bit,
          UnitType._byte,

          UnitType.kilobit,
          UnitType.megabit,
          UnitType.gigabit,
          UnitType.terabit,
          UnitType.petabit,
          UnitType.exabit,

          UnitType.kibibit,
          UnitType.mebibit,
          UnitType.gibibit,
          UnitType.tebibit,
          UnitType.pebibit,
          UnitType.exbibit,

          UnitType.kilobyte,
          UnitType.megabyte,
          UnitType.gigabyte,
          UnitType.terabyte,
          UnitType.petabyte,
          UnitType.exabyte,

          UnitType.kibibyte,
          UnitType.mebibyte,
          UnitType.gibibyte,
          UnitType.tebibyte,
          UnitType.pebibyte,
          UnitType.exbibyte,
           */
            // Data
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.kilobyte), (a) => a / 8.0 / KILO);
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.megabyte), (a) => a / 8.0 / MEGA);
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.gigabyte), (a) => a / 8.0 / GIGA);
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.terabyte), (a) => a / 8.0 / TERA);
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.petabyte), (a) => a / 8.0 / PETA);
            x.Add(Key(UnitGroup.data, UnitType.bit, UnitType.exabyte), (a) => a / 8.0 / EXA);

            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.bit), (a) => a * 8.0);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType._byte), identity);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.kilobyte), (a) => a / KILO);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.megabyte), (a) => a / MEGA);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.gigabyte), (a) => a / GIGA);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.terabyte), (a) => a / TERA);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.petabyte), (a) => a / PETA);
            x.Add(Key(UnitGroup.data, UnitType._byte, UnitType.exabyte), (a) => a / EXA);

            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.bit), (a) => a * 8.0 * KILO);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType._byte), (a) => a * KILO);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.kilobyte), identity);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.megabyte), (a) => a * KILO / MEGA);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.gigabyte), (a) => a * KILO / GIGA);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.terabyte), (a) => a * KILO / TERA);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.petabyte), (a) => a * KILO / PETA);
            x.Add(Key(UnitGroup.data, UnitType.kilobyte, UnitType.exabyte), (a) => a * KILO / EXA);

            return x;
        }

        protected override UnitGroup group()
        {
            return UnitGroup.data;
        }

        protected override List<UnitType> types()
        {
            return Units.DataOpts;
        }
    }
}
