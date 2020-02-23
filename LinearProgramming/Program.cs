using System;

namespace LinearProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var intro = new EwIntro();
            intro.Run();

            var wire = new EwWire();
            wire.Run();

            var knapsack = new EwKnapsack();
            knapsack.Run();

            Console.ReadKey();
        }
    }
}
