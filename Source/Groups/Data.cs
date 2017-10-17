using System;
using System.Linq;
using System.Collections.Generic;


namespace Conversion.Source.Groups
{
    public class Data : Converter
    {
        static private ILogger logger = new ConsoleLogger(nameof(Data));

        private const double BIT = 1;
        private const double BYTE = 8 * BIT;

        public const double KIBI = BASE * 1024;
        public const double MEBI = KIBI * 1024;
        public const double GIBI = MEBI * 1024;
        public const double TEBI = GIBI * 1024;
        public const double PEBI = TEBI * 1024;
        public const double EXBI = PEBI * 1024;

        private static Data instance;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                    instance.Load();
                }
                return instance;
            }
        }

        private static List<DataType> dataTypes = new List<DataType>()
        {
            DataType.Create(UnitType.bit, BASE, BIT),       // 1 bit
            DataType.Create(UnitType._byte, BASE, BYTE),    // 8 bits

            DataType.Create(UnitType.kilobit, KILO, BIT),   // 1000 bits * 1 bits
            DataType.Create(UnitType.megabit, MEGA, BIT),
            DataType.Create(UnitType.gigabit, GIGA, BIT),
            DataType.Create(UnitType.terabit, TERA, BIT),
            DataType.Create(UnitType.petabit, PETA, BIT),
            DataType.Create(UnitType.exabit, EXA, BIT),

            DataType.Create(UnitType.kibibit, KIBI, BIT),   // 1024 bits * 1 bits
            DataType.Create(UnitType.mebibit, MEBI, BIT),
            DataType.Create(UnitType.gibibit, GIBI, BIT),
            DataType.Create(UnitType.tebibit, TEBI, BIT),
            DataType.Create(UnitType.pebibit, PEBI, BIT),
            DataType.Create(UnitType.exbibit, GIBI, BIT),

            DataType.Create(UnitType.kilobyte, KILO, BYTE), // 1000 bits * 8 bits
            DataType.Create(UnitType.megabyte, MEGA, BYTE),
            DataType.Create(UnitType.gigabyte, GIGA, BYTE),
            DataType.Create(UnitType.terabyte, TERA, BYTE),
            DataType.Create(UnitType.petabyte, PETA, BYTE),
            DataType.Create(UnitType.exabyte, EXA, BYTE),

            DataType.Create(UnitType.kibibyte, KIBI, BYTE), // 1024 bits * 8 bits
            DataType.Create(UnitType.mebibyte, MEBI, BYTE),
            DataType.Create(UnitType.gibibyte, GIBI, BYTE),
            DataType.Create(UnitType.tebibyte, TEBI, BYTE),
            DataType.Create(UnitType.pebibyte, PEBI, BYTE),
            DataType.Create(UnitType.exbibyte, EXBI, BYTE),
        };

        public static List<UnitType> DataOpts = dataTypes
            .Select(x => x.Type)
            .ToList();


        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Load()
        {

            foreach (UnitType a in Units.DataOpts)
            {
                foreach (UnitType b in Units.DataOpts)
                {
                    funcs.Add(Key(UnitGroup.data, a, b), Convert(a, b));
                }
            }

            return funcs;
        }

        protected override UnitGroup group()
        {
            return UnitGroup.data;
        }

        protected override List<UnitType> types()
        {
            return Units.DataOpts;
        }

        public static Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            DataType fromType = dataTypes.Find(x => x.Type == from);
            DataType toType = dataTypes.Find(x => x.Type == to);

            logger.Info($"{fromType} to {toType}");
            return (Double x) =>
            {
                double fromBits = x * fromType.BitType * fromType.Scale;
                double toBits = x * toType.BitType * toType.Scale;

                logger.Info($"({x}) => {fromBits} to {toBits}");
                return fromBits / toBits;
            };
        }

        private static bool eq(double a, double b)
        {
            return (Math.Abs(a - b) > 0.00001);
        }

    }

    class DataType
    {
        public static DataType Create(UnitType type, double scale, double bitType)
        {
            return new DataType
            {
                Type = type,
                BitType = bitType,
                Scale = scale
            };
        }

        public UnitType Type { get; set; }
        public double BitType { get; set; }
        public double Scale { get; set; }
    }

}
