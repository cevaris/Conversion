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

        public App()
        {
            InitializeComponent();

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
