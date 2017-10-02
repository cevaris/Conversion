using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Conversion
{
    public partial class UnitPage : ContentPage
    {
        public List<UnitType> UnitTypes { get; set; }

        public UnitGroup SelectedUnitGroup { get; set; }

        private static ILogger logger = new ConsoleLogger(nameof(UnitPage));

        private static Conversions conversions = Conversions.Instance;

        public UnitPage()
        {
            InitializeComponent();
        }

        public UnitPage(UnitGroup unitGroup, List<UnitType> unitTypes)
        {
            InitializeComponent();

            SelectedUnitGroup = unitGroup;
            Title = Units.T(SelectedUnitGroup);
            UnitTypes = unitTypes;

            pickerLeft.ItemsSource = UnitTypes.Select(x => $"{Units.T(x)} ({Units.TAbbr(x)})").ToList();
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = UnitTypes.Select(x => $"{Units.T(x)} ({Units.TAbbr(x)})").ToList();
            pickerRight.SelectedIndex = 1;

            NumLeft.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            UnitType typeLeft = UnitTypes[pickerLeft.SelectedIndex];
            UnitType typeRight = UnitTypes[pickerRight.SelectedIndex];

            Double input = Convert.ToDouble(NumLeft.Text);
            logger.Info($"parsed value: {input}");
            Double result = conversions.Convert(SelectedUnitGroup, typeLeft, typeRight, input);
            //NumRight.Text = Math.Round(result, 6).ToString();
            NumRight.Text = result.ToString();

            ResultInput.Text = Units.TAbbr(typeLeft);
            ResultOutput.Text = Units.TAbbr(typeRight);
        }

        void OnChangedEvent(object sender, System.EventArgs e)
        {
            if (sender == SwapLabel)
            {
                var tmp = pickerLeft.SelectedIndex;
                pickerLeft.SelectedIndex = pickerRight.SelectedIndex;
                pickerRight.SelectedIndex = tmp;
                recalculate();
            }

            if (sender == pickerLeft || sender == pickerRight)
            {
                logger.Info($"{pickerLeft.SelectedIndex} - {pickerRight.SelectedIndex}");
                if (pickerLeft.SelectedIndex >= 0 && pickerRight.SelectedIndex >= 0)
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
