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
                    returnPhase = (setPressure <= (0.5491 * Math.Pow(setTemp, 2) - 109.38 * setTemp + 5337.2)) ? 1 : 0;
                    break;
                case "Ethane":
                    returnPhase = (Math.Log10(setPressure) <= -4E-05 * Math.Pow(setTemp, 2) + .0198 * setTemp + 1.3108) ? 1 : 0;
                    break;
                case "Propane":
                    returnPhase = setPressure <= (9835.1 * Math.Log(setTemp) - 48464) ? 1 : 0;
                    break;
            }
            
            return returnPhase;
        }
    }
}
