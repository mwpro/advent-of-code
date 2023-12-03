namespace Aoc2023.Aoc02;

[TestFixture]
public class Aoc021
{
    private int Run(string[] input) => Fast(input);
    
    private int Fast(string[] input)
    {
        var limits = new Dictionary<string, int>(StringComparer.Ordinal)
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };
        
        var result = 0;
        for (var index = 0; index < input.Length; index++)
        {
            var isPossible = true;
            var line = input[index];

            foreach (var draw in line
                         .Substring(line.IndexOf(':', StringComparison.Ordinal) + 1)
                         .Split(new[] { ';', ',' }, StringSplitOptions.TrimEntries))
            {
                var spacePos = draw.IndexOf(' ', StringComparison.Ordinal);
                var amount = int.Parse(draw.Substring(0, spacePos));
                var color = draw.Substring(spacePos + 1);

                isPossible = amount <= limits[color];
                if (!isPossible) break;
            }

            if (isPossible)
            {
                result += index + 1;
            }
        }

        return result;
    }

    private int First(string[] input)
    {
        var limits = new Dictionary<string, int>()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };
        
        var result = 0;
        foreach (var line in input)
        {
            var isPossible = true;
            var idAndGames = line.Split(':');
            var id = int.Parse(idAndGames[0].Trim().Substring(4));
            var games = idAndGames[1].Split(';');
            foreach (var game in games)
            {
                var draws = game.Split(',');
                foreach (var draw in draws)
                {
                    var splitDraw = draw.Trim().Split(' ');
                    var amount = int.Parse(splitDraw[0]);
                    var color = splitDraw[1];
                    isPossible = isPossible && amount <= limits[color];
                }
            }
            if (isPossible)
            {
                result += id;
            }
        }

        return result;
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
        
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc02/input.txt");
        
        var result = Run(input);
        
        Assert.That(result, Is.EqualTo(2683));
    }
}