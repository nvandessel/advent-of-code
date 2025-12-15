using System;
using System.Collections.Generic;
using adventofcode.Core;

namespace adventofcode.Y2025.D04
{
    [SolutionAttribute("Printing Department")]
    public class PrintingDepartment : Solution
    {
        public override int Year => 2025;
        public override int Day => 04;

        public override SolutionResult Execute(string input)
        {
            var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            var grid = new RollGrid(lines);

            var partOne = 0;
            foreach (var (i, j) in grid.RollIndexes)
            {
                if (grid.HasAdjacent(i, j))
                {
                    partOne++;
                }
            }
            
            var partTwo = 0;
            var hasAdjacent = true;
            while (hasAdjacent)
            {
                hasAdjacent = false;
                var adjacentRolls = new List<(int, int)>();
                foreach (var (i, j) in grid.RollIndexes)
                {
                    if (grid.HasAdjacent(i, j))
                    {
                        hasAdjacent = true;
                        partTwo++;
                        adjacentRolls.Add((i, j));
                    }
                }

                foreach (var (i, j) in adjacentRolls)
                {
                    grid.RemoveRoll(i, j);
                }
            }

            return new SolutionResult
            {
                Part1 = partOne.ToString(),
                Part2 = partTwo.ToString() 
            };
        }
    }
}
