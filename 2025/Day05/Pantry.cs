using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2025.D05;

public class Pantry
{
    public List<long> Ingredients { get; }
    public List<(long, long)> FreshIngredientRanges { get; }
    public int FreshIngredientsFound { get; private set; }

    public Pantry(string[]  lines)
    {
        var freshIngredientsFound = false;
        Ingredients = [];
        FreshIngredientRanges = [];
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                freshIngredientsFound = true;
                continue;
            }
            
            if (!freshIngredientsFound)
            {
                var parts = line.Split('-');
                FreshIngredientRanges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
            }
            else
            {
                var ingredient = long.Parse(line);
                Ingredients.Add(ingredient);
                if (IsFresh(ingredient))
                {
                    FreshIngredientsFound++;
                }
            }
        }
    }

    public bool IsFresh(long ingredient)
    {
        foreach (var freshIngredient in FreshIngredientRanges)
        {
            if (freshIngredient.Item1 <= ingredient && freshIngredient.Item2 >= ingredient)
            {
                return true;
            }
        }

        return false;
    }

    public List<(long, long)> OverlappingRanges()
    {
        var sortedRanges = FreshIngredientRanges.OrderBy(r => r.Item1).ToList();
        if (sortedRanges.Count == 0)
        {
            return [];
        }

        var mergedRanges = new List<(long, long)> { sortedRanges[0] };
        for (int i = 1; i < sortedRanges.Count; i++)
        {
            var lastMerged = mergedRanges[^1];
            var current = sortedRanges[i];
            if (current.Item1 <= lastMerged.Item2)
            {
                var newEnd = Math.Max(lastMerged.Item2, current.Item2);
                mergedRanges[^1] = (lastMerged.Item1, newEnd);
            }
            else
            {
                mergedRanges.Add(current);
            }
        }
    
        return mergedRanges;
    }
}