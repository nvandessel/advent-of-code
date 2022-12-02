using System;
using adventofcode.Core;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutions = SolutionFinder.FindSolutions();
            foreach (var solution in solutions)
            {
                var instance = (Solution)Activator.CreateInstance(solution);
                instance.Solve();
            }
        }
    }
}
