using NeuralNetworks.Perceptron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Feed_Forward_neural_Network
{
    public class FFNN
    {
        private Layer Inputs;
        public Layer[] layers;
        ErrorFunction errorFunction;

        public FFNN(ActivationFunction activation, ErrorFunction errorFunction, params int[] neuronsPerLayer) 
        {
            this.errorFunction = errorFunction;
            // placeholder layer to serve as holder for inputs
            Inputs = new Layer(neuronsPerLayer[0]);

            layers = new Layer[neuronsPerLayer.Length];
            for(int i = 0; i < layers.Length; i++) 
            {
                layers[i] = new Layer(activation, neuronsPerLayer[i], i == 0 ? Inputs : layers[i - 1]);
            }
        }

        public void Randomize(Random random, double min, double max) 
        {
            foreach(var layer in layers) 
            {
                layer.Randomize(random, min, max);
            }
        }

        public double[] Compute(double[] inputs) 
        {
            Inputs.SetInputs(inputs);
            foreach(var layer in layers) 
            {
                layer.Compute();
            }
            return layers[^1].Outputs;
        }
    }
}
