using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            foreach (UnitGroup unitGroup in Units.UnitMap.Keys)
            {
                UnitGroupItemsSource.Add(new UnitGroupModel
                {
                    UnitGroup = unitGroup,
                    Name = unitGroup.ToString()
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
                UnitPage page = new UnitPage(unitGroup, Units.UnitMap[unitGroup]);
                await Navigation.PushAsync(page);

                // deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }


    }
}
