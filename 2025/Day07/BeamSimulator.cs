using System;

namespace adventofcode.Y2025.D07;

public class BeamSimulator(ManifoldGrid grid, bool debugMode = false)
{
    private const char Beam = '|';
    private const char Empty = '.';
    private const char Splitter = '^';

    private char[,] _beamGrid;


    public int SimulateBeams()
    {
        var splitCount = 0;
        InitializeBeamGrid();
        
        _beamGrid[1, grid.StartColumn] = Beam;
        for (var row = 1; row < grid.Rows; row++)
        {
            for (var col = 0; col < grid.Cols; col++)
            {
                if (IsBeam(row, col))
                {
                    continue;
                }
                
                var isBeamAbove = IsBeam(row - 1, col);
                if (_beamGrid[row, col] == Empty && isBeamAbove)
                {
                    _beamGrid[row, col] = Beam;
                }
                
                if (_beamGrid[row, col] == Splitter && isBeamAbove)
                {
                    SplitBeam(row, col);
                    splitCount++;
                }
            }
        }
        
        if (debugMode)
        {
            PrintBeamGrid();
        }
        
        return splitCount;
    }
    
    private void InitializeBeamGrid()
    {
        _beamGrid = new char[grid.Rows, grid.Cols];
        for (var row = 0; row < grid.Rows; row++)
        {
            for (var col = 0; col < grid.Cols; col++)
            {
                _beamGrid[row, col] = grid.GetCell(row, col);
            }
        }
    }
    
    private void SplitBeam(int row, int col)
    {
        var left = col - 1;
        if (left >= 0)
        {
            _beamGrid[row, left] = Beam;
        }
        
        var right = col + 1;
        if (right < grid.Cols)
        {
            _beamGrid[row, right] = Beam;
        }
    }
    
    private bool IsBeam(int row, int col) => _beamGrid[row, col] == Beam;

    private void PrintBeamGrid()
    {
        Console.WriteLine("\n=== Beam Grid ===");
        for (var row = 0; row < grid.Rows; row++)
        {
            var rowChars = new char[grid.Cols];
            for (var col = 0; col < grid.Cols; col++)
            {
                rowChars[col] = _beamGrid[row, col];
            }
            Console.WriteLine(new string(rowChars));
        }
        Console.WriteLine("=================\n");
    }
}
