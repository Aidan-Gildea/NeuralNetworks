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

        public double xMin, xMax; // original x range, stored for unnormalization
        public double yMin, yMax; // original y range, stored for unnormalization
        public bool normalize;
        // stochastic = one at a time
        // batch training is multiple inputs at once (a batch of inputs)

        

        public LineOfBestFitPerceptron(Point[] points, double min, double max, double learningRate, bool normalize) : base(min, max, 1, learningRate)
        {
            this.normalize = normalize;

            yValues = points.Select(currPoint => currPoint.y).ToArray();
            double[] tempxValues = points.Select(currPoint => currPoint.x).ToArray();

            // will normalize the data if bool normalize is set to true
            if (normalize)
            {
                xMin = tempxValues.Min();
                xMax = tempxValues.Max();
                yMin = yValues.Min();
                yMax = yValues.Max();

                tempxValues = NormalizationFunctions.Normalize(tempxValues, xMin, xMax, 0, 1);
                yValues = NormalizationFunctions.Normalize(yValues, yMin, yMax, 0, 1);
            }

            xValues = new double[points.Length][];
            for (int i = 0; i < xValues.Length; i++)
            {
                xValues[i] = new double[1];
                xValues[i][0] = tempxValues[i];
            }

            // error and activation functions are set
            errorFunction = new(ErrorFunctions.MSE, ErrorFunctions.MSEDerivative);
            activationFunction = new(ActivationFunctions.Identity, ActivationFunctions.IdentityDerivative);
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


            // i don't understand this math from unnormalizing
            double slope = Weights[0] * (yMax - yMin) / (xMax - xMin);
            double yIntercept = Bias * (yMax - yMin) + yMin - slope * xMin;

            Console.WriteLine($"Final Line of best fit: y = {slope}x + {yIntercept}");

        }

        public void BatchTrain(double maxEpochs) 
        {
            int epochs = 0;
            while(GetError(xValues, yValues, Weights, Bias) > 0 && epochs < maxEpochs) 
            {

                double[] accumulatedWeightChanges = new double[Weights.Length];
                double accumulatedBiasChange = 0;

                for (int i = 0; i < xValues.Length; i++)
                {
                    double output = Compute(xValues[i]);
                    double desiredOutput = yValues[i];
                    double activationInput = Weights[0] * xValues[i][0] + Bias;

                    accumulatedWeightChanges[0] += ErrorToWeightPartialDerivative(output, desiredOutput, activationInput);
                    accumulatedBiasChange += ErrorToBiasPartialDerivative(output, desiredOutput, activationInput);
                }

                // Apply the accumulated changes
                for (int i = 0; i < Weights.Length; i++)
                {
                    Weights[i] += accumulatedWeightChanges[i];
                }
                Bias += accumulatedBiasChange;

            }

        }


        private double ErrorToWeightPartialDerivative(double output, double desiredOutput, double activationInput) 
        {
            return errorFunction.Derivative(output, desiredOutput) * activationFunction.Derivative(activationInput) * -LearningRate;
        }

        private double ErrorToBiasPartialDerivative(double output, double desiredOutput, double activationInput) 
        {
            return errorFunction.Derivative(output, desiredOutput) * activationFunction.Derivative(activationInput) * -LearningRate;
        }
    }

}
