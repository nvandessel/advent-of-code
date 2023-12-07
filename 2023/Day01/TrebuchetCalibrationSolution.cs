using System;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2023.D01
{
    [SolutionAttribute("Trebuchet Calibration")]
    public class TrebuchetCalibrationSolution : Solution
    {
        public override int Year => 2023;
        public override int Day => 01;
        
        private readonly string[] _numberStrings = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

        public override void Solve()
        {
            var input = GetInput();
            
            var partOne = GetCalibratedValue(input);
            OutputAnswer("Part One: Calibrated value = " + partOne);
            
            var partTwo = GetCalibratedValue(input, includeStrings: true);
            OutputAnswer("Part Two: Calibrated value = " + partTwo);
        }

        private int GetCalibratedValue(string input, bool includeStrings = false)
        {
            var lines = input.Split("\n");
            return lines.Sum(line => GetLineValue(line, includeStrings));
        }

        private int GetLineValue(string input, bool includeStrings)
        {
            var result = "";
            var chars = input.ToArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (HasValue(chars[i], i))
                {
                    break;
                }
            }
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if (HasValue(chars[i], i))
                {
                    break;
                }
            }
            return int.Parse(result);

            bool HasValue(char c, int i)
            {
                if (char.IsNumber(c))
                {
                    result += c;
                    return true;
                }
                if (includeStrings && TryMatchNumberWord(input[i..], out var number))
                {
                    result += number;
                    return true;
                }
                return false;
            }
        }
        
        private bool TryMatchNumberWord(string input, out int number)
        {
            for (var i = 0; i < _numberStrings.Length; i++)
            {
                var word = _numberStrings[i];
                if (input.StartsWith(word, StringComparison.OrdinalIgnoreCase))
                {
                    number = i + 1;
                    return true;
                }
            }
            number = 0;
            return false;
        }
    }
}