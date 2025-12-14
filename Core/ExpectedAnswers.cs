using System;
using System.IO;
using System.Text.Json;

namespace adventofcode.Core;

public class ExpectedAnswers
{
    public string Part1 { get; set; } = "";
    public string Part2 { get; set; } = "";

    public static ExpectedAnswers Load(int year, int day)
    {
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            year.ToString(),
            $"Day{day:00}",
            "Expected.json");

        if (!File.Exists(filePath))
        {
            return new ExpectedAnswers();
        }

        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ExpectedAnswers>(json, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            }) ?? new ExpectedAnswers();
        }
        catch (Exception)
        {
            // If JSON parsing fails, return empty
            return new ExpectedAnswers();
        }
    }

    public bool HasPart1Expected => !string.IsNullOrWhiteSpace(Part1);
    public bool HasPart2Expected => !string.IsNullOrWhiteSpace(Part2);
}
