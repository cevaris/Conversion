using System;
using System.Linq;
using System.Collections.Generic;


namespace Conversion.Source.Groups
{
    public class Data : Converter
    {
        static private ILogger logger = new ConsoleLogger(nameof(Data));

        private static Data instance;

        private const double BIT = 1;
        private const double BYTE = 8 * BIT;

        private const double KIBI = BASE * 1024;
        private const double MEBI = KIBI * 1024;
        private const double GIBI = MEBI * 1024;
        private const double TEBI = GIBI * 1024;
        private const double PEBI = TEBI * 1024;
        private const double EXBI = PEBI * 1024;

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

        private static List<UnitType> DataOpts = dataTypes
           .Select(x => x.Type)
           .ToList();

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


        protected override Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> Load()
        {
            foreach (UnitType a in types())
            {
                foreach (UnitType b in types())
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
            return DataOpts;
        }

        public static Func<Double, Double> Convert(UnitType from, UnitType to)
        {
            DataType fromType = dataTypes.Find(x => x.Type == from);
            DataType toType = dataTypes.Find(x => x.Type == to);

            return (Double x) =>
            {
                double fromBits = x * fromType.BitOrByte * fromType.Scale;
                double toBits = x * toType.BitOrByte * toType.Scale;

                logger.Info($"({x}) => {fromBits} to {toBits}");
                return fromBits / toBits;
            };
        }
    }

    class DataType
    {
        public static DataType Create(UnitType type, double scale, double bitOrByte)
        {
            return new DataType
            {
                Type = type,
                BitOrByte = bitOrByte,
                Scale = scale
            };
        }

        public UnitType Type { get; set; }
        public double BitOrByte { get; set; }
        public double Scale { get; set; }
    }

}
