using System;
using System.Linq;
using adventofcode.Core;

namespace adventofcode.Y2024.D02
{
    [SolutionAttribute("Red Nosed Reports")]
    public class RedNosedReports : Solution
    {
        public override int Year => 2024;
        public override int Day => 02;

        public override void Solve()
        {
            var input = GetInput();

            var partOne = SafeReportCount(input, false);
            OutputAnswer("Part One = " + partOne);

            var partTwo = SafeReportCount(input, true);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int SafeReportCount(string input, bool dampener)
        {
            var reports = input.Split("\n");
            return reports.Count(report => IsReportSafe(report, dampener));
        }

        private static bool IsReportSafe(string report, bool allowDampener)
        {
            if (string.IsNullOrEmpty(report)) return false;

            var levels = ParseLevels(report);

            if (IsValidSequence(levels)) return true;

            if (allowDampener)
            {
                return CanBeMadeValidWithOneRemoval(levels);
            }

            return false;
        }

        private static int[] ParseLevels(string report)
        {
            return report.Split(' ').Select(int.Parse).ToArray();
        }

        private static bool IsValidSequence(int[] levels)
        {
            if (levels.Length < 2) return false;

            bool? isAscending = null;

            for (var i = 1; i < levels.Length; i++)
            {
                var difference = levels[i] - levels[i - 1];

                if (Math.Abs(difference) is < 1 or > 3)
                {
                    return false;
                }

                if (isAscending == null)
                {
                    isAscending = difference > 0;
                }
                else if ((isAscending.Value && difference <= 0) || (!isAscending.Value && difference >= 0))
                {
                    return false; 
                }
            }

            return true;
        }

        private static bool CanBeMadeValidWithOneRemoval(int[] levels)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                // Create a new sequence excluding the current level.
                var modifiedSequence = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();

                // Check if the modified sequence is valid.
                if (IsValidSequence(modifiedSequence))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
