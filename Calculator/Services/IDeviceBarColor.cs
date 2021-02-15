using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Services
{
    public interface IDeviceBarColor
    {
        void SetDeviceBarColor(System.Drawing.Color color, string type = "");

        void SetLightTheme(System.Drawing.Color color);
        void SetDarkTheme(System.Drawing.Color color);
    }
}
