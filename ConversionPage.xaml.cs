using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionPage : CarouselPage
    {
        public ConversionPage()
        {
            InitializeComponent();

            ItemsSource = new UnitGroup[] {
                UnitGroup.Distance,
                UnitGroup.Temperature
            };

            ItemTemplate = new DataTemplate(() =>
            {
                return new UnitPage();
            });

            //ItemsSource = new List<ContentPage>(){
            //    new UnitPage()
            //};
        }
    }
}
