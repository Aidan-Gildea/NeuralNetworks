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
        public double[] yValues; // the desired y values and expected outputs
        public double[][] xValues; // inputs, stored as a 2d array where each x is an input aligns with a single weight

        private static double MSE(double desired, double actual)
        {
            return Math.Pow(desired - actual, 2);
        }

        public LineOfBestFitPerceptron(Point[] points, double min, double max) : base(min, max, 1)
        {
            yValues = points.Select(currPoint => currPoint.y).ToArray();
            double[] tempxValues = points.Select(currPoint => currPoint.x).ToArray();

            // create a jagged array of all the different values to pass into the error function

            xValues = new double[points.Length][];
            for (int i = 0; i < xValues.Length; i++)
            {
                xValues[i] = new double[1];
                xValues[i][0] = tempxValues[i];
            }
            ErrorFunction = MSE;
        }



        public void Train(double minErrorRange, double maxIterations, double mutateAmount) 
        {
            double error = GetError(xValues, yValues, Weights, Bias);
            int counter = 0; 

            while(error > minErrorRange && counter <= maxIterations) 
            {
                error = Mutate(xValues, yValues, mutateAmount); // will mutate and return the new error
                Console.WriteLine($"Slope: {Weights[0]}, Y Offset: {Bias}, Error: {error}");
                counter++;
            }

            Console.WriteLine($"Final Line of best fit: y = {Weights[0]}x + {Bias}");

        }


    }

}
