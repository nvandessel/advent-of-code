using System;
using System.Collections.Generic;
using adventofcode.Core;

namespace adventofcode.Y2024.D06
{
    [SolutionAttribute("Guard Gallivant")]
    public class GuardGallivant : Solution
    {
        public override int Year => 2024;
        public override int Day => 06;

        public override void Solve()
        {
            var input = GetInput();
            var map = new GuardMap(input);

            var partOne = GetGuardPositions(map);
            OutputAnswer("Part One = " + partOne);

            var guardPath = TraceGuardPath(map);
            var candidates = GetCandidatePositions(guardPath, map);
            var partTwo = CountValidObstructionPositions(candidates, map);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int GetGuardPositions(GuardMap map)
        {
            var visited = new HashSet<(int row, int col)>();
            var curDir = map.GuardStartDirection;
            var (row, col) = map.GuardStartPosition;

            while (map.IsWithinMap(row, col))
            {
                visited.Add((row, col));

                var nextRow = row + curDir.rowDir;
                var nextCol = col + curDir.colDir;

                if (!map.IsWithinMap(nextRow, nextCol))
                {
                    break;
                }

                if (map.IsObstacle(nextRow, nextCol))
                {
                    curDir = map.NextDirection(curDir);
                }
                else
                {
                    row = nextRow;
                    col = nextCol;
                }
            }

            return visited.Count;
        }

        private int CountValidObstructionPositions(HashSet<(int row, int col)> candidates, GuardMap map)
        {
            int count = 0;

            foreach (var position in candidates)
            {
                if (CausesLoop(map, position))
                {
                    count++;
                }
            }

            return count;
        }

        private HashSet<(int row, int col, (int rowDir, int colDir))> TraceGuardPath(GuardMap map)
        {
            var visitedStates = new HashSet<(int row, int col, (int rowDir, int colDir))>();
            var curDir = map.GuardStartDirection;
            var (row, col) = map.GuardStartPosition;

            while (map.IsWithinMap(row, col))
            {
                var state = (row, col, curDir);
                if (visitedStates.Contains(state))
                {
                    // Loop detected in the initial route
                    break;
                }

                visitedStates.Add(state);

                // Calculate the next position
                var nextRow = row + curDir.rowDir;
                var nextCol = col + curDir.colDir;

                if (!map.IsWithinMap(nextRow, nextCol))
                {
                    break;
                }

                if (map.IsObstacle(nextRow, nextCol))
                {
                    curDir = map.NextDirection(curDir);
                }
                else
                {
                    // Move forward
                    row = nextRow;
                    col = nextCol;
                }
            }

            return visitedStates;
        }

        private HashSet<(int row, int col)> GetCandidatePositions(HashSet<(int row, int col, (int rowDir, int colDir))> guardPath, GuardMap map)
        {
            var candidates = new HashSet<(int row, int col)>();

            foreach (var (row, col, (rowDir, colDir)) in guardPath)
            {
                (int row, int col) neighbor = (row + rowDir, col + colDir);

                if (map.IsWithinMap(neighbor.row, neighbor.col) &&
                        !map.IsObstacle(neighbor.row, neighbor.col) &&
                        neighbor != map.GuardStartPosition) // Cannot obstruct the guard's start position
                {
                    candidates.Add(neighbor);
                }
            }

            return candidates;
        }

        private bool CausesLoop(GuardMap map, (int row, int col) obstruction)
        {
            map.AddTemporaryObstacle(obstruction);

            var visitedStates = new HashSet<(int row, int col, (int rowDir, int colDir))>();
            var curDir = map.GuardStartDirection;
            var (row, col) = map.GuardStartPosition;

            while (true)
            {
                var state = (row, col, curDir);

                // Detect loop
                if (visitedStates.Contains(state))
                {
                    map.RemoveTemporaryObstacle(obstruction); // Clean up
                    return true;
                }

                visitedStates.Add(state);

                var nextRow = row + curDir.rowDir;
                var nextCol = col + curDir.colDir;

                // If the guard moves off the map, terminate
                if (!map.IsWithinMap(nextRow, nextCol))
                {
                    map.RemoveTemporaryObstacle(obstruction); // Clean up
                    return false;
                }

                // Redirect on obstacle
                if (map.IsObstacle(nextRow, nextCol))
                {
                    curDir = map.NextDirection(curDir);
                }
                else
                {
                    // Move forward
                    row = nextRow;
                    col = nextCol;
                }
            }
        }
    }
}
