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
        public static double MSE(double desired, double actual)
        {
            return Math.Pow(desired - actual, 2);
        }



    }
}
