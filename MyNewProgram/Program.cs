using System.Security.Cryptography.X509Certificates;
using NeuralNetworks;

namespace MyNewProgram
{

    internal class Program
    {
        
        static void Main(string[] args)
        {
            HillClimber hillClimber = new HillClimber("Hello World!");

            hillClimber.Converge();
        }
    }
}
