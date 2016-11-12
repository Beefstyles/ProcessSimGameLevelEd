using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimLevelEditor
{
    class LevelOutputJSON
    {
        public string Title;
        public int GridXSize;
        public int GridYSize;
        public int Capex;
        public int LevelInletTemp;
        public int LevelInletPress;
        public int AtmosphericTemp;
        public int AtmosphericPress;
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
    }
}
