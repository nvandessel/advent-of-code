using System;
using System.Collections.Generic;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2025.D03
{
    [SolutionAttribute("Lobby")]
    public class Lobby : Solution
    {
        public override int Year => 2025;
        public override int Day => 03;

        public override void Solve()
        {
            var input = GetInput();
            var banks = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var partOne = 0UL;
            var partTwo = 0UL;
            foreach (var bank in banks)
            {
                partOne += HighestSubsequence(bank, 2);
                partTwo += HighestSubsequence(bank, 12);
            }

            OutputAnswer($"PartOne = {partOne}");
            OutputAnswer($"PartTwo = {partTwo}");
        }

        private ulong HighestSubsequence(string sequence, int sequenceLength)
        {
            var startIndex = 0;
            var result = new List<int>();

            while (result.Count < sequenceLength)
            {
                var remaining = sequenceLength - result.Count;
                var maxIndex = sequence.Length - remaining;

                var highest = -1;
                var highestIndex = -1;

                for (int i = startIndex; i <= maxIndex; i++)
                {
                    var digit = sequence[i] - '0'; 
                    if (digit > highest)
                    {
                        highest = digit;
                        highestIndex = i;
                    }
                }
                
                result.Add(highest);
                startIndex = highestIndex + 1;
            }
            
            var resultString =  string.Join("", result);
            return ulong.Parse(resultString);
        }
    }
}
