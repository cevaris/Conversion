using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionView : ContentView
    {
        ILogger logger = new ConsoleLogger(nameof(ConversionView));

        public List<UnitPage> MyDataSource { get; set; }

        public ConversionView()
        {
            InitializeComponent();

            MyDataSource = new List<UnitPage>();

            MyDataSource.Add(new UnitPage(UnitGroup.Distance, Units.DistanceOpts));
            MyDataSource.Add(new UnitPage(UnitGroup.Data, Units.DistanceOpts));
            MyDataSource.Add(new UnitPage(UnitGroup.Speed, Units.DistanceOpts));
            MyDataSource.Add(new UnitPage(UnitGroup.Temperature, Units.TemperatureOpts));
            MyDataSource.Add(new UnitPage(UnitGroup.Time, Units.DistanceOpts));
            MyDataSource.Add(new UnitPage(UnitGroup.Weight, Units.DistanceOpts));  


        }
    }
}
