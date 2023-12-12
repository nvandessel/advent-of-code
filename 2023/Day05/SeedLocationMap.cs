using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2023.D05;

public class SeedLocationMap
{
    private readonly List<Map> _maps;
    
    public SeedLocationMap(IEnumerable<string> maps)
    {
        _maps = maps.Select(map =>
        {
            var longs = map.Split('\n')
                .Skip(1)
                .Select(line => line.Split(' ')
                    .Select(long.Parse)
                    .ToArray())
                .ToArray();
            return new Map(longs);
        }).ToList();
    }
    
    public long GetSeedLocation(long seed)
    {
        var mapValue = seed;
        foreach (var map in _maps)
        {
            if (map.TryGetValue(mapValue, out var result))
            {
                mapValue = result;
            }
        }
        return mapValue;
    }

    public long GetSeedLocationRange(long seed, long length)
    {
        var mapValue = seed;
        foreach (var map in _maps)
        {
            if (map.TryGetRange(mapValue, length, out var result))
            {
                mapValue = result;
            }
        }
        return mapValue;
    }
}