﻿using System;
using System.Collections.Generic;
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

            HeaderLabel.Text = unitGroup.ToString();
            SelectedUnitGroup = unitGroup;
            UnitTypes = unitTypes;

            pickerLeft.ItemsSource = UnitTypes;
            pickerLeft.SelectedIndex = 0;
            pickerRight.ItemsSource = UnitTypes;
            pickerRight.SelectedIndex = 1;

            int index = Units.UnitGroups.FindIndex(x => x == unitGroup);
            if (index == 0)
            {
                HeaderLabelPrev.Text = "";
                HeaderLabelNext.Text = $"{Units.UnitGroups[index + 1]} >";
            }
            else if (index == Units.UnitGroups.Count - 1)
            {
                HeaderLabelPrev.Text = $"< {Units.UnitGroups[index - 1]}";
                HeaderLabelNext.Text = "";
            }
            else
            {
                HeaderLabelPrev.Text = $"< {Units.UnitGroups[index - 1]}";
                HeaderLabelNext.Text = $"{Units.UnitGroups[index + 1]} >";
            }

            NumLeft.Text = "1";
            recalculate();
        }

        private void recalculate()
        {
            UnitType typeLeft = UnitTypes[pickerLeft.SelectedIndex];
            UnitType typeRight = UnitTypes[pickerRight.SelectedIndex];

            Double result = conversions.Convert(SelectedUnitGroup, typeLeft, typeRight, Convert.ToDouble(NumLeft.Text));
            NumRight.Text = Math.Round(result, 6).ToString();

            //ResultInput.Text = typeLeft.ToString();
            //ResultOutput.Text = typeRight.ToString();
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

            if (sender == HeaderLabelPrev || sender == HeaderLabelNext)
            {
                int index = Units.UnitGroups.FindIndex(x => x == SelectedUnitGroup);
                UnitGroup? requestedUnitGroup = null;
                if (sender == HeaderLabelPrev && !string.IsNullOrEmpty(HeaderLabelPrev.Text))
                {
                    requestedUnitGroup = Units.UnitGroups[index - 1];
                }

                if (sender == HeaderLabelNext && !string.IsNullOrEmpty(HeaderLabelNext.Text))
                {
                    requestedUnitGroup = Units.UnitGroups[index + 1];
                }

                if (requestedUnitGroup.HasValue)
                {
                    var message = new ScrollToUnitGroupMessage
                    {
                        SelectedItem = requestedUnitGroup.Value
                    };
                    message.Send(this);
                }

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
