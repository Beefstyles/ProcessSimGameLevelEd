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
        private int totalFlow = 0;
        ComponentDict componentDictionary = new ComponentDict();
        ConditionsDict conditionsDictionary = new ConditionsDict();

        public Dictionary<string, int> GridDictionary = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
            componentDictionary.InitiliaseCompDictionary();
            
            DataGrid();
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
            PhaseGraph pg = new PhaseGraph();
            pg.Show();   
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
            totalFlow = componentDictionary.ComponentDictionary.Sum(x => x.Value);
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
                    componentDictionary.ComponentDictionary[textBox.Name] = componentFrac;
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

        private void ConditionsTextBoxChange(object sender, TextChangedEventArgs e)
        {
            int conditon;
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out conditon);
                try
                {
                    conditionsDictionary.ConditionsDictionary[textBox.Name] = conditon;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in adding to conditions dictionary " + ex.Message);
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
