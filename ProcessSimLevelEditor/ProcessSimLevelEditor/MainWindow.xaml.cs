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
using System.Data;

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
        private List<List<int>> lsts = new List<List<int>>();

        public Dictionary<string, int> GridDictionary = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
            componentDictionary.InitiliaseCompDictionary();
            
            //DataGrid();
            TestList(10,10);
        }

        private void TestList(int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                lsts.Add(new List<int>());

                for (int y = 0; y < ySize; y++)
                {
                    lsts[x].Add(y * 10 + y);
                }
            }

            lst.ItemsSource = lsts;
        }

        

        private void DataGrid()
        {
            List <DataGridClass> dg = new List<DataGridClass>();
            for (int i = 1; i <= 10; i++)
            {
                dg.Add(new DataGridClass() { Pos = i });
            }


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

        public static DataView GetBindable2DArray<T>(T[,] array)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                dt.Columns.Add(i.ToString(), typeof(Ref<T>));
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);

            for (int i = 0; i < array.GetLength(0); i++) 
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int a = i;
                    int b = j;
                    Ref<T> refT = new Ref<T>(() => array[a, b], z => { array[a, b] = z; });
                    dv[i][j] = refT;
                }
            }
            return dv;
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

        private void IterateThroughList(object sender, RoutedEventArgs e)
        {
            foreach(var listVal in lsts)
            {
                MessageBox.Show(listVal.ToString());
            }
        }

        private void levelGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }
    }
}
