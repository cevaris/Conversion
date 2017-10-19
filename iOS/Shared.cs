using System;
using Conversion.iOS;
using Conversion.Source;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Shared))]
namespace Conversion.iOS
{
    public class Shared: IShared
    {
        private static ILogger logger = new ConsoleLogger(nameof(Shared));

        public void CopyToClipbard(string text)
        {
            UIPasteboard.General.String = text;
            logger.Info($"copied {UIPasteboard.General.String}");
        }
    }
}
