using System;
using System.Text.RegularExpressions;
using adventofcode.Core;

namespace adventofcode.Y2024.D03
{
    [SolutionAttribute("Mull It Over")]
    public class MullItOver : Solution
    {
        public override int Year => 2024;
        public override int Day => 03;

        private const string Pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        private const string PatternConditional = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";

        public override void Solve()
        {
            var input = GetInput();

            var partOne = ComputeCorrupted(input);
            OutputAnswer("Part One = " + partOne);

            var partTwo = ComputeCorruptedConditionals(input);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int ComputeCorrupted(string input)
        {
            var matches = Regex.Matches(input, Pattern);
            var result = 0;

            foreach (Match match in matches)
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                
                result += x * y;
            }

            return result;
        }
        
        private int ComputeCorruptedConditionals(string input)
        {
            var matches = Regex.Matches(input, PatternConditional);
            var result = 0;
            var shouldCompute = true;

            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("mul"))
                {
                    if (shouldCompute == false)
                    {
                        continue;
                    }
                    
                    var x = int.Parse(match.Groups[1].Value);
                    var y = int.Parse(match.Groups[2].Value);
                    result += x * y;
                }
                else if (match.Value.StartsWith("do("))
                {
                    shouldCompute = true;
                }
                else if (match.Value.StartsWith("don't("))
                {
                    shouldCompute = false;
                }
                
            }

            return result;
        }
    }
}
