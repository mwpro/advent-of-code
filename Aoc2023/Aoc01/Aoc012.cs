namespace Aoc2023.Aoc01;

[TestFixture]
public class Aoc012
{
    private int Run(string[] input) => Fast(input);
    
    private int Fast(string[] input)
    {
        var replacements = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
    
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
        };

        return input.Aggregate(0, (sum, line) =>
        {
            var first = -1;
            var firstPos = int.MaxValue;
            var last = -1;
            var lastPos = int.MinValue;
            foreach (var replacement in replacements)
            {
                var firstTmp = line.IndexOf(replacement.Key, StringComparison.Ordinal);
                var lastTmp = line.LastIndexOf(replacement.Key, StringComparison.Ordinal);
                if (firstTmp != -1 && firstTmp < firstPos)
                {
                    first = replacement.Value;
                    firstPos = firstTmp;
                }
            
                if (lastTmp != -1 && lastTmp > lastPos)
                {
                    last = replacement.Value;
                    lastPos = lastTmp;
                }
            }

            return sum + (first * 10 + last);
        });
    }
    
    private int Readable(string[] input)
    {
        var replacements = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
    
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
        };
        
        return input.Aggregate(0, (sum, line) =>
        {
            var candidates = replacements.Select(rep =>
            {
                var first = line.IndexOf(rep.Key, StringComparison.Ordinal);
                var last = line.LastIndexOf(rep.Key, StringComparison.Ordinal);
                return (Number: rep.Value, First: first != -1 ? first : int.MaxValue, Last: last != -1 ? last : int.MinValue);
            }).ToArray();

            var first = candidates.MinBy(x => x.First).Number;
            var last = candidates.MaxBy(x => x.Last).Number;
            return sum + (first * 10 + last);
        });
    }

    [Test]
    public void Sample()
    {
        const string input = @"""two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(281));
    }

    [Test]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc01/input.txt");
        
        var result = Run(input);
        
        Assert.That(result, Is.EqualTo(54504));
    }
}