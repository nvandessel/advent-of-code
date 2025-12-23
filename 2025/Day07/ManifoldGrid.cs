using System;
using System.Linq;

namespace adventofcode.Y2025.D07;

/// <summary>
/// Immutable representation of the tachyon manifold grid.
/// Provides navigation and query methods for grid cells.
/// </summary>
public class ManifoldGrid
{
    private const char Empty = '.';
    private const char Splitter = '^';
    private const char Start = 'S';
    
    private readonly char[,] _cells;
    
    public int Rows { get; }
    public int Cols { get; }
    public int StartColumn { get; }
    
    public ManifoldGrid(string input)
    {
        var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        Rows = lines.Length;
        Cols = lines.Max(l => l.Length);
        _cells = BuildGrid(lines);
        StartColumn = FindStartColumn();
        
        if (StartColumn == -1)
        {
            throw new InvalidOperationException("Start position 'S' not found in grid");
        }
    }
    
    private char[,] BuildGrid(string[] lines)
    {
        var grid = new char[Rows, Cols];
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                grid[row, col] = col < lines[row].Length ? lines[row][col] : Empty;
            }
        }
        return grid;
    }
    
    private int FindStartColumn()
    {
        for (int col = 0; col < Cols; col++)
        {
            if (_cells[0, col] == Start)
            {
                return col;
            }
        }
        return -1;
    }
    
    /// <summary>
    /// Checks if the position contains a splitter (^).
    /// </summary>
    public bool IsSplitter(int row, int col)
    {
        return IsInBounds(row, col) && _cells[row, col] == Splitter;
    }
    
    /// <summary>
    /// Checks if the position is empty (.).
    /// </summary>
    public bool IsEmpty(int row, int col)
    {
        return IsInBounds(row, col) && _cells[row, col] == Empty;
    }
    
    /// <summary>
    /// Checks if the position is within grid bounds.
    /// </summary>
    public bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Cols;
    }
    
    /// <summary>
    /// Gets the character at the specified position.
    /// </summary>
    public char GetCell(int row, int col)
    {
        if (!IsInBounds(row, col))
        {
            throw new ArgumentOutOfRangeException($"Position ({row}, {col}) is out of bounds");
        }
        return _cells[row, col];
    }
}
