using System.Collections.Generic;

namespace adventofcode.Y2025.D07;

public class TimelineCounter(ManifoldGrid grid)
{
    private Dictionary<(int row, int col), long> _memo;

    public long CountTimelines()
    {
        _memo = new Dictionary<(int row, int col), long>();
        return CountTimelinesFrom(1, grid.StartColumn);
    }
    
    private long CountTimelinesFrom(int row, int col)
    {
        if (row >= grid.Rows)
        {
            return 1;
        }

        if (_memo.TryGetValue((row, col), out var cachedCount))
        {
            return cachedCount;
        }
        
        var timelineCount = 0L;
        if (grid.IsSplitter(row, col))
        {
            var left = col - 1;
            if (left >= 0)
            {
                timelineCount += CountTimelinesFrom(row + 1, left);
            }
            
            var right = col + 1;
            if (right < grid.Cols)
            {
                timelineCount += CountTimelinesFrom(row + 1, right);
            }
            
        }
        else
        {
            timelineCount = CountTimelinesFrom(row + 1, col);
        }
        
        _memo[(row, col)] = timelineCount;
        return timelineCount;
    }
}
