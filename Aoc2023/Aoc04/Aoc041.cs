namespace Aoc2023.Aoc04;

[TestFixture]
public class Aoc041
{
    private int Run(string[] input) => First(input);
    
    private int Fast(string[] input)
    {
        return 0;
    }

    private int First(string[] input)
    {
        var scoreTable = new int[] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };
        
        var result = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var cards = line
                .Substring(line.IndexOf(':', StringComparison.Ordinal) + 1)
                .Split('|');
                
            var winningCards = cards[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var ownedCards = cards[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<string> enumerable = ownedCards.Intersect(winningCards);
            result += scoreTable[enumerable.Count()];
        }
        return result;
    }

    [Test]
    public void Sample()
    {
        const string input = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(13));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc04/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(32001));
    }
}