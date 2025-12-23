namespace adventofcode.Y2025.D07;

public class Manifold
{
    private readonly BeamSimulator _beamSimulator;
    private readonly TimelineCounter _timelineCounter;
    
    public Manifold(string input)
    {
        var grid = new ManifoldGrid(input);
        _beamSimulator = new BeamSimulator(grid);
        _timelineCounter = new TimelineCounter(grid);
    }
    
    public int CountSplits()
    {
        return _beamSimulator.SimulateBeams();
    }
    
    public long CountTimelines()
    {
        return _timelineCounter.CountTimelines();
    }
}
