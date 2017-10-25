using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Conversion.Models;
using Xamarin.Forms;

namespace Conversion.Views
{
    public partial class MainPage : ContentPage
    {
        private ILogger logger = new ConsoleLogger(nameof(MainPage));

        public ObservableCollection<UnitGroupModel> UnitGroupItemsSource { get; set; }

        public MainPage()
        {
            InitializeComponent();

            UnitGroupItemsSource = new ObservableCollection<UnitGroupModel>();
            foreach (UnitGroup unitGroup in Units.UnitGroups)
            {
                UnitGroupItemsSource.Add(new UnitGroupModel
                {
                    UnitGroup = unitGroup,
                    Name = Units.T(unitGroup),
                    GroupImageSource = ImageSource.FromResource($"Conversion.Resources.{unitGroup}.png"),
                });
            }

            UnitsList.ItemsSource = UnitGroupItemsSource;
        }

        async void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            UnitGroupModel unitGroupModel = (UnitGroupModel)e.SelectedItem;
            if (unitGroupModel != null)
            {
                UnitGroup unitGroup = unitGroupModel.UnitGroup;
                UnitPage page = new UnitPage(Units.ConverterMap[unitGroup]);
                await Navigation.PushAsync(page);

                // deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }


    }
}
