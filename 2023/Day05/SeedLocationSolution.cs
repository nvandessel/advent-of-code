using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using adventofcode.Core;

namespace adventofcode.Y2023.D05;

[SolutionAttribute("Seed Location Solution")]
public class SeedLocationSolution : Solution
{
    public override int Year => 2023;
    public override int Day => 05;

    public override void Solve()
    {
        var input = GetInput().Split("\n\n");

        var map = new SeedLocationMap(input.Skip(1).ToArray());

        var seeds = input[0]
            .Split(':')[1]
            .Trim()
            .Split(' ')
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(long.Parse)
            .ToArray();
        
        var partOne = seeds.Select(seed => map.GetSeedLocation(seed)).Min();
        OutputAnswer($"Part One: {partOne}");

        var partTwo = PartTwoRange(seeds, map);
        OutputAnswer($"Part Two: {partTwo}");
    }

    private static long PartTwo(long[] seeds, SeedLocationMap map)
    {
        var result = long.MaxValue;
        for (var i = 0; i < seeds.Length; i += 2)
        {
            var seedRange = seeds[i + 1];
            for (var j = 0; j < seedRange; j++)
            {
                var location = map.GetSeedLocation(seeds[i] + j);
                if (location < result)
                {
                    result = location;
                }
            }
        }
        return result;
    }
    
    private static long PartTwoParallel(long[] seeds, SeedLocationMap map, int chunkSize = 100000)
    {
        var result = long.MaxValue;
        var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount/2 };
        Parallel.For(0, seeds.Length / 2, options, i =>
        {
            var seedRange = seeds[i * 2 + 1];
            var chunkCount = (int)Math.Ceiling((double)seedRange / chunkSize);

            Parallel.For(0, chunkCount, options, chunkIndex =>
            {
                var chunkStart = chunkIndex * chunkSize;
                var chunkEnd = Math.Min(chunkStart + chunkSize, seedRange);
                
                for (var j = chunkStart;  j < chunkEnd; j++)
                {
                    var location = map.GetSeedLocation(seeds[i * 2] + j);
                    Interlocked.Exchange(ref result, Math.Min(Interlocked.Read(ref result), location));
                }
            });
        });
        return result;
    }
    
    private static long PartTwoRange(long[] seeds, SeedLocationMap map, int chunkSize = 100000)
    {
        var result = long.MaxValue;
        var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount/2 };
        Parallel.For(0, seeds.Length / 2, options, i =>
        {
            var location = map.GetSeedLocationRange(seeds[i * 2], seeds[i * 2 + 1]);
            Interlocked.Exchange(ref result, Math.Min(Interlocked.Read(ref result), location));
        });
        return result;
    }
}