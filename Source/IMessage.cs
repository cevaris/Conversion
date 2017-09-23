using System;
using Xamarin.Forms;

namespace Conversion
{
    public interface IMessage<A>
    {
        void Send(A sender);
    }

    public class ScrollToUnitGroupMessage : IMessage<UnitPage>
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
    }
}
