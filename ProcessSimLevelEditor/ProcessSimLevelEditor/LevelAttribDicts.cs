using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessSimLevelEditor
{
    class LevelAttribDicts
    {
        public Dictionary<string, string> LevelAttribStringsDictionary = new Dictionary<string, string>();
        public Dictionary<string, decimal> LevelAttribDecimalDictionary = new Dictionary<string, decimal>();
        public Dictionary<string, int> ComponentsDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> ConditionsDictionary = new Dictionary<string, int>();

        public void InitConditionsDictionary()
        {
            try
            {
                ConditionsDictionary.Add("Temperature", 0);
                ConditionsDictionary.Add("Pressure", 0);
                ConditionsDictionary.Add("AtmosphericTemp", 0);
                ConditionsDictionary.Add("AtmosphericPress", 0);
                ConditionsDictionary.Add("XGrid", 0);
                ConditionsDictionary.Add("YGrid", 0);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in InitConditionsDictionary " + ex.Message);
            }
        }


        public void InitLevelAttribStringsDictionary()
        {
            try
            {
                LevelAttribStringsDictionary.Add("Title", "");
                LevelAttribStringsDictionary.Add("Objective1Text", "");
                LevelAttribStringsDictionary.Add("Objective2Text", "");
                LevelAttribStringsDictionary.Add("Objective3Text", "");
                LevelAttribStringsDictionary.Add("Objective1Comparision", "");
                LevelAttribStringsDictionary.Add("Objective2Comparision", "");
                LevelAttribStringsDictionary.Add("Objective3Comparision", "");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in InitLevelAttribStringsDictionary " + ex.Message);
            }

        }

        public void InitLevelAttribDecimalDictionary()
        {
            try
            {
                LevelAttribDecimalDictionary.Add("Capex", 0M);
                LevelAttribDecimalDictionary.Add("Objective1Value", 0M);
                LevelAttribDecimalDictionary.Add("Objective2Value", 0M);
                LevelAttribDecimalDictionary.Add("Objective3Value", 0M);
                LevelAttribDecimalDictionary.Add("CostPlatMax", 0M);
                LevelAttribDecimalDictionary.Add("CostGoldMax", 0M);
                LevelAttribDecimalDictionary.Add("CostSilverMax", 0M);
                LevelAttribDecimalDictionary.Add("CostBronzeMax", 0M);
                LevelAttribDecimalDictionary.Add("CostPassMax", 0M);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in InitLevelAttribDecimalDictionary " + ex.Message);
            }

            
        }

        public void InitiliaseCompDictionary()
        {
            try
            {
                ComponentsDictionary.Add("Methane", 0);
                ComponentsDictionary.Add("Ethane", 0);
                ComponentsDictionary.Add("Propane", 0);
                ComponentsDictionary.Add("NButane", 0);
                ComponentsDictionary.Add("IButane", 0);
                ComponentsDictionary.Add("NPentane", 0);
                ComponentsDictionary.Add("IPentane", 0);
                ComponentsDictionary.Add("Hexane", 0);
                ComponentsDictionary.Add("Benzene", 0);
                ComponentsDictionary.Add("Heptane", 0);
                ComponentsDictionary.Add("Octane", 0);
                ComponentsDictionary.Add("Nonane", 0);
                ComponentsDictionary.Add("Decane", 0);
                ComponentsDictionary.Add("Water", 0);
                ComponentsDictionary.Add("Nitrogen", 0);
                ComponentsDictionary.Add("CO2", 0);
                ComponentsDictionary.Add("H2S", 0);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in InitiliaseCompDictionary " + ex.Message);
            }
        }
    }
}
