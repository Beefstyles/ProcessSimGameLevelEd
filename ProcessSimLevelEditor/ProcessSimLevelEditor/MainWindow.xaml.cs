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
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;

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
        private int[,] m_intArray = new int[5, 5];


        public Dictionary<string, int> GridDictionary = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
            componentDictionary.InitiliaseCompDictionary();

            //DataGrid();
            //TestList(10,10);
            InitGridArray();
        }

        private void InitGridArray()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    m_intArray[i, j] = (i * 10 + j);
                }
            }
            levelGrid.ItemsSource = GetBindable2DArray<int>(m_intArray);
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
            //IterateThroughGrid();
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

        private void CalculateTotalFlow()
        {
            totalFlow = componentDictionary.ComponentDictionary.Sum(x => x.Value);
        }

    
        //Added to textbox input that do not allow for input of anything other than numerical values
        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex("[^0-9.0]+");
            e.Handled = rgx.IsMatch(e.Text);
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
            int condition;
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out condition);
                try
                {
                    conditionsDictionary.ConditionsDictionary[textBox.Name] = condition;
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
            MessageBox.Show(m_intArray[0, 0].ToString());
        }

        private void ClearGraphValues(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_intArray.GetLength(0); i++)
            {
                for (int j = 0; j < m_intArray.GetLength(1); j++)
                {
                    m_intArray[i, j] = 0;
                }
            }
            levelGrid.Items.Refresh();
        }

        private void levelGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }

        

        private void OutputJson(object sender, RoutedEventArgs e)
        {
            LevelOutputJSON levelOutput = new LevelOutputJSON();

            try
            {
                SetJsonDetails(levelOutput);
            }
            catch(Exception error)
            {
                MessageBox.Show("Setting JsonValues did not work correctly, error code is " + error.Message);
            }

            try
            {
                WriteJsonFile(levelOutput);
            }
            catch (Exception error)
            {
                MessageBox.Show("Wring JsonValue output did not work correctly, error code is " + error.Message);
            }
        }
        private void SetJsonDetails(LevelOutputJSON levelOutput)
        {
            /*
            public string Title;
            public int GridXSize;
            public int GridYSize;
            public int Capex;
            public int LevelInletTemp;
            public int LevelInletPress;
            public string Objective1Text;
            public string Objective2Text;
            public string Objective3Text;
            public float Objective1Value;
            public float Objective2Value;
            public float Objective3Value;
            public int CostPlatMax;
            public int CostGoldMax;
            public int CostSilverMax;
            public int CostBronzeMax;
            public int CostPassMax;
            public char[] GridArray;
            public int Methane, Ethane, Propane, NButane, IButane, NPentane,
            IPentane, Hexane, Benzene, Heptane, Octane, Nonane, Decane,
            Water, Nitrogen, CO2, H2S;
            */
            levelOutput.Title = Title;
            
            
        }

        private void WriteJsonFile(LevelOutputJSON levelOutput)
        {
            JsonSerializer serialiser = new JsonSerializer();
            serialiser.NullValueHandling = NullValueHandling.Ignore;
            string myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter sw = new StreamWriter(myDocsPath + @"\jsonTest.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serialiser.Formatting = Formatting.Indented;
                serialiser.Serialize(writer, levelOutput);
            }
        }

        private void OpenFileButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.json*)|All files (*.*)";
            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("Open!");
            }
        }
    }
}
