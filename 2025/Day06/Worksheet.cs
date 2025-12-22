using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2025.D06
{
    public class Worksheet
    {
        private readonly char[,] _grid;
        private readonly int _rows;
        private readonly int _cols;

        public Worksheet(string input)
        {
            var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            _rows = lines.Length;
            _cols = lines.Max(l => l.Length);
            _grid = BuildGrid(lines);
        }

        public IEnumerable<Problem> ParseLeftToRight()
        {
            var problemBuilders = new Dictionary<int, ProblemBuilder>();
            var lines = ExtractLines();
            foreach (var line in lines)
            {
                var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < tokens.Length; i++)
                {
                    if (!problemBuilders.TryGetValue(i, out var builder))
                    {
                        builder = new ProblemBuilder();
                        problemBuilders[i] = builder;
                    }

                    var token = tokens[i];
                    if (token.Length == 1 && !char.IsDigit(token[0]))
                    {
                        builder.SetOperator(token[0]);
                    }
                    else
                    {
                        builder.AddValue(long.Parse(token));
                    }
                }
            }

            return problemBuilders.Values.Select(builder => builder.Build());
        }

        public IEnumerable<Problem> ParseRightToLeft()
        {
            var problems = new List<Problem>();
            ProblemBuilder currentBuilder = null;

            for (var col = _cols - 1; col >= 0; col--)
            {
                if (IsEmptyColumn(col))
                {
                    if (currentBuilder != null)
                    {
                        problems.Add(currentBuilder.Build());
                        currentBuilder = null;
                    }
                    continue;
                }

                currentBuilder ??= new ProblemBuilder();
                var column = GetColumn(col).ToList();
                var lastChar = column.Last();
                
                if (!char.IsDigit(lastChar) && lastChar != ' ')
                {
                    currentBuilder.SetOperator(lastChar);
                    
                    var numberValue = ExtractNumber(column.Take(_rows - 1));
                    if (numberValue.HasValue)
                    {
                        currentBuilder.AddValue(numberValue.Value);
                    }
                }
                else
                {
                    var numberValue = ExtractNumber(column);
                    if (numberValue.HasValue)
                    {
                        currentBuilder.AddValue(numberValue.Value);
                    }
                }
            }

            if (currentBuilder != null)
            {
                problems.Add(currentBuilder.Build());
            }

            return problems;
        }

        private char[,] BuildGrid(string[] lines)
        {
            var grid = new char[_rows, _cols];
            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    grid[row, col] = col < lines[row].Length ? lines[row][col] : ' ';
                }
            }
            return grid;
        }

        private string[] ExtractLines()
        {
            var lines = new string[_rows];
            for (var row = 0; row < _rows; row++)
            {
                var chars = new char[_cols];
                for (var col = 0; col < _cols; col++)
                {
                    chars[col] = _grid[row, col];
                }
                lines[row] = new string(chars);
            }
            return lines;
        }

        private bool IsEmptyColumn(int col)
        {
            for (var row = 0; row < _rows; row++)
            {
                if (_grid[row, col] != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerable<char> GetColumn(int col)
        {
            for (var row = 0; row < _rows; row++)
            {
                yield return _grid[row, col];
            }
        }

        private static long? ExtractNumber(IEnumerable<char> chars)
        {
            var digits = chars.Where(char.IsDigit).ToArray();
            if (digits.Length == 0)
            {
                return null;
            }
            return long.Parse(new string(digits));
        }
    }
}
