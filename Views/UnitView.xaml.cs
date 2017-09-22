﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class UnitPage : ContentPage
    {
        List<UnitType> UnitTypes { get; set; }

        UnitGroup SelectedUnitGroup { get; set; }

        private static ILogger logger = new ConsoleLogger(nameof(UnitPage));

        private static Conversions conversions = Conversions.Instance;

        public UnitPage()
        {
            InitializeComponent();
        }

        public UnitPage(UnitGroup unitGroup, List<UnitType> unitTypes)
        {
            InitializeComponent();

            HeaderLabel.Text = unitGroup.ToString();
            SelectedUnitGroup = unitGroup;
            UnitTypes = unitTypes;

            pickerLeft.ItemsSource = UnitTypes;
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = UnitTypes;
            pickerRight.SelectedIndex = 1;

            NumLeft.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            UnitType typeLeft = UnitTypes[pickerLeft.SelectedIndex];
            UnitType typeRight = UnitTypes[pickerRight.SelectedIndex];

            Double result = conversions.Convert(SelectedUnitGroup, typeLeft, typeRight, Convert.ToDouble(NumLeft.Text));
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

            if (sender == pickerLeft || sender == pickerRight)
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