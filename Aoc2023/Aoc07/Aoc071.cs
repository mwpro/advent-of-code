namespace Aoc2023.Aoc07;

[TestFixture]
public class Aoc071
{
    private long Run(string[] input) => First(input);
    private long First(string[] input)
    {
        var cardRanks = new Dictionary<char, int>()
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            { 'J', 11 },
            { 'T', 10 },
            { '9', 9 },
            { '8', 8 },
            { '7', 7 },
            { '6', 6 },
            { '5', 5 },
            { '4', 4 },
            { '3', 3 },
            { '2', 2 },
            
        };

        var mappedSets = new List<(long setValue, int bid)>(input.Length);
        foreach (var line in input)
        {
            var bid = int.Parse(line.Substring(6));
            var setValue = 0L;

            var sortedCards = line.Substring(0, 5).GroupBy(x => x).OrderByDescending(x => x.Count()).ToArray();
            
            
            // Five of a kind, where all five cards have the same label: AAAAA
            if (sortedCards.Length == 1)
            {
                setValue = 70000000000;
            } 
            else if (sortedCards.Length == 2)
            {
                // Four of a kind, where four cards have the same label and one card has a different label: AA8AA
                if (sortedCards[0].Count() == 4)
                {
                    setValue = 60000000000;    
                }
                // Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
                else
                {
                    setValue = 50000000000;
                }
            }
            else if (sortedCards.Length == 3)
            {
                // Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
                if (sortedCards[0].Count() == 3)
                {
                    setValue = 40000000000;    
                }
                // Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432 => 33224
                else
                {
                    setValue = 30000000000;
                }
            }
            // One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4 => AA432
            else if (sortedCards.Length == 4)
            {
                setValue = 20000000000;
            }
            // High card, where all cards' labels are distinct: 23456
            // do nothing

            // values of cards by position
            var multiplier = 100000000;
            for (var j = 0; j < 5; j++)
            {
                setValue += cardRanks[line[j]] * multiplier;
                multiplier /= 100;
            }
            
            mappedSets.Add((setValue, bid));
        }

        return mappedSets.OrderByDescending(x => x.setValue)
                .Select((set, index) => (input.Length - index) * set.bid)
                .Aggregate(0, (result, rankedSetValue) => result + rankedSetValue);
    }

    [Test]
    public void Sample()
    {
        const string input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(6440));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc07/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(249483956));
    }
}