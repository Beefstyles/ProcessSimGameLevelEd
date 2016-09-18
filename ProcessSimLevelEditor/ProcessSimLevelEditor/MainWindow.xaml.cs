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
using System.Text.RegularExpressions;

namespace ProcessSimLevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int methaneFrac, ethaneFrac;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculatePhase(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(MethaneFrac.Text, out methaneFrac);
                Int32.TryParse(EthaneFrac.Text, out ethaneFrac);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            MessageBox.Show("Tried to parse values");
        }

        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
