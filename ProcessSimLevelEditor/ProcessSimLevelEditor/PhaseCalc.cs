using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimLevelEditor
{
    class PhaseCalc
    {
        public int ReturnPhase(string componentName, int setTemp, int setPressure)
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
                case "nButane":
                    returnPhase = setPressure <= (10196 * Math.Log(setTemp) - 52175) ? 1 : 0;
                    break;
                case "iButane":
                    returnPhase = setPressure <= (10196 * Math.Log(setTemp) - 52175) ? 1 : 0;
                    break;
                case "nPentane":
                    returnPhase = setPressure <= (0.0003 * Math.Pow(setTemp, 3) - 0.2002 * Math.Pow(setTemp, 2) + 48.614 * setTemp - 3916.6) ? 1 : 0;
                    break;
                case "iPentane":
                    returnPhase = setPressure <= (0.0003 * Math.Pow(setTemp, 3) - 0.2002 * Math.Pow(setTemp, 2) + 48.614 * setTemp - 3916.6) ? 1 : 0;
                    break;
                case "Benzene":
                    returnPhase = setPressure <= (0.0003 * Math.Pow(setTemp, 3) - 0.2265 * Math.Pow(setTemp, 2) + 67.137 * setTemp - 6612.4) ? 1 : 0;
                    break;
                case "Hexane":
                    returnPhase = setPressure <= (0.0002 * Math.Pow(setTemp, 3) - 0.1758 * Math.Pow(setTemp, 2) + 47.795 * setTemp - 4310.9) ? 1 : 0;
                    break;
                case "Heptane":
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "Octane":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "Nonane":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "Decane":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "Water":
                    returnPhase = setPressure <= (-0.0002 * Math.Pow(setTemp, 2) + .0948 * setTemp - 7.5362) ? 1 : 0;
                    break;
                case "Nitrogen":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "CO2":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                case "H2S":
                    //To be implemented
                    returnPhase = setPressure <= (-0.0161 * Math.Pow(setTemp, 2) + 16.404 * setTemp - 3203.8) ? 1 : 0;
                    break;
                default:
                    returnPhase = 999; //Phase not implemented
                    break;
            }
            
            return returnPhase;
        }


    }
}
