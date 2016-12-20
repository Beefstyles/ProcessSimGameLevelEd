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
        public decimal Capex;
        public int LevelInletTemp;
        public int LevelInletPress;
        public int AtmosphericTemp;
        public int AtmosphericPress;
        public string Objective1Text;
        public string Objective2Text;
        public string Objective3Text;
        public decimal Objective1Value;
        public decimal Objective2Value;
        public decimal Objective3Value;
        public decimal CostPlatMax;
        public decimal CostGoldMax;
        public decimal CostSilverMax;
        public decimal CostBronzeMax;
        public decimal CostPassMax;
        public List<string> LevelGridArray;
        public int Methane, Ethane, Propane, NButane, IButane, NPentane,
        IPentane, Hexane, Benzene, Heptane, Octane, Nonane, Decane,
        Water, Nitrogen, CO2, H2S;

       

      
    }


}
