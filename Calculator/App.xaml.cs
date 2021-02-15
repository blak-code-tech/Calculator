using Calculator.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator
{
    public partial class App : Application
    {
        public IDeviceBarColor barColors => DependencyService.Get<IDeviceBarColor>();

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            
        }

        protected override void OnStart()
        {
            var check = Preferences.ContainsKey("AppTheme");
            if (check)
            {
                var pref = Preferences.Get("AppTheme", "Light");

                if (pref == "Light")
                {
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    barColors.SetLightTheme(System.Drawing.Color.FromArgb(255, 255, 255));
                }
                else
                {
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    barColors.SetDarkTheme(System.Drawing.Color.FromArgb(34, 37, 45));
                }
            }
            else
            {
                if (Current.UserAppTheme == OSAppTheme.Dark)
                {
                    barColors.SetDarkTheme(System.Drawing.Color.FromArgb(34, 37, 45));
                }
                else
                {
                    barColors.SetLightTheme(System.Drawing.Color.FromArgb(255, 255, 255));
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            if (Current.UserAppTheme == OSAppTheme.Dark)
            {
                barColors.SetDarkTheme(System.Drawing.Color.FromArgb(34, 37, 45));
            }
            else
            {
                barColors.SetLightTheme(System.Drawing.Color.FromArgb(255, 255, 255));
            }
        }
    }
}
