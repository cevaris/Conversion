using System;
using Xamarin.Forms;

namespace Conversion
{
    public interface IMessage
    {
    }

    public class ScrollToUnitGroupMessage : IMessage
    {
        public UnitGroup SelectedItem { get; set; }

        public static string Name
        {
            get
            {
                return nameof(ScrollToUnitGroupMessage);
            }
        }

        public void Send(UnitPage sender)
        {
            MessagingCenter.Send<UnitPage, ScrollToUnitGroupMessage>(sender, Name, this);
        }

        public static void Subscribe(ConversionView subscriber, Action<UnitPage, ScrollToUnitGroupMessage> func)
        {
            MessagingCenter.Subscribe<UnitPage, ScrollToUnitGroupMessage>(subscriber, Name, func);
        }
    }
}
