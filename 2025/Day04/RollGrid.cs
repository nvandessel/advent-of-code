using System.Collections.Generic;

namespace adventofcode.Y2025.D04;

public class RollGrid
{
    private const char Roll = '@';
    
    public char[,] Grid { get; }
    public List<(int, int)> RollIndexes { get; }
    
    public RollGrid(string[] input)
    {
        Grid = new char[input.Length, input[0].Length];
        RollIndexes = [];
        for (var i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                Grid[i, j] = input[i][j];
                if (Grid[i, j] == Roll)
                {
                    RollIndexes.Add((i, j));
                }
            }
        }
    }

    public bool HasAdjacent(int centerRow, int centerCol)
    {
        if (!IsInBounds(centerRow, centerCol) || Grid[centerRow, centerCol] != Roll)
        {
            return false; 
        }
    
        var adjacentRolls = 0;
        for (int i = centerRow - 1; i <= centerRow + 1; i++)
        {
            for (int j = centerCol - 1; j <= centerCol + 1; j++)
            {
                if (i == centerRow && j == centerCol)
                {
                    continue;
                }
            
                if (IsInBounds(i, j))
                {
                    if (Grid[i, j] == Roll)
                    {
                        adjacentRolls++;
                    }
                }
            }
        }
    
        return adjacentRolls < 4;
    }
        
    public void RemoveRoll(int row, int col)
    {
        RollIndexes.Remove((row, col));
        Grid[row, col] = '.';
    }
        
    private bool IsInBounds(int row, int col)
    {
        if (row < 0 || row >= Grid.GetLength(0))
        {
            return false;
        }

        if (col < 0 || col >= Grid.GetLength(1))
        {
            return false;
        }
            
        return true;
    }
}
