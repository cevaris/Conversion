using System;
using Android.Content;
using Conversion.Droid;
using Conversion.Source;
using Xamarin.Forms;

[assembly: Dependency(typeof(Shared))]
namespace Conversion.Droid
{
    public class Shared : IShared
    {
        private static ILogger logger = new ConsoleLogger(nameof(Shared));

        public void CopyToClipbard(string text)
        {
            ClipboardManager clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("copy", text);
            clipboardManager.PrimaryClip = clip;
            logger.Info($"copied {clipboardManager.Text}");
        }
    }
}
