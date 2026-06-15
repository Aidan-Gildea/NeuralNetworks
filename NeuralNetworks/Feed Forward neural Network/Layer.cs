using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks.Perceptron;

namespace NeuralNetworks.Feed_Forward_neural_Network
{
    public class Layer
    {
        public Neuron[] Neurons { get; }

        public double[] Outputs { get; private set; }

        // standard layer constructor
        public Layer(ActivationFunction activation, int neuronCount, Layer previousLayer)
        {
            Neurons = new Neuron[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                Neurons[i] = new Neuron(activation, previousLayer.Neurons);
            }
        }

        // constructor for the input layer, 
        public Layer(int inputCount)
        {
            Neurons = new Neuron[inputCount];
            for (int i = 0; i < inputCount; i++)
            {
                Neurons[i] = new Neuron(null, Array.Empty<Neuron>());
            }
            Outputs = new double[inputCount];
        }

        // set the outputs of the input layer, will be used to set the outputs of the input layer and the output of each neuron in the input layer
        // basically manually setting up a layer with a bunch of neurons whose 
        public void SetInputs(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Neurons[i].Output = values[i];
                Outputs[i] = values[i];
            }
        }

        // randomize all of the neurons; pass of to the neuron native randomization function
        public void Randomize(Random random, double min, double max)
        {
            foreach (var neuron in Neurons)
            {
                neuron.Randomize(random, min, max);
            }
        }

        // compute for standard layer, will compute the output of each neuron and store it in the outputs array
        public double[] Compute()
        {
            Outputs = new double[Neurons.Length];
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Compute();
                Outputs[i] = Neurons[i].Output;
            }
            return Outputs;
        }

        public void Backprop(double learningRate) 
        {
            // backprops all of the layer's neurons
            foreach(var neuron in Neurons) 
            {
                neuron.BackProp(learningRate);
            }
        }

        public void SetAsInputLayer() 
        {
            foreach(var neuron in Neurons) 
            {
                neuron.IsInput = true;
            }
        }

        public void ApplyUpdates(double momentum) 
        {
            foreach(var neuron in Neurons) 
            {
                neuron.ApplyUpdates(momentum);
            }
        }
    }
}
