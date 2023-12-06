namespace Aoc2023.Aoc05;

public record struct MapEntry(long StartY, long StartX, long Length)
{
    public bool Contains(long value) {
        return value >= StartX && value < StartX + Length;
    }

    public static readonly MapEntry Default = new MapEntry(-1, -1, 0);
};

[TestFixture]
public class Aoc051
{
    private long Run(string[] input) => Fast(input);

    private long Fast(string[] input)
    {
        var maps = new List<MapEntry>[7];
        int mapIndex = 0;
        List<MapEntry> lastMap = null;
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            if (line.Length == 0) {
                // empty line
                continue;
            }
            if (!char.IsDigit(line[0])){
                lastMap = new List<MapEntry>(); 
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


        var seeds = input[0].Substring(7).Split(' ').Select(x => long.Parse(x));
        var result = long.MaxValue;
        foreach (var seed in seeds)
        {
            var seedResult = seed;
            foreach (var map in maps)
            {
                var matchedMap = map.FirstOrDefault(m => m.Contains(seedResult), MapEntry.Default);
                seedResult = matchedMap.StartY + seedResult - matchedMap.StartX;
            }
            result = Math.Min(seedResult, result);
        }


        return result;
    }

    private long First(string[] input)
    {
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


        var seeds = input[0].Substring(7).Split(' ').Select(x => long.Parse(x));
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
        
        Assert.That(result, Is.EqualTo(35));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc05/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(26273516));
    }
}