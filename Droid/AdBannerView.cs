using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Conversion.Droid;
using Android.Gms.Ads;
using Android.Content.Res;
using Conversion;

[assembly: ExportRenderer(typeof(PclAdBannerView), typeof(AdBannerView))]
namespace Conversion.Droid
{
    public class AdBannerView : ViewRenderer<PclAdBannerView, AdView>
    {
        AdView adView;

        private ILogger logger = new ConsoleLogger(nameof(AdBannerView));

        protected override void OnElementChanged(ElementChangedEventArgs<PclAdBannerView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {

                ScreenLayout screenlayout = Resources.Configuration.ScreenLayout;
                logger.Info($"screenLayout {screenlayout}");

                adView = new AdView(Forms.Context);
                adView.AdUnitId = Secrets.DroidBannerId;
                adView.AdSize = AdSize.SmartBanner;

                adView.LoadAd(new AdRequest.Builder().AddTestDevice(Secrets.DroidTestRequestId).Build());
                SetNativeControl(adView);
            }
        }
    }
}