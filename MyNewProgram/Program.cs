using NeuralNetworks.Perceptron;

namespace MyNewProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Points roughly on y = 2x + 1 with slight noise
            // Plug these into Desmos to verify: (1,3.1), (2,5.0), (3,6.8), (4,9.2), (5,11.1)
            Point[] points =
            {
                new Point { x = 1, y = 3.1 },
                new Point { x = 2, y = 5.0 },
                new Point { x = 3, y = 6.8 },
                new Point { x = 4, y = 9.2 },
                new Point { x = 5, y = 11.1 },
            };

            LineOfBestFitPerceptron perceptron = new(points, -10, 10, normalize: true);
            perceptron.Train(minErrorRange: 0.0001, maxIterations: 1000000, mutateAmount: 0.0005);
        }
    }
}
