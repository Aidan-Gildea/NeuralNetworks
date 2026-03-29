using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{

    public class ErrorFunction
    {
        public Func<double, double, double> Function { get; private set; }
        public Func<double, double, double> Derivative { get; private set; }
        public ErrorFunction(Func<double, double, double> function, Func<double, double, double> derivative)
        {
            Function = function;
            Derivative = derivative;
        }
    }
    public class ActivationFunction
    {
        public Func<double, double> Function { get; private set; }
        public Func<double, double> Derivative { get; private set; }
        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            Function = function;
            Derivative = derivative;
        }
    }

    public class Perceptron // access modifiers
    {
        public double[] Weights;
        public double Bias;
        private Random myRandom;

        // note that you must manually define the error function and activation function outside of this class and in the inherited

        protected ErrorFunction errorFunction;
        protected ActivationFunction activationFunction;

        public Perceptron(double[] weights, double bias) // manually setup perceptron
        {
            this.Weights = weights;
            this.Bias = bias;
        }

        public Perceptron(double min, double max, int NoOfWeights) // min and max of weights / bias, no of weights
        {
            myRandom = new Random();
            Weights = new double[NoOfWeights];
            // assign the error function outside the definition

            // fill out the random weights based off of min and max weight
            for (int i = 0; i < Weights.Count(); i++)
            {
                Weights[i] = RandomFunctions.RandomNumberBetween(min, max, myRandom);
            }
            Bias = RandomFunctions.RandomNumberBetween(min, max, myRandom);

        }
        
        

        public double Compute(double[] inputs) // this function only works for if you have same no. of weights as inputs
        {
            double total = 0;

            for (int i = 0; i < Weights.Length; i++) 
            {
                total += inputs[i] * Weights[i];
            }
            total += Bias;
            return total; 
        }

        public double[] Compute(double[][] inputs)
        {
            int rows = inputs.GetLength(0);
            int cols = inputs.GetLength(1);
            double[] outputs = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double[] row = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = inputs[i][j];
                }
                outputs[i] = Compute(row);
            }

            return outputs;
        }

        public double GetError(double[][] inputs, double[] desiredOutputs, double[] weights, double bias)
        {
            double accumulatedError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double actual = 0;
                for (int j = 0; j < weights.Length; j++)
                    actual += inputs[i][j] * weights[j];
                actual += bias;

                accumulatedError += errorFunction.Function(desiredOutputs[i], actual);
            }
            return accumulatedError / inputs.Length;
        }

        public double Mutate(double[][] inputs, double[] desiredOutputs, double mutationAmount)
        {
            int mutateIndex = myRandom.Next(2);
            double tempBias = Bias;
            double[] tempWeights = new double[Weights.Length];
            Weights.CopyTo(tempWeights, 0);

            if (mutateIndex == 0) // mutating a weight
            {
                int weightMutateIndex = myRandom.Next(Weights.Length);
                tempWeights[weightMutateIndex] += mutationAmount * ((myRandom.Next(2) * 2) - 1);
            }
            else if (mutateIndex == 1) // mutating the bias
            {
                tempBias += mutationAmount * ((myRandom.Next(2) * 2) - 1);
            }

            double originalError = GetError(inputs, desiredOutputs, Weights, Bias);
            double newError = GetError(inputs, desiredOutputs, tempWeights, tempBias);

            if (newError < originalError)
            {
                tempWeights.CopyTo(Weights, 0);
                Bias = tempBias;
                return newError;
            }

            return originalError;
        }


    }
}
