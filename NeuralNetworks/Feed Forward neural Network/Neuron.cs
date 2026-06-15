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

        public bool IsInput = false; 

        Dendrite[] dendrites;
        public Dendrite[] Dendrites => dendrites;

        public double[] Weights => dendrites.Select(d => d.Weight).ToArray();

        public double momentum { get; set; }
        public double previousBiasUpdate { get; set; }
        // a value to represent how important the decreasing of a single neuron is to the decreasing of the cost function
        
        // ------------- now for the backpropogation variables
        // note that delta is also an accumulator as if you actually directly updated, it would influence all of the other nodes that reference it
        public double Delta { get; set; }
        public double BiasUpdate { get; set; }

        // weight updates are stored in the dendrite class
        // --------------


        public double Output { get; set; }
        public double Raw { get; private set; }
        public ActivationFunction Activation { get; set; }

        public Neuron(ActivationFunction activation, Neuron[] previousNeurons) 
        {
            Activation = activation;
            dendrites = new Dendrite[previousNeurons.Length];
            Delta = 0;
            BiasUpdate = 0; 

            for (int i = 0; i < previousNeurons.Length; i++)
            {
                dendrites[i] = new Dendrite(previousNeurons[i], this, 0);
            }

            previousBiasUpdate = 0; 
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

        public void CopyTo(Neuron neuron) 
        {
            neuron.Bias = Bias;

            for(int i = 0; i < neuron.dendrites.Length; i++) 
            {
                neuron.dendrites[i].Weight = dendrites[i].Weight;
            }
        }

        public void BackProp(double learningRate) 
        {
            // update weight accumulators
            foreach(var dendrite in dendrites)
            {
                double weightDerivative = Delta * Activation.Derivative(Raw) * dendrite.Previous.Output;
                dendrite.WeightUpdate += -learningRate * weightDerivative;
            }

            // update bias accumulator
            double biasDerivative = Delta * Activation.Derivative(Raw);
            BiasUpdate += -learningRate * biasDerivative;

            // HAD AN ERROR: I did -1 * biasDerivative but i was missing the learning rate.

            // set the deltas of the previous nodes

            foreach(var dendrite in dendrites) 
            {
                Neuron prev = dendrite.Previous;

                prev.Delta += Delta * Activation.Derivative(Raw) * dendrite.Weight;
            }
        }

        public void ApplyUpdates(double momentum) 
        {
            // -- apply weight & bias accumulators

            // apply weights


            foreach(var dendrite in dendrites) 
            {
                dendrite.ApplyUpdates(momentum);
            }

            // update bias with momentum accounted for
            BiasUpdate += previousBiasUpdate * momentum;
            Bias += BiasUpdate;

            previousBiasUpdate = BiasUpdate;
            BiasUpdate = 0;

            // reset delta for next iteration
            Delta = 0; 

        }
    }
}
