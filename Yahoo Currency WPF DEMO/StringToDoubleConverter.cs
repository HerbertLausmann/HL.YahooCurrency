using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahoo_Currency_WPF_DEMO
{
    class StringToDoubleConverter : System.Windows.DependencyObject, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((double)value).ToString();
            }
            catch { return 0.0.ToString(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double.TryParse((string)value, out double output);
            return output;
        }
    }
}
