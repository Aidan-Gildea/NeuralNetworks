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
            // initialize a private inputs layer with the same size as the input layer
            layers = new Layer[neuronsPerLayer.Length];

            for(int i = 0; i < layers.Length; i++) 
            {
                layers[i] = new Layer(activation, neuronsPerLayer[i], i == 0 ? Inputs : layers[i - 1]);
            }
            layers[0].SetAsInputLayer();
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

        public void Backprop(double[] inputs, double[] desiredOutputs, double learningRate)
        {
            // Delta is a per-example quantity; only WeightUpdate/BiasUpdate accumulate across the batch.
            foreach (var layer in layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.Delta = 0;
                }
            }

            // get the set of outputs from a single pass
            double[] outputs = Compute(inputs);

            // Setting the output layer's neuron's deltas
            Layer OutputLayer = layers[^1];
            for(int i = 0; i < OutputLayer.Outputs.Length; i++) 
            {
                // formula as described in wiki
                Neuron curr = OutputLayer.Neurons[i];
                // HAD AN ERROR: PASSED THE ERROR FUNCTION BACKWARDS
                curr.Delta += errorFunction.Derivative(curr.Output, desiredOutputs[i]);
            }


            // now that I have calculuated the output layer, all others just reference this defined delta
            // Invoke the backpropogation function of each of the subsequent layers in reverse order
            // backprop will iteratively pass through the neural network from front to back and update weights
            // You call the layer, each layer defines the variables for the one that is backpropped after it
            // // kind of recursive but not rly


            for(int i = layers.Length - 1; i >= 0; i--)
            {
                // note that backprop assumes the deltas are already computed, but everything else is new. 
                // HAD AN ERROR: I did layers.Length -2, but that made it not work. 
                layers[i].Backprop(learningRate);
            }

        }


        public void ApplyUpdates(double momentum) 
        {
            // applies all of the accumulated deltas and derivative changes from batch training
            // does not apply to the private input layer

            foreach(var layer in layers) 
            {
                layer.ApplyUpdates(momentum);
            }

        }

        // for supervised learning
        public double Train(double[][] trainingInputs, double[][] trainingOutputs, double learningRate, double momentum) 
        {
            // Train function only goes through one iteration of training

            int numDatapoints = trainingInputs.Length;

            for (int i = 0; i < numDatapoints; i++) 
            {
                Backprop(trainingInputs[i], trainingOutputs[i], learningRate);
            }

            // apply all the updates in the neural network, applying all accumulated values and resetting deltas to 0
            // note that we accumulate and then apply for batch training

            ApplyUpdates(momentum);

            // return average error 
            double AccumulatedError = 0; 
            for(int i = 0; i < numDatapoints; i++) 
            {
                double[] outputs = Compute(trainingInputs[i]);
                AccumulatedError += GetErrorOfOutputs(outputs, trainingOutputs[i]);
            }
            return AccumulatedError / numDatapoints;
            // stopped here, need to implement everything else from the wiki

        }

        public double GetErrorOfOutputs(double[] outputs, double[] desiredOutputs) 
        {
            // the goal of this function is to take a collection of outputs from different neurons and average 
            // them to return a single averaged output

            double AccumulatedError = 0; 
            for(int i = 0; i < outputs.Length; i++) 
            {
                // now working with a single output
                AccumulatedError += errorFunction.Function(outputs[i], desiredOutputs[i]);
            }
            return AccumulatedError / outputs.Length;
            
        }

       
        public double BatchTrain(double[][] inputs, double[][] desiredOutputs, int batchSize, double learningRate, double momentum) 
        {
            List<(double[][] i, double[][] o)> batchedData= BatchData(inputs, desiredOutputs, batchSize);

            double returnError = 100;
            foreach(var data in batchedData)
            {
                returnError = Train(data.i, data.o, learningRate, momentum);
            }

            return returnError;
        }
        private List<(double[][] i, double[][] o)> BatchData(double[][] inputs, double[][] outputs, int batchsize) 
        {
            int startindex = 0;

            List<(double[][] i, double[][] o)> returnItems = new();

            while (startindex < inputs.Length)
            {
                // actual size of THIS batch (smaller for the final leftover batch)
                int currentSize = Math.Min(batchsize, inputs.Length - startindex);

                double[][] a = new double[currentSize][]; // inputs
                double[][] b = new double[currentSize][]; // outputs

                for(int i = 0; i < currentSize; i++)
                {
                    a[i] = inputs[startindex];
                    b[i] = outputs[startindex];

                    startindex++;
                }

                returnItems.Add((a, b));
            }

            return returnItems;
        }
    }
}
