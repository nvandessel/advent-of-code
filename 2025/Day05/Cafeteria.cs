using System;
using System.Text.Json;
using adventofcode.Core;

namespace adventofcode.Y2025.D05
{
    [SolutionAttribute("Cafeteria")]
    public class Cafeteria : Solution
    {
        public override int Year => 2025;
        public override int Day => 05;

        public override SolutionResult Execute(string input)
        {
            var lines = input.Split(['\r', '\n']);
            var pantry = new Pantry(lines);

            var partOne = pantry.FreshIngredientsFound;
            var partTwo = 0L;
            Console.WriteLine(JsonSerializer.Serialize(pantry.OverlappingRanges()));
            foreach (var line in pantry.OverlappingRanges())
            {
                partTwo += line.Item2 - line.Item1 + 1;
            }
            
            return new SolutionResult
            {
                Part1 = partOne.ToString(),
                Part2 = partTwo.ToString() 
            };
        }
    }
}
