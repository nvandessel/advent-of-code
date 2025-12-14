using System;

namespace adventofcode.Core;

public class SolutionResult
{
    public string Part1 { get; set; } = "";
    public string Part2 { get; set; } = "";
    public TimeSpan Part1Time { get; set; }
    public TimeSpan Part2Time { get; set; }
    public Exception Part1Exception { get; set; }
    public Exception Part2Exception { get; set; }
}
