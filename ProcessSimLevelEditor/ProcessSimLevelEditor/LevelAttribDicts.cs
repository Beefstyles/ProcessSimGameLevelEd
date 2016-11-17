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
        public Dictionary<string, string> LevelAttribFloatDictionary = new Dictionary<string, string>();

        public void InitLevelAttribStringsDictionary()
        {
            LevelAttribStringsDictionary.Add("Title", "");
            LevelAttribStringsDictionary.Add("Objective1Text", "");
            LevelAttribStringsDictionary.Add("Objective2Text", "");
            LevelAttribStringsDictionary.Add("Objective3Text", "");
        }

        public void InitLevelAttribFloatsDictionary()
        {

        }
    }
}
