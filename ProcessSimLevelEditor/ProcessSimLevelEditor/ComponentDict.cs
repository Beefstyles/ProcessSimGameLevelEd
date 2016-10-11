using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimLevelEditor
{
    class ComponentDict
    {
        public Dictionary<string, int> ComponentDictionary = new Dictionary<string, int>();

        public void InitiliaseCompDictionary()
        {
            ComponentDictionary.Add("Methane", 0);
            ComponentDictionary.Add("Ethane", 0);
            ComponentDictionary.Add("Propane", 0);
            ComponentDictionary.Add("NButane", 0);
            ComponentDictionary.Add("IButane", 0);
            ComponentDictionary.Add("NPentane", 0);
            ComponentDictionary.Add("IPentane", 0);
            ComponentDictionary.Add("Hexane", 0);
            ComponentDictionary.Add("Benzene", 0);
            ComponentDictionary.Add("Heptane", 0);
            ComponentDictionary.Add("Octane", 0);
            ComponentDictionary.Add("Nonane", 0);
            ComponentDictionary.Add("Decane", 0);
            ComponentDictionary.Add("Water", 0);
            ComponentDictionary.Add("Nitrogen", 0);
            ComponentDictionary.Add("CO2", 0);
            ComponentDictionary.Add("H2S", 0);
        }
    }
}
