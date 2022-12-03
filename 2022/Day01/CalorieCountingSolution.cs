using System;
using System.Collections.Generic;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2022.D01
{
    [SolutionAttribute("Calorie Counting")]
    public class CalorieCountingSolution : Solution
    {
        public override int Year => 2022;
        public override int Day => 01;

        public override void Solve()
        {
            var input = GetInput();
            
            var partOne = GetCaloriesPerElf(input).FirstOrDefault();
            OutputAnswer("Part One: Most Calories = " + partOne);

            var partTwo = GetCaloriesPerElf(input).Take(3).Sum();
            OutputAnswer("Part Two: Top 3 Calories = " + partTwo);
        }

        private IEnumerable<int> GetCaloriesPerElf(string input)
        {
            var perElfData = input.Split("\n\n");
            var result = new List<int>();
            for (int i = 0; i < perElfData.Length; i++)
            {
                var data = perElfData[i].Trim().Split("\n");
                var elfCalories = data.Sum(int.Parse);
                result.Add(elfCalories);
            }
            return result.OrderByDescending(result => result);
        }
    }
}