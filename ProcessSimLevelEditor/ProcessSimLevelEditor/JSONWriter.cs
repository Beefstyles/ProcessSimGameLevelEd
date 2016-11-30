using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessSimLevelEditor
{
    class JSONWriter
    {
        public void OutputJson(object sender, RoutedEventArgs e)
        {
            LevelOutputJSON levelOutput = new LevelOutputJSON();

            try
            {
                SetJsonDetails(levelOutput);
            }
            catch (Exception error)
            {
                MessageBox.Show("Setting JsonValues did not work correctly, error code is " + error.Message);
            }

            try
            {
                WriteJsonFile(levelOutput);
            }
            catch (Exception error)
            {
                MessageBox.Show("Writing JsonValue output did not work correctly, error code is " + error.Message);
            }
        }

        private void SetJsonDetails(string title, string obj1Text, string obj2Text, string obj3Text)
        {
            try
            {
                levelOutput.Title = title;
                levelOutput.Objective1Text = obj1Text;
                levelOutput.Objective1Text = obj2Text;
                levelOutput.Objective1Text = obj3Text;
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
    }
}
