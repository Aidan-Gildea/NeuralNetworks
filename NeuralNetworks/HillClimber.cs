using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public class HillClimber
    {
        // what is the benefit of creating fields as properties 
        public string TargetString { get; set; }
        public string CurrentString { get; set; }
        
        public float CurrentError { get; set; }
        
        private Random random = new Random();
        public HillClimber(string targetString) 
        {
            TargetString = targetString;

            CurrentString = GetRandomString(targetString.Length);

        }

        public void Converge() 
        {
            
            CurrentError = CalculateError(CurrentString);

            while (CurrentError != 0) 
            {
                string mutatedString = Mutate();
                float newError = CalculateError(mutatedString);

                if(newError < CurrentError) 
                {
                    CurrentString = mutatedString;
                    CurrentError = newError;
                }
                Console.WriteLine(CurrentString + " => " + TargetString);
                Console.WriteLine("error: " + CurrentError);
            }
        }
        public float CalculateError(string mutatedString) 
        {
            float accumulatedSum = 0; 
            for(int i = 0; i < TargetString.Length; i++) 
            {
                accumulatedSum += Math.Abs(TargetString[i] - mutatedString[i]);
            }

            return accumulatedSum / TargetString.Length;

        }

        private string Mutate() 
        {
            int charIndex = random.Next(0, CurrentString.Length);

            // 1. Convert to char array
            char[] chars = CurrentString.ToCharArray();
            
            // 2. Modify the specific character
            chars[charIndex] = (char)(chars[charIndex] + (random.Next(0, 2) * 2) - 1);
            
            // 3. Create and return a new string
            return new string(chars);
        }

        public string GetRandomString(int length) 
        {
            StringBuilder sb = new StringBuilder();

            while(sb.Length < length) 
            {
                 char myChar = (char)random.Next(32, 126);
                sb.Append(myChar);
            }
            return sb.ToString();
        }
    }
}
