using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2025.D07;

public class Manifold
{
    public int TimesSplit;
    
    private const char Empty = '.';
    private const char Splitter = '^';
    private const char Beam = '|';
            
    private char[,] _grid;
    private char[,] _beamGrid;
    private readonly int _rows;
    private readonly int _cols;
    private List<(int row, int col)> _splitters = [];

    public Manifold(string input)
    {
        var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        _rows = lines.GetLength(0);
        _cols = lines.Max(l => l.Length);
        _grid = BuildGrid(lines);
        _beamGrid = new char[_rows, _cols];
        Array.Copy(_grid, _beamGrid, _beamGrid.Length);
    }
    
    private char[,] BuildGrid(string[] lines)
    {
        var grid = new char[_rows, _cols];
        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _cols; col++)
            {
                grid[row, col] = lines[row][col];
            }
        }
        return grid;
    }

    private static bool IsSplitter(char[,] grid, int row, int col) => grid[row, col] == Splitter;
    private static bool IsBeam(char[,] grid, int row, int col) => grid[row, col] == Beam;
    private static bool IsEmpty(char[,] grid, int row, int col) => grid[row, col] == Empty;

    public void ProcessBeams()
    {
        TimesSplit += 0;
        
        var startCol = FindStartColumn();
        _beamGrid[1, startCol] = Beam;

        for (int row = 1; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
        
                if (IsBeam(_beamGrid, row, col))
                {
                    continue;
                }
                
                var isBeamAbove = IsBeam(_beamGrid, row - 1, col);
                if (IsEmpty(_beamGrid, row, col) && isBeamAbove)
                {
                    _beamGrid[row, col] = Beam;
                }
                
                if (IsSplitter(_beamGrid, row, col) && isBeamAbove)
                {
                    SplitBeam(row, col);
                    TimesSplit++;
                    _splitters.Add((row, col));
                }
            }
        }
        PrintGrid();
    }

    public int CountTimelines()
    {
        var timelines = 0;
        var particles = new Stack<(int row, int col)>();
        particles.Push((1, FindStartColumn()));

        while (particles.Count > 0)
        {
            var particle = particles.Pop();
            if (particle.row >= _rows)
            {
                timelines++;
                continue;
            }

            if (IsSplitter(_grid, particle.row, particle.col))
            {
                var left = particle.col - 1;
                if (left >= 0)
                {
                    particles.Push((particle.row + 1, left));
                }
        
                var right = particle.col + 1;
                if (right < _cols)
                {
                    particles.Push((particle.row + 1, right));
                }
            }
            else
            {
                var below = particle.row + 1;
                if (below >= _rows)
                {
                    timelines++;
                }
                else if (below < _rows && (IsEmpty(_grid, below, particle.col) || IsSplitter(_grid, below, particle.col)))
                {
                    particles.Push((below, particle.col));
                }
            }
        }
        
        return timelines;
    }

    private int FindStartColumn()
    {
        for (int i = 0; i < _cols; i++)
        {
            if (_grid[0, i] == 'S')
            {
                return i;
            }
        }
        return -1;
    }

    private void SplitBeam(int row, int col)
    {
        var left = col - 1;
        if (left >= 0)
        {
            _beamGrid[row, left] = Beam;
        }
        
        var right = col + 1;
        if (right < _cols)
        {
            _beamGrid[row, right] = Beam;
        }
    }

    private void PrintGrid()
    {
        for (int row = 0; row < _rows; row++)
        {
            var rowChars = new char[_cols];
            for (int col = 0; col < _cols; col++)
            {
                rowChars[col] = _beamGrid[row, col];
            }

            string rowString = new string(rowChars);
            Console.WriteLine(rowString);
        }
    }
}