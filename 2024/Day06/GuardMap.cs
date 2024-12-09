using System.Collections.Generic;

namespace adventofcode.Y2024.D06
{
    public class GuardMap
    {
        public readonly int Rows;
        public readonly int Cols;

        public readonly (int row, int col) GuardStartPosition;
        public readonly (int rowDir, int colDir) GuardStartDirection;

        private const char OBSTACLE = '#';
        private const char UP = '^';
        private const char DOWN = 'v';
        private const char LEFT = '<';
        private const char RIGHT = '>';
        private static readonly Dictionary<char, (int rowDir, int colDir)> DIRECTIONS = new()
        {
            { UP, ( -1, 0 ) },
            { DOWN, ( 1, 0 ) },
            { LEFT, ( 0, -1 ) },
            { RIGHT, ( 0, 1 ) },
        };
        private static readonly Dictionary<(int rowDir, int colDir), char> DIRECTIONS_REV_LOOKUP = new()
        {
            { ( -1, 0 ), UP },
            { ( 1, 0 ), DOWN },
            { ( 0, -1 ), LEFT },
            { ( 0, 1 ), RIGHT },
        };

        private char[,] _map;

        public GuardMap(string input)
        {
            var lines = input.Trim().Split("\n");
            Rows = lines.Length;
            Cols = lines[0].Length;
            _map = new char[Rows, Cols];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    _map[row, col] = lines[row][col];
                    foreach (var dir in DIRECTIONS.Keys)
                    {
                        if (lines[row][col] != dir)
                        {
                            continue;
                        }

                        GuardStartPosition = (row, col);
                        GuardStartDirection = DIRECTIONS[dir];
                    }
                }
            }
        }

        public void AddTemporaryObstacle((int row, int col) position)
        {
            _map[position.row, position.col] = OBSTACLE;
        }

        public void RemoveTemporaryObstacle((int row, int col) position)
        {
            _map[position.row, position.col] = '.';
        }

        public bool IsWithinMap(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Cols;
        }

        public bool IsObstacle(int row, int col)
        {
            if (_map[row, col] == OBSTACLE)
            {
                return true;
            }
            return false;
        }

        public (int rowDir, int colDir) NextDirection((int rowDir, int colDir) curDir)
        {
            return curDir switch
            {
                (-1, 0) => DIRECTIONS[RIGHT], // UP → RIGHT
                    (0, 1) => DIRECTIONS[DOWN],  // RIGHT → DOWN
                    (1, 0) => DIRECTIONS[LEFT],  // DOWN → LEFT
                    (0, -1) => DIRECTIONS[UP],   // LEFT → UP
                    _ => curDir, // No valid direction
            };
        }
    }
}
