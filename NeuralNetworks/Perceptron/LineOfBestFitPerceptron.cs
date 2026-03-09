using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    public struct Point 
    {
        public double x;
        public double y;
    }
    public class LineOfBestFitPerceptron : Perceptron
    {
        public Point[] points;
        public double[] yValues; // the actual y values, the expected outputs
        public double[] xValues; // x values
        
        public LineOfBestFitPerceptron(Point[] points, double minWeight, double maxWeight) : base(minWeight, maxWeight)
        {
            this.points = points;
            yValues = points.Select(currPoint => currPoint.y).ToArray();
            xValues = points.Select(currPoint => currPoint.x).ToArray();
        }

        public double Mutate(double mutationAmount, Random random)
        {
            double error = ErrorFunctions.MSE(xValues, weights, bias, yValues); 
            int index = random.Next(weights.Length) + 1;
            double[] newWeights = weights;
            double newBias = bias;
            double newError; 

            if (index == weights.Length) // modifying the bias
            {
                newBias += RandomFunctions.RandomNumberBetween(-mutationAmount, mutationAmount);
                newError = ErrorFunctions.MSE(xValues, weights, newBias, yValues);
            }
            else // either add or subtract the mutation amount
            {
                newWeights[index] += RandomFunctions.RandomNumberBetween(-mutationAmount, mutationAmount);
                newError = ErrorFunctions.MSE(xValues, newWeights, bias, yValues);
            }

            if(newError < error) 
            {
                weights = newWeights;
                bias = newBias;
                error = newError;
            }
            // end of mutation
            return newError;

        }
        public string Train(double minErrorRange, double mutationAmount, Random random) 
        {
            double error = ErrorFunctions.MSE(xValues, weights, bias, yValues);
            while(Math.Abs(error) > minErrorRange) 
            {
                Mutate(mutationAmount, random);
                Console.WriteLine($"Error: {error}");
            }



        }


    }

}
