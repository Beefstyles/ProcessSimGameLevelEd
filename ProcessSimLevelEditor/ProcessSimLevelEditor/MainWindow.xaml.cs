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
        private int methaneFrac, ethaneFrac, propaneFrac;
        private int totalFlow = 0;
        public Dictionary<string, int> ComponentDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> ConditionsDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> GridDictionary = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
            InitiliaseCompDictionary();
            DataGrid();
        }

        private void InitiliaseCompDictionary()
        {
            ComponentDictionary.Add("Methane", 0);
            ComponentDictionary.Add("Ethane", 0);
            ComponentDictionary.Add("Propane", 0);
            ComponentDictionary.Add("NButane", 0);
            ComponentDictionary.Add("IButane", 0);
            ComponentDictionary.Add("NPentane", 0);
            ComponentDictionary.Add("IPentane", 0);
            ComponentDictionary.Add("Hexane", 0);
            ComponentDictionary.Add("Benzene", 0);
            ComponentDictionary.Add("Heptane", 0);
            ComponentDictionary.Add("Octane", 0);
            ComponentDictionary.Add("Nonane", 0);
            ComponentDictionary.Add("Decane", 0);
            ComponentDictionary.Add("Water", 0);
            ComponentDictionary.Add("Nitrogen", 0);
            ComponentDictionary.Add("CO2", 0);
            ComponentDictionary.Add("H2S", 0);
        }

        private void DataGrid()
        {
            List <DataGridClass> dg = new List<DataGridClass>();
            for (int i = 1; i <= 10; i++)
            {
                dg.Add(new DataGridClass() { Pos = i });
            }
            gameGrid.ItemsSource = dg;

        }

        public class DataGridClass
        {
            public int Pos { get; set; }
        }

        private void CalculateValues(object sender, RoutedEventArgs e)
        {
             var tb = sender as TextBox;
            CalculateTotalFlow();
            //MessageBox.Show("Total flow is " + totalFlow);
            IterateThroughGrid();
            DisplayTotalFlow();   
        }

        private void IterateThroughGrid()
        {
            /*foreach(System.Data.DataRowView dr in gameGrid.ItemsSource)
            {
                MessageBox.Show(dr[0].ToString());
            }
            */
        }

        private void CalculateTotalFlow()
        {
            totalFlow = ComponentDictionary.Sum(x => x.Value);
        }

        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled)
            {
                MessageBox.Show("Enter only numerical values here");
            }
        }

        private void ComponentTextBoxChange(object sender, TextChangedEventArgs e)
        {
            int componentFrac;
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out componentFrac);
                try
                {
                    ComponentDictionary[textBox.Name] = componentFrac;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error in adding to component dictionary " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in parsing " + ex.Message);
            }
        }

        

        private void DisplayTotalFlow()
        {
            TotalFlowCalc.Text = totalFlow.ToString();
        }
    }
}
