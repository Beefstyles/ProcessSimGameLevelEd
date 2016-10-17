using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimLevelEditor
{
    class PhaseCalc
    {
        public int ReturnPhase(Dictionary<string,float> CompDict, string componentName, int setTemp, int setPressure)
        {
            int returnPhase = 0;
            switch (componentName)
            {
                case "Methane":
                    if (setPressure <= (0.5491 * Math.Pow(setTemp, 2) - 109.38 * setTemp + 5337.2))
                    {
                        returnPhase = 1;
                    }
                    else
                    {
                        returnPhase = 0;
                    }
                    break;
            }
            
            return returnPhase;
        }
    }
}
