using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conversion
{
    public partial class MainPage : ContentPage
    {

        ILogger logger = new ConsoleLogger(nameof(ConversionView));

        public MainPage()
        {
            InitializeComponent();

            //TestThis.Children.Add(new UnitPage(UnitGroup.Distance, Units.DistanceOpts));
            //TestThis.Children.Add(new UnitPage(UnitGroup.Data, Units.DistanceOpts));
            //TestThis.Children.Add(new UnitPage(UnitGroup.Speed, Units.DistanceOpts));
            //TestThis.Children.Add(new UnitPage(UnitGroup.Temperature, Units.TemperatureOpts));
            //TestThis.Children.Add(new UnitPage(UnitGroup.Time, Units.DistanceOpts));
            //TestThis.Children.Add(new UnitPage(UnitGroup.Weight, Units.DistanceOpts));

        }
    }
}
