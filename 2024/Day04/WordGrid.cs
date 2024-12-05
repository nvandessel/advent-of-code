using System;

namespace adventofcode.Y2024.D04
{
    public class WordGrid
    {

        public readonly int Rows;
        public readonly int Cols;
        private readonly string[] _grid;

        public WordGrid(string[] grid)
        {
            _grid = grid;
            Rows = _grid.Length;
            Cols = _grid[0].Length;
        }

        public bool IsWithinBounds(int row, int col)
        {
            var result = row >= 0 && row < Rows && col >= 0 && col < Cols;
            return result;
        }

        public bool DoesWordExist(string word, int startRow, int startCol, int rowDir, int colDir)
        {
            for (int i = 0; i < word.Length; i++)
            {
                var row = startRow + (i * rowDir);
                var col = startCol + (i * colDir);

                if (!IsWithinBounds(row,col) || _grid[row][col] != word[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool DoesPatternExist(int startRow, int startCol, char[,] pattern)
        {
            for (int row = 0; row < pattern.GetLength(0); row++)
            {
                for (int col = 0; col < pattern.GetLength(1); col++)
                {
                    var gridRow = startRow + row;
                    var gridCol = startCol + col;
                    if (!IsWithinBounds(gridRow, gridCol))
                    {
                        return false;
                    }

                    var gridChar = _grid[gridRow][gridCol];
                    var patternChar = pattern[row, col];

                    if (patternChar != '.' && gridChar != patternChar)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
