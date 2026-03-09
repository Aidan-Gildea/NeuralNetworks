using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    
    public class Perceptron
    {
        public double[] weights;
        public double bias;

        
        public Perceptron(double[] weights, double bias) 
        {
            this.weights = weights;
            this.bias = bias;
        }

        public Perceptron(double min, double max)
        {
            // fill out the random weights based off of min and max weight
            for (int i = 0; i < weights.Count(); i++)
            {
                weights[i] = RandomFunctions.RandomNumberBetween(min, max);
            }
            bias = RandomFunctions.RandomNumberBetween(min, max);
        }


        public double Compute(double[] inputs) 
        {
            double total = 0;

            for (int i = 0; i < weights.Length; i++) 
            {
                total += inputs[i] * weights[i];
            }
            total += bias;
            return total; 
        }

        public double[] Compute(double[,] inputs) 
        {
            int rows = inputs.GetLength(0);
            int cols = inputs.GetLength(1);
            double[] outputs = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double[] row = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = inputs[i, j];
                }
                outputs[i] = Compute(row);
            }

            return outputs;
        }

        
    }
}
