using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace RequestsApp.Services
{
    public class DateBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                if (status)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#75eb46"));
                }
                else
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#eb4646"));
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
