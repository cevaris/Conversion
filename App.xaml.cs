using Xamarin.Forms;

namespace Conversion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ConversionPage();
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
