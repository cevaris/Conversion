using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionView : CarouselPage
    {
        ILogger logger = new ConsoleLogger(nameof(ConversionView));
        
        public ConversionView()
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
