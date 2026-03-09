### This document stores the previous programs used to test my perceptrons: 

**1. Single perceptron test**
```
using System.Security.Cryptography.X509Certificates;
using NeuralNetworks;
using NeuralNetworks.Perceptron;

namespace MyNewProgram
{

    internal class Program
    {
        
        static void Main(string[] args)
        {
            double[] weights = { 0.75, -1.25 };
            double bias = 0.5;
            Perceptron perceptron = new(weights, bias);

            double[,] test_inputs = { { 0, 0 }, { 0.3, -0.7 }, { 1, 1 }, { -1, -1 }, { -0.5, 0.5 } };

            double[] test_outputs = perceptron.Compute(test_inputs);

            foreach(var output in test_outputs) 
            {
                Console.WriteLine(output.ToString());
            }

        }
    }
}

```

**2. Hill Climber Line of Best Fit Perceptron Test**
```

```