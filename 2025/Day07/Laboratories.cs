using System;
using adventofcode.Core;

namespace adventofcode.Y2025.D07
{
    [SolutionAttribute("Laboratories")]
    public class Laboratories : Solution
    {
        public override int Year => 2025;
        public override int Day => 07;

        public override SolutionResult Execute(string input)
        {
            var manifold = new Manifold(input);
            manifold.ProcessBeams();
            
            return new SolutionResult
            {
                Part1 = manifold.TimesSplit.ToString(),
                Part2 = manifold.CountTimelines().ToString() 
            };
        }
    }
}
