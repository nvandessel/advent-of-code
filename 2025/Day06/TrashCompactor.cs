using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2025.D06
{
    [SolutionAttribute("Trash Compactor")]
    public class TrashCompactor : Solution
    {
        public override int Year => 2025;
        public override int Day => 06;

        public override SolutionResult Execute(string input)
        {
            var worksheet = new Worksheet(input);
            
            var partOne = worksheet.ParseLeftToRight().Sum(p => p.Calculate());
            var partTwo = worksheet.ParseRightToLeft().Sum(p => p.Calculate());
            
            return new SolutionResult
            {
                Part1 = partOne.ToString(),
                Part2 = partTwo.ToString()
            };
        }
    }
}
