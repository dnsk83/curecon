using System;
using System.Threading.Tasks;
using curecon.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace curecon
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new CurrenciesListPage();
        }

        protected override void OnStart()
        {
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
