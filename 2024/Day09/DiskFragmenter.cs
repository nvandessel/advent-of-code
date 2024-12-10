using adventofcode.Core;

namespace adventofcode.Y2024.D09
{
    [SolutionAttribute("Disk Fragmenter")]
    public class DiskFragmenter : Solution
    {
        public override int Year => 2024;
        public override int Day => 09;

        public override void Solve()
        {
            var input = GetInput();

            var c = new ChatGptSolution();
            c.SolvePartTwo(input);

            var disk1 = new Disk(input);
            disk1.SortBySequence();
            OutputAnswer("Part One = " + disk1.CalculateChecksum());

            var disk2 = new Disk(input);
            disk2.SortByFiles();
            OutputAnswer("Part Two = " + disk2.CalculateChecksum());
        }

    }
}
