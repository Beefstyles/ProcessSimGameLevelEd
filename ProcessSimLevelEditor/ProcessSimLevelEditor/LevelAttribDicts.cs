using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ConditionsDictionary.Add("Temperature", 0);
            ConditionsDictionary.Add("Pressure", 0);
            ConditionsDictionary.Add("AtmosphericTemp", 0);
            ConditionsDictionary.Add("AtmosphericPress", 0);
            ConditionsDictionary.Add("XGrid", 0);
            ConditionsDictionary.Add("YGrid", 0);
        }


        public void InitLevelAttribStringsDictionary()
        {
            LevelAttribStringsDictionary.Add("Title", "");
            LevelAttribStringsDictionary.Add("Objective1Text", "");
            LevelAttribStringsDictionary.Add("Objective2Text", "");
            LevelAttribStringsDictionary.Add("Objective3Text", "");
            
        }

        public void InitLevelAttribDecimalDictionary()
        {
            LevelAttribDecimalDictionary.Add("Capex", 0);
            LevelAttribDecimalDictionary.Add("Objective1Value", 0);
            LevelAttribDecimalDictionary.Add("Objective1Value", 0);
            LevelAttribDecimalDictionary.Add("Objective2Value", 0);
            LevelAttribDecimalDictionary.Add("Objective3Value", 0);
            LevelAttribDecimalDictionary.Add("CostPlatMax", 0);
            LevelAttribDecimalDictionary.Add("CostGoldMax", 0);
            LevelAttribDecimalDictionary.Add("CostSilverMax", 0);
            LevelAttribDecimalDictionary.Add("CostBronzeMax", 0);
            LevelAttribDecimalDictionary.Add("CostPassMax", 0);
        }

        public void InitiliaseCompDictionary()
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
    }
}
