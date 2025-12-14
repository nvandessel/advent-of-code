using System;

namespace adventofcode.Core;

public static class OutputFormatter
{
    // ANSI color codes
    private const string Green = "\x1b[32m";
    private const string Red = "\x1b[31m";
    private const string Yellow = "\x1b[33m";
    private const string Gray = "\x1b[90m";
    private const string Reset = "\x1b[0m";
    private const string Bold = "\x1b[1m";

    public static void PrintWatchHeader(int year, int day)
    {
        Console.Clear();
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine($"🎄 Advent of Code {year} Day {day}");
        Console.WriteLine("   Watching for changes...");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine();
    }

    public static void PrintPartResult(int partNum, string actual, string expected, TimeSpan time, Exception exception = null)
    {
        if (exception != null)
        {
            Console.WriteLine($"{Red}✗ Part {partNum}: {exception.GetType().Name}{Reset}");
            var lines = exception.ToString().Split('\n');
            if (lines.Length > 1)
            {
                Console.WriteLine($"{Gray}   {lines[0].Trim()}{Reset}");
            }
            return;
        }

        if (string.IsNullOrEmpty(actual))
        {
            Console.WriteLine($"{Gray}⊘ Part {partNum}: (not implemented){Reset}");
            return;
        }

        if (string.IsNullOrWhiteSpace(expected))
        {
            // No expected answer to validate against
            Console.WriteLine($"{Yellow}⊘ Part {partNum}: {actual} (no expected answer) {Gray}({time.TotalMilliseconds:F0}ms){Reset}");
            return;
        }

        if (actual.Trim() == expected.Trim())
        {
            // Success!
            Console.WriteLine($"{Green}✓ Part {partNum}: {actual} ✓ {Gray}({time.TotalMilliseconds:F0}ms){Reset}");
        }
        else
        {
            // Failure
            Console.WriteLine($"{Red}✗ Part {partNum}: {actual} (expected: {expected}) {Gray}({time.TotalMilliseconds:F0}ms){Reset}");
        }
    }

    public static void PrintBuildError(string errors)
    {
        Console.WriteLine($"{Red}{Bold}✗ Build Failed:{Reset}");
        Console.WriteLine(errors);
        Console.WriteLine();
        Console.WriteLine($"{Gray}Fix the errors and save to retry...{Reset}");
    }

    public static void PrintFooter()
    {
        Console.WriteLine();
        Console.WriteLine($"{Gray}Last run: {DateTime.Now:HH:mm:ss}{Reset}");
    }

    public static void PrintNoExpectedFile(int year, int day)
    {
        Console.WriteLine($"{Yellow}⚠ Expected.json not found - running without validation{Reset}");
        Console.WriteLine($"{Gray}   Create {year}/Day{day:00}/Expected.json to enable answer validation{Reset}");
        Console.WriteLine();
    }

    public static void PrintRealInputHeader(int year, int day)
    {
        Console.WriteLine($"{Bold}Solution {year}, Day {day}{Reset}");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    }

    public static void PrintRealInputResult(string label, string value)
    {
        Console.WriteLine($"{Bold}{label}:{Reset} {value}");
    }
}
