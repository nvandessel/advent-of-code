using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2023.D03;

public class Schematic
{
    private readonly char[,] _schematic;
    
    private readonly string _symbols = "!@#$%^&*()-_+=[]{};:\"<>,?/\\'|~";
    // period is intentionally not here

    private HashSet<(int row, int col)> _symbolLocations = new();
    private Dictionary<(int row, int col), int> _partNumbersAndLocations = new();

    private HashSet<(int row, int col)> _gearLocations = new();
    private List<int> _gearNumbers = new();

    public Schematic(string input)
    {
        var lines = input.Split("\n");
        _schematic = CreateSchematic(lines);
    }
    
    public void FindSymbols()
    {
        _symbolLocations.Clear();
        _gearLocations.Clear();
        for (int row = 0; row < _schematic.GetLength(0); row++)
        {
            for (int col = 0; col < _schematic.GetLength(1); col++)
            {
                var c = _schematic[row, col];
                if (char.IsDigit(c) || c == '.')
                {
                    continue;
                }

                if (_symbols.Contains(c))
                {
                    _symbolLocations.Add((row, col));
                }
                
                if (c == '*')
                {
                    _gearLocations.Add((row, col));
                }
            }
        }
    }

    public IEnumerable<int> ReturnPartNumbers()
    {
        _partNumbersAndLocations.Clear();
        foreach (var location in _symbolLocations)
        {
            FindNumbers(location.row, location.col);
        }
        return _partNumbersAndLocations.Select(pair => pair.Value);
    }
    
    public IEnumerable<int> ReturnGearNumbers()
    {
        _gearNumbers.Clear();
        foreach (var (row, col) in _gearLocations)
        {
            FindGears(row, col);
        }
        return _gearNumbers;
    }
    
    private char[,] CreateSchematic(string[] lines)
    {
        var result = new char[lines.Length, lines[0].Length];
        for (var row = 0; row < lines.Length; row++)
        {
            var line = lines[row];
            for (var col = 0; col < line.Length; col++)
            {
                result[row, col] = line[col];
            }
        }
        return result;
    }
    
    private void FindNumbers(int row, int col)
    {
        for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (var colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0)
                {
                    continue; // Skip the current position
                }

                var newRow = row + rowOffset;
                var newCol = col + colOffset;

                if (IsAdjacentDigit(_schematic, row, col, rowOffset, colOffset))
                {
                    var rowArray = GetRow(newRow);
                    var result = FindContiguousNumber(rowArray, newCol);
                    _partNumbersAndLocations.TryAdd((newRow, result.startIndex), result.number);
                }
            }
        }
    }
    
    private void FindGears(int row, int col)
    {
        var pendingNumbers = new Dictionary<(int row, int col), int>();
        
        for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (var colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0)
                {
                    continue; // Skip the current position
                }

                var newRow = row + rowOffset;
                var newCol = col + colOffset;

                if (IsAdjacentDigit(_schematic, row, col, rowOffset, colOffset))
                {
                    var rowArray = GetRow(newRow);
                    var result = FindContiguousNumber(rowArray, newCol);
                    pendingNumbers.TryAdd((newRow, result.startIndex), result.number);
                }
            }
        }

        if (pendingNumbers.Count != 2) // Gears have exactly 2 numbers
        {
            return;
        }
        var gearNumber = pendingNumbers.Values.ToArray();
        _gearNumbers.Add(gearNumber[0] * gearNumber[1]);
    }
    
    private bool IsAdjacentDigit(char[,] schematic, int row, int col, int rowOffset, int colOffset)
    {
        var numRows = schematic.GetLength(0);
        var numCols = schematic.GetLength(1);
        var newRow = row + rowOffset;
        var newCol = col + colOffset;

        if (newRow < 0 || newRow >= numRows || newCol < 0 || newCol >= numCols)
        {
            return false;
        }
            
        var adjacentValue = schematic[newRow, newCol];
        return char.IsDigit(adjacentValue);
    }
    
    /// <summary>
    /// Traverses the array in both directions (left/right) to find the entire number.
    /// </summary>
    /// <param name="array">The sequence of items to be traversed, checking for numbers.</param>
    /// <param name="index">The index where we want to start traversing the array.</param>
    /// <returns>A tuple containing the startingIndex of the number, and the number itself.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is outside of bounds of array.</exception>
    private (int startIndex, int number) FindContiguousNumber(char[] array, int index)
    {
        if (index < 0 || index >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
        }

        var startIndex = index;
        while (startIndex >= 0 && char.IsDigit(array[startIndex]))
        {
            startIndex--;
        }
        startIndex++;

        var endIndex = index;
        while (endIndex < array.Length && char.IsDigit(array[endIndex]))
        {
            endIndex++;
        }
        endIndex--;

        // Ensure the length is at least 1
        var length = Math.Max(1, endIndex - startIndex + 1);
        var number = int.Parse(new string(array, startIndex, length));
        return (startIndex, number);
    }
    
    private char[] GetRow(int rowIndex)
    {
        var cols = _schematic.GetLength(1);
        var rowArray = new char[cols];

        for (var col = 0; col < cols; col++)
        {
            rowArray[col] = _schematic[rowIndex, col];
        }
        return rowArray;
    }
}