using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Feed_Forward_neural_Network
{
    public class Dendrite
    {
        public Neuron Previous { get; }
        public Neuron Next { get; }

        public double Weight { get; set; }

        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous;
            Next = next;
            Weight = weight;
        }

        public double Compute() 
        {
            return Previous.Output * Weight;
        }
    }
}
