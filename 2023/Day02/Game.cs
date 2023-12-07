using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2023.D02;

public class Game
{
    public int Number;
    public List<Dictionary<CubeColor, int>> Sets;
    public Dictionary<CubeColor, int> MinimumRequiredCubes;
    
    public Game(string input)
    {
        var data = input.Split(':');
        if (data.Length !=  2)
        {
            throw new ArgumentException();
        }

        MinimumRequiredCubes = new Dictionary<CubeColor, int>();
        Number = ExtractGameNumber(data[0]);
        Sets = ExtractSets(data[1]);
    }
    
    private int ExtractGameNumber(string input)
    {
        var result = input.Split(' ');
        if (result.Length !=  2)
        {
            return -1;
        }
        return int.Parse(result[1]);
    }

    private List<Dictionary<CubeColor, int>> ExtractSets(string input)
    {
        var result = new List<Dictionary<CubeColor, int>>();
        var sets = input
            .Split(';')
            .Select(set => set.Trim())
            .ToArray();
        foreach (var set in sets)
        {
            result.Add(ExtractCubes(set));
        }
        return result;
    }

    private Dictionary<CubeColor, int> ExtractCubes(string set)
    {
        var result = new Dictionary<CubeColor, int>();
        var cubes = set
            .Split(',')
            .Select(cube => cube.Trim())
            .ToArray();

        foreach (var cube in cubes)
        {
            var valuePair = cube.Split(' ');
            var quantity = int.Parse(valuePair[0]);
            var color = Enum.Parse<CubeColor>(valuePair[1], true);
            if (result.TryAdd(color, quantity) == false)
            {
                result[color] += quantity;
            }
            if (MinimumRequiredCubes.TryAdd(color, quantity) == false)
            {
                if (MinimumRequiredCubes[color] < quantity)
                {
                    MinimumRequiredCubes[color] = quantity;
                }
            }
        }

        return result;
    }
}

public enum CubeColor
{
    Red,
    Green,
    Blue
}