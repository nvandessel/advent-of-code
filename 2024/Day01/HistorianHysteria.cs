using System;
using System.Collections.Generic;
using adventofcode.Core;

namespace adventofcode.Y2024.D01
{
    [SolutionAttribute("Historian Hysteria")]
    public class HistorianHysteria : Solution
    {
        public override int Year => 2024;
        public override int Day => 01;

        public override void Solve()
        {
            var input = GetInput();
            var lists = GetLists(input);

            var partOne = GetDistance(lists.left, lists.right);
            OutputAnswer("Part One = " + partOne);
            
            var partTwo = GetSimilarity(lists.left, lists.right);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int GetDistance(List<int> left, List<int> right)
        {
            left.Sort();
            right.Sort();
            var result = 0;
            for (int i = 0; i < left.Count; i++)
            {
                result += Math.Abs(left[i] - right[i]);
            }
            return result;
        }

        private int GetSimilarity(List<int> left, List<int> right)
        {
            var rightCount = new Dictionary<int, int>();
            foreach (var number in right)
            {
                if(rightCount.ContainsKey(number))
                {
                  rightCount[number]++;
                }
                else
                {
                  rightCount.Add(number, 1);
                }
            }

            var result = 0;
            foreach (var number in left)
            {
                if (rightCount.ContainsKey(number))
                {
                  result += number * rightCount[number];
                }
            }

            return result;
        }

        private (List<int> left, List<int> right) GetLists(string input)
        {
            var lines = input.Split("\n");
            var left = new List<int>();
            var right = new List<int>();
            
            foreach (var line in lines)
            {
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }
                var values = line.Split("   ");
                left.Add(Int32.Parse(values[0]));
                right.Add(Int32.Parse(values[1]));
            }

            return (left, right);
        }
    }
}
