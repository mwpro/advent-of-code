namespace Aoc2023.Aoc02;

[TestFixture]
public class Aoc022
{
    private int Run(string[] input) => Fast(input);
    
    private int Fast(string[] input)
    {
        var rgb = new Dictionary<string, int>(StringComparer.Ordinal)
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 }
        };
        return input.Sum(line =>
        {
            rgb["red"] = 0;
            rgb["green"] = 0;
            rgb["blue"] = 0;

            foreach (var draw in line
                         .Substring(line.IndexOf(':', StringComparison.Ordinal) + 1)
                         .Split(new[] { ';', ',' }, StringSplitOptions.TrimEntries))
            {
                var spacePos = draw.IndexOf(' ', StringComparison.Ordinal);
                var amount = int.Parse(draw.Substring(0, spacePos));
                var color = draw.Substring(spacePos + 1);

                if (rgb[color] < amount)
                {
                    rgb[color] = amount;
                }
            }

            return rgb["red"] * rgb["green"] * rgb["blue"];
        });
    }

    private int First(string[] input)
    {
        return input.Sum(line => line.Substring(line.IndexOf(':', StringComparison.Ordinal) + 1)
            .Split(';')
            .SelectMany(game =>
            {
                return game.Split(',', StringSplitOptions.TrimEntries)
                    .Select(draw =>
                    {
                        var splitDraw = draw.Split(' ', StringSplitOptions.TrimEntries);
                        var amount = int.Parse(splitDraw[0]);
                        var color = splitDraw[1];
                        return (color, amount);
                    });
            })
            .GroupBy(x => x.color, (s, tuples) => tuples.Max(x => x.amount))
            .Aggregate(1, (acc, maxForColor) => acc * maxForColor));
    }

    [Test]
    public void Sample()
    {
        const string input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(2286));
    }

    [Test]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc02/input.txt");
        
        var result = Run(input);
        
        Assert.That(result, Is.EqualTo(49710));
    }
}