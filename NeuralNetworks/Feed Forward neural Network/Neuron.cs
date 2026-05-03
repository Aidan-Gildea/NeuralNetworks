using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks.Perceptron;

namespace NeuralNetworks.Feed_Forward_neural_Network
{
    public class Neuron
    {
        public double Bias;
        
        Dendrite[] dendrites;

        public double Output { get; set; }
        public double Raw { get; private set; }
        public ActivationFunction Activation { get; set; }

        public Neuron(ActivationFunction activation, Neuron[] previousNeurons) 
        {
            Activation = activation;
            dendrites = new Dendrite[previousNeurons.Length];
            for (int i = 0; i < previousNeurons.Length; i++)
            {
                dendrites[i] = new Dendrite(previousNeurons[i], this, 0);
            }
            // all weights default to 0
        }

        public void Randomize(Random random, double min, double max) 
        {
            Bias = random.NextDouble() * (max - min) + min;
            foreach (var dendrite in dendrites)
            {
                dendrite.Weight = random.NextDouble() * (max - min) + min;
            }
        }

        // if placeholder input neurons, then compute is never actually run :) The value that this neuron will output is stored
        // in output (constant) which was manually set. 
        public void Compute()
        {
            Raw = Bias;
            foreach(var dendrite in dendrites)
            {
                Raw += dendrite.Compute();
            }
            Output = Activation.Function(Raw);
        }
    }
}
