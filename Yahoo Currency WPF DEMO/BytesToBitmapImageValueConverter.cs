using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Yahoo_Currency_WPF_DEMO
{
    public class BytesToBitmapImageValueConverter : System.Windows.DependencyObject, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage bmp = new BitmapImage();
            try
            {
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = (System.IO.Stream)new System.IO.MemoryStream((byte[])value);
                bmp.EndInit();
            }
            catch
            {
                return null;
            }
            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapSource source = (BitmapSource)value;

            PngBitmapEncoder enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(source));

            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            enc.Save(mem);
            mem.Position = 0;
            return mem.ToArray();
        }
    }
}
