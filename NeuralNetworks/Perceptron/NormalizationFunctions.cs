using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    public static class NormalizationFunctions
    {
        // Normalizes x from [min, max] into [nMin, nMax]
        public static double Normalize(double x, double min, double max, double nMin, double nMax)
        {
            return ((x - min) / (max - min)) * (nMax - nMin) + nMin;
        }

        // Unnormalizes x from [nMin, nMax] back into [min, max]
        public static double Unnormalize(double x, double min, double max, double nMin, double nMax)
        {
            return ((x - nMin) / (nMax - nMin)) * (max - min) + min;
        }

        public static double[] Normalize(double[] data, double min, double max, double nMin, double nMax)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                result[i] = Normalize(data[i], min, max, nMin, nMax);
            return result;
        }

        public static double[] Unnormalize(double[] data, double min, double max, double nMin, double nMax)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                result[i] = Unnormalize(data[i], min, max, nMin, nMax);
            return result;
        }

        public static double[][] Normalize(double[][] data, double min, double max, double nMin, double nMax)
        {
            double[][] result = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
                result[i] = Normalize(data[i], min, max, nMin, nMax);
            return result;
        }

        public static double[][] Unnormalize(double[][] data, double min, double max, double nMin, double nMax)
        {
            double[][] result = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
                result[i] = Unnormalize(data[i], min, max, nMin, nMax);
            return result;
        }
    }
}
