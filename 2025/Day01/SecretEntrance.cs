using System;
using adventofcode.Core;

namespace adventofcode.Y2025.D01
{
    [SolutionAttribute("Secret Entrance")]
    public class SecretEntrance : Solution
    {
        public override int Year => 2025;
        public override int Day => 01;

        public override void Solve()
        {
            var input = GetInput().Trim();
            var dial = new Dial(50, 0, 99, 0);
            foreach (var line in input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
            {
                var rotation = int.Parse(line[1..]);
                var delta = line[0] switch
                {
                    'L' => -rotation,
                    'R' => rotation,
                    _ => throw new Exception()
                };
                
                dial.Rotate(delta);
            }

            OutputAnswer($"Part One = {dial.PartOneTracker}");
            OutputAnswer($"Part Two = {dial.PartTwoTracker}");
        }
    }
}
