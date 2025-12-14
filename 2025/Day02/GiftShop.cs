using System;
using System.Collections.Generic;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2025.D02
{
    [SolutionAttribute("Gift Shop")]
    public class GiftShop : Solution
    {
        public override int Year => 2025;
        public override int Day => 02;

        public override void Solve()
        {
            var input = GetInput();
            var line = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            double partOne = 0;
            double partTwo = 0;
            
            foreach (var range in line)
            {
                var split = range.Split('-', StringSplitOptions.RemoveEmptyEntries);
                var start = double.Parse(split[0]);
                var end = double.Parse(split[1]);

                for (var i = start; i <= end; i++)
                {
                    if (DoesRepeat(i.ToString()))
                    {
                        partOne += i;
                        partTwo += i;
                    }
                    else if (DoesRepeatPartTwo(i.ToString()))
                    {
                        partTwo += i;
                    }
                }
            }
            OutputAnswer($"Part One = {partOne}");
            OutputAnswer($"Part Two = {partTwo}");
        }
        
        public bool DoesRepeat(string input)
        {
            var prev = input[0].ToString();
            for (var i = 1; i <= input.Length / 2; i++)
            {
                var next = input[i..];
                if (prev == next)
                {
                    return true;
                }
                prev += input[i];
            }
            return false;
        }
        
        public bool DoesRepeatPartTwo(string input)
        {
            if (input.Length <= 2)
            {
                return false;
            }
            var found = false;
            var prev = input[..2];
            var c = input[0];
            if (input.Length % 2 != 0 && input.All(x =>  x == c))
            {
                return true;
            }
            for (var i = 2; i < input.Length;)
            {
                var endIndex = input.Length;
                if (i + prev.Length <= input.Length)
                {
                    endIndex = i + prev.Length;
                }
                var next = input[i..endIndex];
                if (prev == next)
                {
                    found = true;
                }
                if (found && prev != next)
                {
                    return false;
                }
                if (found)
                {
                    i+= prev.Length;
                }
                else
                {
                    prev += input[i];
                    i++;
                }
            }
            return found;
        }
    }
}
