using NeuralNetworks.Feed_Forward_neural_Network;
using NeuralNetworks.Perceptron;


namespace Backpropogation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new();
            ActivationFunction activationFunction = new(ActivationFunctions.TanH, ActivationFunctions.TanHDerivative);
            ErrorFunction errorFunction = new(ErrorFunctions.MSE, ErrorFunctions.MSEDerivative);

            FFNN xorEstimator = new FFNN(activationFunction, errorFunction, 1, 50, 50, 1);
            xorEstimator.Randomize(random, -1, 1);

            double[][] inputs =
            {
                [0, 0],
                [0, 1],
                [1, 0],
                [1, 1]
            };

            double[][] outputs = 
            {
                [0],
                [1],
                [1],
                [0]
            };

            double error = 1.0;
            for(int i = 0; i < 100000; i++)
            {  // input, output, learning rate
                error = xorEstimator.BatchTrain(inputs, outputs,4, 0.01, 0.1);

                // error base case
                if (error <= 0.01) break; 
                
                Console.WriteLine($"Error: {error}");
            }

            Console.WriteLine("Training Complete");

            Console.WriteLine();

            // compute the values for checking
            Console.WriteLine("Computed Values: ");
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] computedValue = xorEstimator.Compute(inputs[i]);
                Console.WriteLine($"  [{inputs[i][0]}, {inputs[i][1]}] -> {computedValue[0]:F4}  (expected {outputs[i][0]})");
            }
        }
    }
}
