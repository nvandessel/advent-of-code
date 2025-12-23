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
            
            return new SolutionResult
            {
                Part1 = manifold.CountSplits().ToString(),
                Part2 = manifold.CountTimelines().ToString()
            };
        }
    }
}
