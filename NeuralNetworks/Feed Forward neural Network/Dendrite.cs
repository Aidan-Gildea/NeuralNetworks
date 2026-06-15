using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public double WeightUpdate { get; set; }

        public double previousWeightUpdate { get; set; }

        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous;
            Next = next;
            Weight = weight;
            previousWeightUpdate = 0; 
        }

        public double Compute() 
        {
            return Previous.Output * Weight;
        }

        public void ApplyUpdates(double momentum) 
        {
            // apply weight updates with momentum
            WeightUpdate += previousWeightUpdate * momentum; 
            
            Weight += WeightUpdate;

            previousWeightUpdate = WeightUpdate;

            WeightUpdate = 0; 
        }
    }
}
