using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conversion
{
    public partial class ConversionView : CarouselPage
    {
        ILogger logger = new ConsoleLogger(nameof(ConversionView));

        public ConversionView()
        {
            InitializeComponent();

            foreach(KeyValuePair<UnitGroup, List<UnitType>> entry in Units.UnitMap)
            {
                Children.Add(new UnitPage(entry.Key, entry.Value));
            }

            //CurrentPage = Children[3];
            CurrentPageChanged += (sender, e) =>
            {
                logger.Info($"changed page to {((UnitPage)CurrentPage).SelectedUnitGroup}");
            };

            ScrollToUnitGroupMessage.Subscribe(this, (sender, args) =>
            {
                logger.Info($"requested to change page to {args.SelectedItem}");
                CurrentPage = Children[Units.UnitGroups.FindIndex(x => x == args.SelectedItem)];
            });
        }
    }
}
