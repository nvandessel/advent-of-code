using System;
using System.Collections.Generic;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2023.D02
{
    [SolutionAttribute("Cube Conundrum Solution")]
    public class CubeConundrumSolution : Solution
    {
        public override int Year => 2023;
        public override int Day => 02;

        private readonly Dictionary<CubeColor, int> _conditions = new()
        {
            {CubeColor.Red, 12},
            {CubeColor.Green, 13},
            {CubeColor.Blue, 14},
        };

        public override void Solve()
        {
            var input = GetInput();
            var lines = input.Split("\n");
            var games = lines.Select(line => new Game(line)).ToList();
            
            var partOne = GetGameSum(games);
            OutputAnswer($"Part One: Game Sum = {partOne}");
            
            var partTwo = GetGamePower(games);
            OutputAnswer($"Part Two: Game Power = {partTwo}");
        }

        private int GetGameSum(IEnumerable<Game> games)
        {
            return games.Where(IsGameValid).Sum(game => game.Number);
        }
        
        private bool IsGameValid(Game game)
        {
            var result = true;
            foreach (var condition in _conditions)
            {
                foreach (var gameSet in game.Sets)
                {
                    if (gameSet.TryGetValue(condition.Key, out var value))
                    {
                        if (value > condition.Value)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private int GetGamePower(IEnumerable<Game> games)
        {
            var result = 0;
            foreach (var game in games)
            {
                var power = 1;
                foreach (var minimumRequiredCube in game.MinimumRequiredCubes)
                {
                    power *= minimumRequiredCube.Value;
                }
                result += power;
            }
            return result;
        }
    }
}
