using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Perceptron
{
    public class ActivationFunctions
    {
        public double BinaryStep(double x) 
        {
            if(x >= 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        public double Sigmoid(double x) 
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double TanH(double x) 
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public double ReLU(double x) 
        {
            if(x <= 0) 
            {
                return 0;
            }
            return x;
        }
}
