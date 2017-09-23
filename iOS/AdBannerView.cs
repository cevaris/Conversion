using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Google.MobileAds;
using UIKit;
using CoreGraphics;
using Conversion;
using Conversion.iOS;

[assembly: ExportRenderer(typeof(AdBannerView), typeof(PclAdBannerView))]
namespace Conversion.iOS
{
    public class AdBannerView : ViewRenderer
    {
        BannerView adView;
        bool viewOnScreen = false;
        private static ILogger logger = new ConsoleLogger(nameof(AdBannerView));

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            logger.Info("AdBannerView is being called");

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                UIViewController viewCtrl = null;

                foreach (UIWindow v in UIApplication.SharedApplication.Windows)
                {
                    if (v.RootViewController != null)
                    {
                        viewCtrl = v.RootViewController;
                    }
                }

                string bannerId = Secrets.IOSBannerId;
                if (App.IsDebug)
                {
                    bannerId = Secrets.TestBannerId;
                }

                adView = new BannerView(
                    size: AdSizeCons.Banner,
                    origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height)
                )
                {
                    AdUnitID = bannerId,
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