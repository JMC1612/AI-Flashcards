using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace BfK_S_ApiProjekt.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter?.ToString() == "invert";
            bool flag = value is bool b && b;

            if (invert)
                flag = !flag;

            return flag ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility vis && vis == Visibility.Visible;
    }
}
