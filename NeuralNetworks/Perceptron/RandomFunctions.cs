using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    public static class RandomFunctions
    {
        // shared within your project a single instance of this random, other can access 
        // but primarily only made for the random functions within it

        public static double RandomNumberBetween(double minValue, double maxValue, Random random)
        {
            var next = random.NextDouble(); // returns a double between 0 and 1

            return (next * (maxValue - minValue)) + minValue;
        }
    }
}
