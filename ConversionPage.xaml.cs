using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionPage : ContentPage
    {
        List<UnitType> TempUnits { get; set; }

        private static ILogger logger = new ConsoleLogger(nameof(ConversionPage));

        private static Conversions conversions = Conversions.Instance;

        public ConversionPage()
        {
            InitializeComponent();

            TempUnits = Units.TemperatureOptions;

            pickerLeft.ItemsSource = TempUnits;
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = TempUnits;
            pickerRight.SelectedIndex = 1;

            NumLeft.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            UnitType typeLeft = TempUnits[pickerLeft.SelectedIndex];
            UnitType typeRight = TempUnits[pickerRight.SelectedIndex];

            Double result = conversions.Convert(UnitGroup.Temperature, typeLeft, typeRight, Convert.ToDouble(NumLeft.Text));
            NumRight.Text = Math.Round(result, 6).ToString();
        }

        void OnChangedEvent(object sender, System.EventArgs e)
        {
            if (sender == SwapButton)
            {
                var tmp = pickerLeft.SelectedIndex;
                pickerLeft.SelectedIndex = pickerRight.SelectedIndex;
                pickerRight.SelectedIndex = tmp;
                recalculate();
            }

            if(sender == pickerLeft || sender == pickerRight)
            {
                Picker picker = sender as Picker;
                if (picker.SelectedIndex > 0)
                {
                    recalculate();
                }
            }
        }

        void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string text = ((Entry)sender).Text;
            if (!String.IsNullOrEmpty(text))
            {
                recalculate();
            }
        }
    }
}
