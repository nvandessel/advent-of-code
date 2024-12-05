using System.Collections.Generic;
using adventofcode.Core;

namespace adventofcode.Y2024.D04
{
    [SolutionAttribute("Ceres Search")]
    public class CeresSearch : Solution
    {
        public override int Year => 2024;
        public override int Day => 04;

        private const string WORD = "XMAS";
        private static readonly (int rowDir, int colDir)[] DIRECTIONS = new[]
        {
            ( 0, 1 ),   // Right
            ( 0, -1 ),  // Left
            ( 1, 0 ),   // Down
            ( -1, 0 ),  // Up
            ( 1, 1 ),   // Diagonal Down-Right
            ( 1, -1 ),  // Diagonal Down-Left
            ( -1, 1 ),  // Diagonal Up-Right
            ( -1, -1 )  // Diagonal Up-Left
        };

        public override void Solve()
        {
           var input = GetInput();
           var lines = input.TrimEnd().Split("\n");
           var grid = new WordGrid(lines);

            var partOne = CountWords(grid, WORD);
            OutputAnswer("Part One = " + partOne);

            var patterns = GeneratePatterns();
            var partTwo = CountPatterns(grid, patterns);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int CountWords(WordGrid grid, string word)
        {
            var result = 0;

            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Cols; col++)
                {
                    foreach (var (rowDir, colDir) in DIRECTIONS)
                    {
                        if (grid.DoesWordExist(word, row, col, rowDir, colDir))
                        {
                            result++;
                        }
                    }
                }
            }
            return result;
        }

        private int CountPatterns(WordGrid grid, List<char[,]> patterns)
        {
            var result = 0;
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Cols; col++)
                {
                    foreach (var pattern in patterns)
                    {
                        if (grid.DoesPatternExist(row, col, pattern))
                        {
                            result++;
                        }
                    }

                }
            }
            return result;
        }

        private List<char[,]> GeneratePatterns()
        {
            var patterns = new List<char[,]>();

            // Base cross pattern (M.A.S)
            char[,] basePattern = new char[3, 3]
            {
                { 'M', '.', 'S' },
                { '.', 'A', '.' },
                { 'M', '.', 'S' }
            };

            patterns.Add(basePattern);

            char[,] rotated90 = RotatePattern(basePattern);
            patterns.Add(rotated90);

            char[,] rotated180 = RotatePattern(rotated90);
            patterns.Add(rotated180);

            char[,] rotated270 = RotatePattern(rotated180);
            patterns.Add(rotated270);

            return patterns;

        }

        private char[,] RotatePattern(char[,] pattern)
        {
            int size = pattern.GetLength(0);
            char[,] rotated = new char[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    rotated[c, size - 1 - r] = pattern[r, c];
                }
            }

            return rotated;
        }
    }
}
