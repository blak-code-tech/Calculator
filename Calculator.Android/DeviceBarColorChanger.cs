using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Calculator.Droid;
using Calculator.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceBarColorChanger))]

namespace Calculator.Droid
{
    public class DeviceBarColorChanger : IDeviceBarColor
    {
        [Obsolete]
        public void SetDarkTheme(System.Drawing.Color color)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
            {
                return;
            }

            var window = GetWindow();
            var androidColor = color.ToPlatformColor();

            window.DecorView.SystemUiVisibility = 0;
            //window.SetNavigationBarColor(androidColor);
            window.SetStatusBarColor(androidColor);
        }

        [System.Obsolete]

        public void SetDeviceBarColor(System.Drawing.Color color, string type = "")
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
            {
                return;
            }

            var window = GetWindow();
            var androidColor = color.ToPlatformColor();

            if (type == "both")
            {
                window.SetNavigationBarColor(androidColor);
                window.SetStatusBarColor(androidColor);
            }
            else
            {
                window.SetStatusBarColor(androidColor);
            }
        }

        [Obsolete]
        public void SetLightTheme(System.Drawing.Color color)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
            {
                return;
            }

            var window = GetWindow();
            var androidColor = color.ToPlatformColor();
            window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            //window.SetNavigationBarColor(androidColor);
            window.SetStatusBarColor(androidColor);
        }

        [Obsolete]
        Window GetWindow()
        {
            var window = ((MainActivity)Forms.Context).Window;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

            return window;
        }
    }
}