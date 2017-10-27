using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Google.MobileAds;
using UIKit;
using CoreGraphics;
using System;

[assembly: ExportRenderer(typeof(Conversion.Views.PclAdBannerView), typeof(Conversion.iOS.AdBannerView))]
namespace Conversion.iOS
{
    public class AdBannerView : ViewRenderer
    {
        BannerView adView;
        bool viewOnScreen = false;
        private readonly static ILogger logger = new ConsoleLogger(nameof(AdBannerView));

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || App.AdsRenderState == AdsState.RenderNothing)
                return;

            if (e.OldElement == null && App.AdsRenderState == AdsState.Render)
            {
                UIViewController viewCtrl = null;

                foreach (UIWindow v in UIApplication.SharedApplication.Windows)
                {
                    if (v.RootViewController != null)
                    {
                        viewCtrl = v.RootViewController;
                    }
                }

                logger.Info($"found root controller {viewCtrl.ToString()}");

                adView = new BannerView(
                    size: AdSizeCons.Banner,
                    origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height)
                )
                {
                    AdUnitID = Secrets.IOSBannerId,
                    RootViewController = viewCtrl
                };

                adView.ReceiveAdFailed += (sender, args) =>
                {
                    var error = ((BannerViewErrorEventArgs)args).Error;
                    logger.Error($"received failed ad: {error} {error.Code}");
                };

                adView.WillChangeAdSizeTo += (sender, args) =>
                {
                    logger.Info($"changing add size to: {((AdSizeDelegateSizeEventArgs)args).Size}");
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen)
                    {
                        logger.Info($"received ad: {args.GetType()}");
                        AddSubview(adView);
                    }
                    viewOnScreen = true;
                };

                Request request = Request.GetDefaultRequest();
                if (App.IsDebug)
                {
                    request.TestDevices = new[] { Request.SimulatorId, Secrets.IOSTestRequestId };
                }
                adView.LoadRequest(request);
                SetNativeControl(adView);
            }
        }
    }
}
