﻿using System;
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
            global::Xamarin.Forms.Forms.Init();
            MobileAds.Configure(Secrets.IOSAppId);


            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
