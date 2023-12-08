using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2023.D03
{
    [SolutionAttribute("Gear Ratios Solution")]
    public class GearRatiosSolution : Solution
    {
        public override int Year => 2023;
        public override int Day => 03;
        

        public override void Solve()
        {
            var input = GetInput();
            var schematic = new Schematic(input);

            schematic.FindSymbols();

            var partOne = schematic.ReturnPartNumbers().Sum();
            OutputAnswer($"Part One: Sum of Part Numbers = {partOne}");
            
            var partTwo = schematic.ReturnGearNumbers().Sum();
            OutputAnswer($"Part Two: Sum of Gear Numbers = {partTwo}");
        }
    }
}
