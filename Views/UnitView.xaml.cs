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

        private static ILogger logger = new ConsoleLogger(nameof(UnitPage));

        private Converter converter;

        public UnitPage()
        {
            InitializeComponent();
        }

        public UnitPage(Converter converter)
        {
            this.converter = converter;

            InitializeComponent();

            Title = Units.T(converter.Group);
            UnitTypes = converter.Types;

            List<string> pickerList = UnitTypes.Select(x => $"{Units.T(x)} ({Units.TAbbr(x)})").ToList();
            pickerLeft.ItemsSource = pickerList;
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = pickerList;
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
            Double result = converter.Convert(converter.Group, typeLeft, typeRight, input);
            NumRight.Text = result.ToString();

            //ResultInput.Text = Units.TAbbr(typeLeft);
            //ResultOutput.Text = Units.TAbbr(typeRight);
        }

        void OnChangedEvent(object sender, System.EventArgs e)
        {
            //if (sender == SwapLabel)
            //{
            //    var tmp = pickerLeft.SelectedIndex;
            //    pickerLeft.SelectedIndex = pickerRight.SelectedIndex;
            //    pickerRight.SelectedIndex = tmp;
            //    recalculate();
            //}

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
                try
                {
                    Convert.ToDouble(e.NewTextValue);
                    recalculate();
                }
                catch (FormatException ex)
                {
                    logger.Error($"error formatting {e.NewTextValue}", ex);
                    NumLeft.Text = e.OldTextValue;
                }
 
            }
        }
    }
}
