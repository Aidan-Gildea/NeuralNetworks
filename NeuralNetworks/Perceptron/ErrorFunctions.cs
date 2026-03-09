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


        // linear regression hill climber using perceptrons and mutation T-T

        public static double MSE(double[] inputs, double[] weights, double bias, double[] targets) 
        {
            double accumulatedSum = 0; 

            for(int i = 0; i < inputs.Length; i++) 
            {
                double actualValue = (inputs[i] * weights[i] + bias);
                accumulatedSum += (targets[i] - actualValue);
            }

            return accumulatedSum / targets.Length;
        }


        //static float MAE(double[] inputs, double weights, double bias, double target) 
        //{
        //    // 1. calculate the weights
        //}
    }
}
