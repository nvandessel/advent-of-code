using System;
using System.Diagnostics;
using System.IO;

namespace adventofcode.Core;

public abstract class Solution : ISolution
{
    public abstract int Year { get; }
    public abstract int Day { get; }
    public string InputFileName => "Input.txt";
    public string SampleInputFileName => "SampleInput.txt";

    // Legacy method for backward compatibility
    public virtual void Solve()
    {
        // Default implementation: run Execute() with real input and display
        SolveReal();
    }

    // New method for solutions to implement
    // Override this instead of Solve() for new solutions
    public virtual SolutionResult Execute(string input)
    {
        // Default: call old Solve() method for backward compatibility
        // New solutions should override this method
        return new SolutionResult
        {
            Part1 = "Override Execute() method",
            Part2 = "Override Execute() method"
        };
    }

    public void SolveSample()
    {
        var input = GetInput(sample: true);
        var stopwatch = Stopwatch.StartNew();
        
        SolutionResult result;
        try
        {
            result = Execute(input);
        }
        catch (Exception ex)
        {
            result = new SolutionResult
            {
                Part1Exception = ex,
                Part2Exception = ex
            };
        }
        
        stopwatch.Stop();
        result.Part1Time = stopwatch.Elapsed;
        result.Part2Time = stopwatch.Elapsed;

        // Load expected answers
        var expected = ExpectedAnswers.Load(Year, Day);

        // Display results
        OutputFormatter.PrintPartResult(1, result.Part1, expected.Part1, result.Part1Time, result.Part1Exception);
        OutputFormatter.PrintPartResult(2, result.Part2, expected.Part2, result.Part2Time, result.Part2Exception);
        OutputFormatter.PrintFooter();
    }

    public void SolveReal()
    {
        var input = GetInput(sample: false);
        var result = Execute(input);

        OutputFormatter.PrintRealInputHeader(Year, Day);
        if (!string.IsNullOrWhiteSpace(result.Part1))
        {
            OutputFormatter.PrintRealInputResult("Part 1", result.Part1);
        }
        if (!string.IsNullOrWhiteSpace(result.Part2))
        {
            OutputFormatter.PrintRealInputResult("Part 2", result.Part2);
        }
    }

    protected virtual string GetInput(bool sample = false)
    {
        var result = "";
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            Year.ToString(),
            $"Day{Day:00}",
            sample ? SampleInputFileName : InputFileName);
        using (var stream = File.OpenText(filePath))
        {
            result = stream.ReadToEnd();
        }
        return result.Trim();
    }

    protected virtual void OutputAnswer(string message)
    {
        Console.WriteLine($"Solution {Year}, {Day}: {message}");
    }
}
