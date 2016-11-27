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
        public Dictionary<string, float> LevelAttribFloatDictionary = new Dictionary<string, float>();

        public void InitLevelAttribStringsDictionary()
        {
            LevelAttribStringsDictionary.Add("Title", "");
            LevelAttribStringsDictionary.Add("Objective1Text", "");
            LevelAttribStringsDictionary.Add("Objective2Text", "");
            LevelAttribStringsDictionary.Add("Objective3Text", "");
            LevelAttribStringsDictionary.Add("XGrid", "");
            LevelAttribStringsDictionary.Add("YGrid", "");
        }

        public void InitLevelAttribFloatsDictionary()
        {
            LevelAttribFloatDictionary.Add("Objective1Value", 0F);
            LevelAttribFloatDictionary.Add("Objective2Value", 0F);
            LevelAttribFloatDictionary.Add("Objective3Value", 0F);
            LevelAttribFloatDictionary.Add("CostPlatMax", 0F);
            LevelAttribFloatDictionary.Add("CostGoldMax", 0F);
            LevelAttribFloatDictionary.Add("CostSilverMax", 0F);
            LevelAttribFloatDictionary.Add("CostBronzeMax", 0F);
            LevelAttribFloatDictionary.Add("CostPassMax", 0F);
        }
    }
}
