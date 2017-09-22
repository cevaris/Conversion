using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionPage : CarouselPage
    {
        ILogger logger = new ConsoleLogger(nameof(ConversionPage));

        public ConversionPage()
        {
            InitializeComponent();

            Children.Add(new UnitPage(UnitGroup.Distance, Units.DistanceOpts));
            Children.Add(new UnitPage(UnitGroup.Data, Units.DistanceOpts));
            Children.Add(new UnitPage(UnitGroup.Speed, Units.DistanceOpts));
            Children.Add(new UnitPage(UnitGroup.Temperature, Units.TemperatureOpts));
            Children.Add(new UnitPage(UnitGroup.Time, Units.DistanceOpts));
            Children.Add(new UnitPage(UnitGroup.Weight, Units.DistanceOpts));
        }
    }
}
