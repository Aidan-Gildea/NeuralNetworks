using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    public static class ErrorFunctions
    {
        // string hill climber error function
        public static float MAE(string mutatedString, string targetString)
        {
            float accumulatedSum = 0;
            for (int i = 0; i < targetString.Length; i++)
            {
                accumulatedSum += Math.Abs(targetString[i] - mutatedString[i]);
            }

            return accumulatedSum / targetString.Length;

        }
        public static double MSE(double actual, double desired)
        {
            return Math.Pow(actual - desired, 2);
        }

        public static double MSEDerivative(double actual, double desired)
        {
            return 2 * (actual - desired);
        }



    }
}
