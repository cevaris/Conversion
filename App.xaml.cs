using System.Globalization;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace Conversion
{
    public partial class App : Application
    {
        public static bool IsDebug
        {
            get
            {
                bool isDebug;
#if DEBUG
                isDebug = true;
#else
                isDebug = false;
#endif
                return isDebug;
            }
        }

        public static CultureInfo CurrentCultureInfo
        {
            get
            {
                //return new CultureInfo("ja-JP");
                //return new CultureInfo("es");
                return CultureInfo.DefaultThreadCurrentUICulture;
            }
        }

        public App()
        {
            InitializeComponent();

            CultureInfo cultureInfo = CurrentCultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            NavigationPage page = new NavigationPage(new Views.MainPage());
            if (Device.RuntimePlatform == Device.iOS)
            {
                page.BarBackgroundColor = Color.FromHex("#f5f5f5");
            }

            MainPage = page;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
