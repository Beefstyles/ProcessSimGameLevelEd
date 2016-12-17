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
        LevelAttribDicts levelAttribDictionary = new LevelAttribDicts();
        JSONWriter jswriter = new JSONWriter();
        PhaseCalc phaseCalculation = new PhaseCalc();
        private List<List<int>> lsts = new List<List<int>>();
        private bool settingJsonValuesFailed;
        private char[,] m_charArray = new char[10, 10];
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
            levelAttribDictionary.InitConditionsDictionary();
            levelAttribDictionary.InitLevelAttribStringsDictionary();
            levelAttribDictionary.InitLevelAttribDecimalDictionary();
        }

        private void InitGridLevelArray(int maxX, int maxY)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    m_charArray[i, j] = '.';
                }
            }
            levelGrid.ItemsSource = GetBindable2DArray<char>(m_charArray);
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
            m_charArray = new char[xGridValue, yGridValue];
            InitGridLevelArray(xGridValue, yGridValue);
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
            totalFlow = levelAttribDictionary.ComponentsDictionary.Sum(x => x.Value);
        }

    
        //Added to textbox input that do not allow for input of anything other than numerical values
        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex("[^0-9.0]+");
            e.Handled = rgx.IsMatch(e.Text);
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
                            levelAttribDictionary.ComponentsDictionary[outputComponent] = condition;
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
                            levelAttribDictionary.ConditionsDictionary[outputComponent] = condition;
                            switch (outputComponent)
                            {
                                case ("Pressure"):
                                    currentPressure = condition;
                                    break;
                                case ("Temperature"):
                                    currentTemperature = condition;
                                    break;
                                case ("XGrid"):
                                    xGridValue = condition;
                                    break;
                                case ("YGrid"):
                                    yGridValue = condition;
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

                    default:
                        {
                            MessageBox.Show("This textbox has no recognisable prefix: " + textBox.Name);
                        }
                        break;
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
            MessageBox.Show(m_charArray[0, 0].ToString());
        }

        private void ClearGraphValues(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_charArray.GetLength(0); i++)
            {
                for (int j = 0; j < m_charArray.GetLength(1); j++)
                {
                    m_charArray[i, j] = '.';
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

        private void SaveAndOutputJson(object sender, RoutedEventArgs e)
        {
            LevelOutputJSON levelOutput = new LevelOutputJSON();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Level";
            saveFileDialog.DefaultExt = ".txt";
            //saveFileDialog.Filter = "Text files (*.json*)|All files (*.*)";
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = saveFileDialog.FileName;
                try
                {
                    SetJsonDetails(levelOutput);
                    settingJsonValuesFailed = false;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Setting Json Details failed horribly: " + ex.Message);
                    settingJsonValuesFailed = true;
                }

                if (!settingJsonValuesFailed)
                {
                    jswriter.OutputJson(levelOutput, fileName);
                }
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

                levelOutput.LevelInletTemp = levelAttribDictionary.ConditionsDictionary["Temperature"];
                levelOutput.LevelInletPress = levelAttribDictionary.ConditionsDictionary["Pressure"];
                levelOutput.AtmosphericTemp = levelAttribDictionary.ConditionsDictionary["AtmosphericTemp"];
                levelOutput.AtmosphericPress = levelAttribDictionary.ConditionsDictionary["AtmosphericPress"];
                levelOutput.GridXSize = levelAttribDictionary.ConditionsDictionary["XGrid"];
                levelOutput.GridYSize = levelAttribDictionary.ConditionsDictionary["YGrid"];

                levelOutput.Capex = levelAttribDictionary.LevelAttribDecimalDictionary["Capex"];
                levelOutput.Objective1Value = levelAttribDictionary.LevelAttribDecimalDictionary["Objective1Value"];
                levelOutput.Objective2Value = levelAttribDictionary.LevelAttribDecimalDictionary["Objective2Value"];
                levelOutput.CostPlatMax = levelAttribDictionary.LevelAttribDecimalDictionary["CostPlatMax"];
                levelOutput.CostGoldMax = levelAttribDictionary.LevelAttribDecimalDictionary["CostGoldMax"];
                levelOutput.CostSilverMax = levelAttribDictionary.LevelAttribDecimalDictionary["CostSilverMax"];
                levelOutput.CostBronzeMax = levelAttribDictionary.LevelAttribDecimalDictionary["CostBronzeMax"];
                levelOutput.CostPassMax = levelAttribDictionary.LevelAttribDecimalDictionary["CostPassMax"];

                levelOutput.Methane = levelAttribDictionary.ComponentsDictionary["Methane"];
                levelOutput.Ethane = levelAttribDictionary.ComponentsDictionary["Ethane"];
                levelOutput.Propane = levelAttribDictionary.ComponentsDictionary["Propane"];
                levelOutput.NButane = levelAttribDictionary.ComponentsDictionary["NButane"];
                levelOutput.IButane = levelAttribDictionary.ComponentsDictionary["IButane"];
                levelOutput.NPentane = levelAttribDictionary.ComponentsDictionary["NPentane"];
                levelOutput.NPentane = levelAttribDictionary.ComponentsDictionary["NPentane"];
                levelOutput.IPentane = levelAttribDictionary.ComponentsDictionary["IPentane"];
                levelOutput.Hexane = levelAttribDictionary.ComponentsDictionary["Hexane"];
                levelOutput.Benzene = levelAttribDictionary.ComponentsDictionary["Benzene"];
                levelOutput.Heptane = levelAttribDictionary.ComponentsDictionary["Heptane"];
                levelOutput.Heptane = levelAttribDictionary.ComponentsDictionary["Heptane"];
                levelOutput.Octane = levelAttribDictionary.ComponentsDictionary["Octane"];
                levelOutput.Nonane = levelAttribDictionary.ComponentsDictionary["Nonane"];
                levelOutput.Decane = levelAttribDictionary.ComponentsDictionary["Decane"];
                levelOutput.Water = levelAttribDictionary.ComponentsDictionary["Water"];
                levelOutput.Nitrogen = levelAttribDictionary.ComponentsDictionary["Nitrogen"];
                levelOutput.CO2 = levelAttribDictionary.ComponentsDictionary["CO2"];
                levelOutput.H2S = levelAttribDictionary.ComponentsDictionary["H2S"];

                levelOutput.GridArray = m_charArray;
            }

            catch (Exception e)
            {
                MessageBox.Show("Error in setting the json details, look into the SetJsonDetails func: " + e.Message);
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
            NButaneVapFrac.Text = phaseCalculation.ReturnPhase("nButane", currentTemperature, currentPressure).ToString();
            IButaneVapFrac.Text = phaseCalculation.ReturnPhase("iButane", currentTemperature, currentPressure).ToString();
            NPentaneVapFrac.Text = phaseCalculation.ReturnPhase("nPentane", currentTemperature, currentPressure).ToString();
            IPentaneVapFrac.Text = phaseCalculation.ReturnPhase("iPentane", currentTemperature, currentPressure).ToString();
            BenzeneVapFrac.Text = phaseCalculation.ReturnPhase("Benzene", currentTemperature, currentPressure).ToString();
            HexaneVapFrac.Text = phaseCalculation.ReturnPhase("Hexane", currentTemperature, currentPressure).ToString();
            HeptaneVapFrac.Text = phaseCalculation.ReturnPhase("Heptane", currentTemperature, currentPressure).ToString();
            OctaneVapFrac.Text = phaseCalculation.ReturnPhase("Octane", currentTemperature, currentPressure).ToString();
            NonaneVapFrac.Text = phaseCalculation.ReturnPhase("Nonane", currentTemperature, currentPressure).ToString();
            DecaneVapFrac.Text = phaseCalculation.ReturnPhase("Decane", currentTemperature, currentPressure).ToString();
            WaterVapFrac.Text = phaseCalculation.ReturnPhase("Water", currentTemperature, currentPressure).ToString();
            NitrogenVapFrac.Text = phaseCalculation.ReturnPhase("Nitrogen", currentTemperature, currentPressure).ToString();
            CO2VapFrac.Text = phaseCalculation.ReturnPhase("CO2", currentTemperature, currentPressure).ToString();
            H2SVapFrac.Text = phaseCalculation.ReturnPhase("H2S", currentTemperature, currentPressure).ToString();
        }

        private int MethaneTest(int testTemp, int testPressure)
        {
            return phaseCalculation.ReturnPhase("Methane", testTemp, testPressure);
        }
    }
}
