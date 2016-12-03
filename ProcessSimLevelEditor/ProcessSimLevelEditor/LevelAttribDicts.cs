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

        public void InitLevelAttribStringsDictionary()
        {
            LevelAttribStringsDictionary.Add("Title", "");
            LevelAttribStringsDictionary.Add("Objective1Text", "");
            LevelAttribStringsDictionary.Add("Objective2Text", "");
            LevelAttribStringsDictionary.Add("Objective3Text", "");
            LevelAttribStringsDictionary.Add("XGrid", "");
            LevelAttribStringsDictionary.Add("YGrid", "");
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
    }
}
