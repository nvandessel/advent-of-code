using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode.Y2022.D01
{
    public class Solution
    {
        public const string INPUT = "Input.txt";
        public const string YEAR = "2022";
        public const string DAY = "Day01";

        public static void Solve()
        {
            var input = GetInput();
            
            var partOne = GetCaloriesPerElf(input).FirstOrDefault();
            var partTwo = GetCaloriesPerElf(input).Take(3).Sum();

            Console.WriteLine("Part One: Most Calories = " + partOne);
            Console.WriteLine("Part Two: Top 3 Calories = " + partTwo);
        }
                    
        private static string GetInput()
        {
            var result = "";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), YEAR, DAY, INPUT);
            using (var stream = File.OpenText(filePath))
            {
                result = stream.ReadToEnd();
            }
            return result;
        }

        private static IEnumerable<int> GetCaloriesPerElf(string input)
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