using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2023.D05;

public class Map
{
    private class MapItem
    {
        public long Dest { get; set; }
        public long Source { get; set; }
        public long Length { get; set; }
    }
    
    private List<MapItem> Maps { get; }
    
    public Map(IEnumerable<long[]> maps)
    {
        Maps = new List<MapItem>();
        foreach (var sequence in maps)
        {
            Add(sequence[0], sequence[1], sequence[2]);
        }
    }

    public void Add(long dest, long source, long length)
    {
        Maps.Add(new MapItem
        {
            Dest = dest,
            Source = source,
            Length = length
        });
    }
    
    public bool TryGetValue(long value, out long result)
    {
        foreach (var map in Maps.Where(map => value >= map.Source && value <= map.Source + map.Length))
        {
            result = map.Dest + (value - map.Source);
            return true;
        }
        result = 0;
        return false;
    }
    
    public bool TryGetRange(long value, long length, out long result)
    {
        foreach (var map in Maps)
        {
            if (value >= map.Source && value <= map.Source + map.Length)
            {
                result = map.Dest + (value - map.Source);
                return true;
            }
            if (value + length >= map.Source && value + length <= map.Source + map.Length)
            {
                result = map.Dest + (value - map.Source);
                return true;
            }
            
        }
        result = 0;
        return false;
    }
}