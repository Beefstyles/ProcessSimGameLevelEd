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
                methaneFrac = Convert.ToInt32(MethaneFrac.Text);
                ethaneFrac = Convert.ToInt32(EthaneFrac.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            MessageBox.Show("Tried to parse values");
        }
    }
}
