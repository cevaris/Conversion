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

            Children.Add(new UnitPage(UnitGroup.Temperature, Units.TemperatureOptions));
            Children.Add(new UnitPage(UnitGroup.Distance, Units.TemperatureOptions));
        }
    }
}
