using System;
using adventofcode.Core;

namespace adventofcode.Y9999.D01
{
    /// <summary>
    /// Test solution to validate the watch mode system.
    /// Year 9999 is used to clearly indicate this is a test/example.
    /// 
    /// To test:
    ///   dotnet run solve-sample -y 9999 -d 1
    ///   dotnet run solve -y 9999 -d 1
    ///   ./watch-solution.sh 9999 1
    /// </summary>
    [SolutionAttribute("Test Solution")]
    public class TestSolution : Solution
    {
        public override int Year => 9999;
        public override int Day => 1;

        public override SolutionResult Execute(string input)
        {
            // Parse input
            var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            
            // Simple test: count lines
            var lineCount = lines.Length;
            var charCount = input.Length;
            
            return new SolutionResult
            {
                Part1 = lineCount.ToString(),
                Part2 = charCount.ToString()
            };
        }
    }
}

