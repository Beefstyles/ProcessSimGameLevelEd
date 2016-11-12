using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimLevelEditor
{
    public class ConditionsDict
    {
        public Dictionary<string, int> ConditionsDictionary = new Dictionary<string, int>();

        public void InitCondDict()
        {
            ConditionsDictionary.Add("Temperature", 0);
            ConditionsDictionary.Add("Pressure", 0);
            ConditionsDictionary.Add("AtmosphericTemp", 0);
            ConditionsDictionary.Add("AtmosphericPress",0)
        }
    }

    
}
