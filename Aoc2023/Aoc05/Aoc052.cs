namespace Aoc2023.Aoc05;

[TestFixture]
public class Aoc052 
{
    private long Run(string[] input) => Fast(input);

    private long Fast(string[] input) // too slow -> took 3872s -> 64 minutes on full data xD 
    {
        var maps = new HashSet<MapEntry>[7];
        int mapIndex = 0;
        HashSet<MapEntry> lastMap = null;
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            if (line.Length == 0) {
                // empty line
                continue;
            }
            if (!char.IsDigit(line[0])){
                lastMap = new HashSet<MapEntry>(50); 
                maps[mapIndex++] = lastMap;
                continue;
            }
            var firstSpacePos = line.IndexOf(' ', StringComparison.Ordinal);
            var secondSpacePos = line.IndexOf(' ', firstSpacePos+1);

            lastMap.Add(new (
                long.Parse(line[..firstSpacePos]), 
                long.Parse(line[firstSpacePos..secondSpacePos]), 
                long.Parse(line[secondSpacePos..])));
        }


        var result = long.MaxValue;
        
        var seedsInput = input[0].Substring(7).Split(' ').Select(x => long.Parse(x)).ToArray();
        for (int i = 0; i < seedsInput.Length; i+=2)
        {
            var seedLength = seedsInput[i+1];            
            var seedBase = seedsInput[i];
            for (int j = 0; j < seedLength; j++)
            {
                var seedResult = seedBase + j;
               
                foreach (var map in maps)
                {                    
                    var matchedMap = map.FirstOrDefault(m => m.Contains(seedResult), MapEntry.Default);
                    seedResult = matchedMap.StartY + seedResult - matchedMap.StartX;
                    //result = Math.Min(seedResult, result);
                }
                result = Math.Min(seedResult, result);
            }
        }

        return result;
    }

    private long First(string[] input) // too slow -> took 3872s -> 64 minutes on full data xD 
    {
        var seedsInput = input[0].Substring(7).Split(' ').Select(x => long.Parse(x)).ToArray();
        var seeds = new List<long>();
        for (int i = 0; i < seedsInput.Length; i+=2)
        {
            for (int j = 0; j < seedsInput[i+1]; j++)
            {
                seeds.Add(seedsInput[i] + j);
            }
        }

        var maps = new List<(long startY, long startX, long length)>[7];
        int mapIndex = 0;
        List<(long startY, long startX, long length)> lastMap = null;
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            if (line.Length == 0) {
                // empty line
                continue;
            }
            if (!char.IsDigit(line[0])){
                lastMap = new List<(long startY, long startX, long length)>(50); 
                maps[mapIndex++] = lastMap;
                continue;
            }
            var firstSpacePos = line.IndexOf(' ', StringComparison.Ordinal);
            var secondSpacePos = line.IndexOf(' ', firstSpacePos+1);

            lastMap.Add(new (
                long.Parse(line[..firstSpacePos]), 
                long.Parse(line[firstSpacePos..secondSpacePos]), 
                long.Parse(line[secondSpacePos..])));
        }


        var result = long.MaxValue;
        foreach (var seed in seeds)
        {
            var seedResult = seed;
            foreach (var map in maps)
            {
                var matchedMap = map.FirstOrDefault(m => seedResult >= m.startX && seedResult < m.startX + m.length);
                if (matchedMap != default) {
                    seedResult = matchedMap.startY + seedResult - matchedMap.startX;
                }
            }
            result = Math.Min(seedResult, result);
        }


        return result;
    }

    [Test]
    public void Sample()
    {
        const string input = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(46));
    }

    [Test]
    [Explicit("Too slow to run everytime")]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc05/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(34039469));
    }
}