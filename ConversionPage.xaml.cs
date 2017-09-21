using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionPage : ContentPage
    {
        List<UnitType> TempUnits { get; set; }


        private ILogger logger = new ConsoleLogger(nameof(ConversionPage));

        enum UnitGroup
        {
            Distance,
            Temperature
        };

        enum UnitType
        {
            Celsius,
            Fahrenheit,
            Kelvin,
            Reaumur,
            Newton,
            Rankine
        };

        Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>> conversions =
                new Dictionary<Tuple<UnitGroup, UnitType, UnitType>, Func<Double, Double>>();


        public ConversionPage()
        {
            InitializeComponent();

            conversions.Add(CreateKey(UnitGroup.Temperature, UnitType.Celsius, UnitType.Fahrenheit), (a) => a * (9.0 / 5.0) + 32);
            conversions.Add(CreateKey(UnitGroup.Temperature, UnitType.Fahrenheit, UnitType.Celsius), (a) => (a - 32) * (5.0 / 9.0));

            TempUnits = new List<UnitType>
            {
                UnitType.Celsius,
                UnitType.Fahrenheit,
                UnitType.Kelvin,
                UnitType.Reaumur,
                UnitType.Newton,
                UnitType.Rankine
            };

            pickerLeft.ItemsSource = TempUnits;
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = TempUnits;
            pickerRight.SelectedIndex = 1;

            NumLeft.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            logger.Info($"{pickerLeft.SelectedIndex}:{pickerRight.SelectedIndex}");
            UnitType typeLeft = TempUnits[pickerLeft.SelectedIndex];
            UnitType typeRight = TempUnits[pickerRight.SelectedIndex];

            if (conversions.TryGetValue(CreateKey(UnitGroup.Temperature, typeLeft, typeRight), out Func<Double, Double> conversion))
            {
                NumRight.Text = Math.Round(conversion(Convert.ToDouble(NumLeft.Text)), 6).ToString();
            }
        }

        void OnClicked(object sender, System.EventArgs e)
        {
            if (sender == SwapButton)
            {
                var tmp = pickerLeft.SelectedIndex;
                pickerLeft.SelectedIndex = pickerRight.SelectedIndex;
                pickerRight.SelectedIndex = tmp;
                recalculate();
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

        private Tuple<UnitGroup, UnitType, UnitType> CreateKey(UnitGroup g, UnitType a, UnitType b)
        {
            return new Tuple<UnitGroup, UnitType, UnitType>(g, a, b);
        }
    }
}
