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
using System.ComponentModel;

namespace ProcessSimLevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int totalFlow = 0;

        ConditionsDict conditionsDictionary = new ConditionsDict();
        LevelAttribDicts levelAttribDictionary = new LevelAttribDicts();
        JSONWriter jswriter = new JSONWriter();
        PhaseCalc phaseCalculation = new PhaseCalc();
        private List<List<int>> lsts = new List<List<int>>();
        private bool settingJsonValuesFailed;
        private int[,] m_intArray = new int[10, 10];
        private int xGridValue = 10;
        private int yGridValue = 10;
        private int currentTemperature, currentPressure;
        GridLegend gridLegend;
       

        public Dictionary<string, int> GridDictionary = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
            InitialiseAllDictionaries();
            ResizeGrid();
        }

        private void InitialiseAllDictionaries()
        {
            levelAttribDictionary.InitiliaseCompDictionary();
            conditionsDictionary.InitCondDict();
            levelAttribDictionary.InitLevelAttribStringsDictionary();
            levelAttribDictionary.InitLevelAttribDecimalDictionary();
        }
        private void InitGridArray(int maxX, int maxY)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
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

        private void ResizeGridHandler(object sender, RoutedEventArgs e)
        {
            ResizeGrid();
        }

        private void ResizeGrid()
        {
            m_intArray = new int[xGridValue, yGridValue];
            InitGridArray(xGridValue, yGridValue);
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
            totalFlow = levelAttribDictionary.ComponentDictionary.Sum(x => x.Value);
        }

    
        //Added to textbox input that do not allow for input of anything other than numerical values
        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex("[^0-9.0]+");
            e.Handled = rgx.IsMatch(e.Text);
        }

        private void GridValuesChanged(object sender, TextChangedEventArgs e)
        {
            int gridVal;
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out gridVal);
                try
                {
                    switch (textBox.Name)
                    {
                        
                    }
                    conditionsDictionary.ConditionsDictionary[textBox.Name] = gridVal;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in parsing grid val " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in parsing " + ex.Message);
            }
        }

        private void ConditionTextBoxChange(object sender, TextChangedEventArgs e)
        {
            int condition;
            try
            {
                var textBox = sender as TextBox;
                string origin = textBox.Name.Substring(0, 5);

                switch (origin)
                {
                    case ("Comp_"):
                        try
                        {
                            Int32.TryParse(textBox.Text, out condition);
                            string outputComponent = textBox.Name.Remove(0, 5);
                            levelAttribDictionary.ComponentDictionary[outputComponent] = condition;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in adding to component dictionary " + ex.Message);
                        }
                        break;
                    case ("Cond_"):
                        try
                        {
                            Int32.TryParse(textBox.Text, out condition);
                            string outputComponent = textBox.Name.Remove(0, 5);
                            conditionsDictionary.ConditionsDictionary[outputComponent] = condition;
                            switch (outputComponent)
                            {
                                case ("Pressure"):
                                    currentPressure = condition;
                                    break;
                                case ("Temperature"):
                                    currentTemperature = condition;
                                    break;
                                default:
                                    MessageBox.Show("Error in altering currentTemperature or currentPressure");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in adding to component dictionary " + ex.Message);
                        }
                        break;

                    case ("AtbS_"):
                        try
                        {
                            string conditionString;

                            conditionString = textBox.Text;
                            string outputComponent = textBox.Name.Remove(0, 5);
                            levelAttribDictionary.LevelAttribStringsDictionary[outputComponent] = conditionString;

                            switch (outputComponent)
                            {
                                case ("XGrid"):
                                    int xGrid;
                                    Int32.TryParse(conditionString, out xGrid);
                                    xGridValue = xGrid;
                                    break;
                                case ("YGrid"):
                                    int yGrid;
                                    Int32.TryParse(conditionString, out yGrid);
                                    yGridValue = yGrid;
                                    break;
                                default:
                                    MessageBox.Show("Error in altering xGrid and YGrid");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in adding to component dictionary " + ex.Message);
                        }
                        break;

                case ("AtbD_"):
                        try
                        {
                            Int32.TryParse(textBox.Text, out condition);
                            string outputComponent = textBox.Name.Remove(0, 5);
                            levelAttribDictionary.LevelAttribDecimalDictionary[outputComponent] = condition;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in adding to component dictionary " + ex.Message);
                        }
                        break;
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in parsing " + ex.Message);
            }
        }

      
        private void LevelAttribStringTextBoxChange(object sender, TextChangedEventArgs e)
        {
            string attrib;
            try
            {
                var textBox = sender as TextBox;
                attrib = textBox.Text;
                try
                {
                    levelAttribDictionary.LevelAttribStringsDictionary[textBox.Name] = attrib;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in adding to level attribs string dictionary " + ex.Message);
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
                settingJsonValuesFailed = false;
            }

            catch(Exception ex)
            {
                MessageBox.Show("Setting Json Details failed horribly: " + ex.Message);
                settingJsonValuesFailed = true;
            }

            if (!settingJsonValuesFailed)
            {
                jswriter.OutputJson(levelOutput);
            }
        }

        private void SetJsonDetails(LevelOutputJSON levelOutput)
        {
            try
            {
                levelOutput.Title = levelAttribDictionary.LevelAttribStringsDictionary["Title"];
                levelOutput.Objective1Text = levelAttribDictionary.LevelAttribStringsDictionary["Objective1Text"];
                levelOutput.Objective1Text = levelAttribDictionary.LevelAttribStringsDictionary["Objective2Text"];
                levelOutput.Objective1Text = levelAttribDictionary.LevelAttribStringsDictionary["Objective3Text"];
            }

            catch (Exception e)
            {
                MessageBox.Show("Error in setting the json details, look into the SetJsonDetails func: " + e.Message);
            }
            
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

        private void DisplayGridLegend(object sender, RoutedEventArgs e)
        {
            SetDisplayGridLegend();
            gridLegend.Show();
        }

        public void SetDisplayGridLegend()
        {
            gridLegend = new GridLegend();
        }

        private void CalculatePhaseForComponents(object sender, RoutedEventArgs e)
        {
            MethaneVapFrac.Text = phaseCalculation.ReturnPhase("Methane", currentTemperature, currentPressure).ToString();
            EthaneVapFrac.Text = phaseCalculation.ReturnPhase("Ethane", currentTemperature, currentPressure).ToString();
            PropaneVapFrac.Text = phaseCalculation.ReturnPhase("Propane", currentTemperature, currentPressure).ToString();
        }
    }
}
