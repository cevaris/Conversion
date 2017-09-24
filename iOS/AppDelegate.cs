using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Google.MobileAds;
using UIKit;

namespace Conversion.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MobileAds.Configure(Secrets.IOSAppId);
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
