using System;
using System.IO;

namespace adventofcode.Core;

public abstract class Solution : ISolution
{
    public abstract int Year { get; }
    public abstract int Day { get; }
    public string InputFileName => "Input.txt";

    public abstract void Solve();

    protected virtual string GetInput()
    {
        var result = "";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), Year.ToString(), $"Day{Day.ToString("00")}", InputFileName);
        using (var stream = File.OpenText(filePath))
        {
            result = stream.ReadToEnd();
        }
        return result;
    }

    protected virtual void OutputAnswer(string message)
    {
        Console.WriteLine($"Solution {Year}, {Day}: {message}");
    }
}