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
            pickerInput.ItemsSource = pickerList;
            pickerInput.SelectedIndex = 1;
            pickerOutput.ItemsSource = pickerList;
            pickerOutput.SelectedIndex = 0;

            NumInput.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            UnitType typeInput = UnitTypes[pickerInput.SelectedIndex];
            UnitType typeOutput = UnitTypes[pickerOutput.SelectedIndex];

            Double input = Convert.ToDouble(NumInput.Text);
            logger.Info($"parsed value: {input}");
            Double result = converter.Convert(converter.Group, typeInput, typeOutput, input);
            NumOutput.Text = result.ToString();
        }

        void OnChangedEvent(object sender, System.EventArgs e)
        {
            if (sender == SwapLabel)
            {
                var tmp = pickerInput.SelectedIndex;
                pickerInput.SelectedIndex = pickerOutput.SelectedIndex;
                pickerOutput.SelectedIndex = tmp;
                recalculate();
            }
            if (sender == NumOutput)
            {
                App.Shared.CopyToClipbard(NumOutput.Text);
                DisplayAlert("Copied", "Text is now ready to paste", "OK");
            }

            if (sender == pickerInput || sender == pickerOutput)
            {
                logger.Info($"{pickerInput.SelectedIndex} - {pickerOutput.SelectedIndex}");
                if (pickerInput.SelectedIndex >= 0 && pickerOutput.SelectedIndex >= 0)
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
                    NumInput.Text = e.OldTextValue;
                }
 
            }
        }
    }
}
