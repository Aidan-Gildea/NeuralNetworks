namespace NeuralNetworks
{
    // there are different accessibility modifiers for methods / fields, but accessibility levels for classes 
    // protected -- only acccessible within the class and its derived classes
    // protected internal 
    // private classes only within the namespace
    // public is accessible outside of namespace nad in other projects
    // all the others are kinda useueslles

    public class DelegatesExample
    {
        public void DoSomething(int x, float y)
        {
            Console.WriteLine($"Doing something with {x} and {y}");
        }

        public Action<int, float> doSomethingDelegate; 

        public bool AreSame(int x, int y) => x == y; // inline function

        public Func<int, int, bool> areSameDelegate;

        public int a = 5;

        // -----------LAMBDA FUNCTIONS------------------

        int Add(int x, int y, int z) 
        {
            return x + y + z;
        }

        // 1. 
        // here is how to define a lambda with the same thing
        // (x,y,z)=>
        // {
        //      return x + y = z;
        // }


        // 2. 
        Func<int, int, int, int> addDelegate = (x, y, z) => x + y + z; // this is a lambda function that takes 3 ints and returns an int, stored in a delegate

        public void Delegatehandler() 
        {
            doSomethingDelegate = DoSomething; // Assign the method to the delegate
            doSomethingDelegate(5, 3.14f); // Call the delegate
            DoSomething(5, 3.14f); // call the function
            // notice how the function and delegate can be called the same way :D

            bool a = AreSame(1, 2);
            bool b = areSameDelegate(1, 2);


        }
    }
}
