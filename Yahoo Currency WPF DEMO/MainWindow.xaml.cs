using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yahoo_Currency_WPF_DEMO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The code bellow enables the textbox for number input only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".") { e.Handled = true; return; }
            e.Handled = !double.TryParse(((TextBox)sender).Text + e.Text + "0", System.Globalization.NumberStyles.AllowDecimalPoint,
                (IFormatProvider)System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat.GetFormat(typeof(double)), out double number);
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).SelectedIndex = 0;
        }
    }
}
