namespace Aoc2023.Aoc08;

[TestFixture]
public class Aoc082
{
    private long Run(string[] input) => SawHintToUseLcmOnReddit_SorryForCheating(input);

    private long SawHintToUseLcmOnReddit_SorryForCheating(string[] input)
    {
        var directions = input[0];
        var map = new Dictionary<string, (string l, string r)>(input.Length);
        var startingNodes = new List<(string l, string r)>();
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            var address = line.Substring(0, 3);
            var nodeValue = (line.Substring(7, 3), line.Substring(12, 3));
            map.Add(address, nodeValue);
            if (address[2] == 'A')
            {
                startingNodes.Add(nodeValue);
            }
        }

        var steps = new List<long>(startingNodes.Count);
        foreach (var startingNode in startingNodes)
        {
            var numberOfSteps = 0;
            var node = startingNode;
            var nextNodeAddress = "AAA";
            
            while (nextNodeAddress[2] != 'Z')
            {
                nextNodeAddress = directions[numberOfSteps++ % directions.Length] == 'L' ? node.l : node.r;
                node = map[nextNodeAddress];
            }
            steps.Add(numberOfSteps);
        }

        return MathHelpers.LeastCommonMultiple(steps);
    }

    private int SeemsCorrectButTakesForever(string[] input)
    {
        var directions = input[0];
        var map = new Dictionary<string, (string l, string r)>(input.Length);
        var currentNodes = new List<(string l, string r)>();
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            var address = line.Substring(0, 3);
            var nodeValue = (line.Substring(7, 3), line.Substring(12, 3));
            map.Add(address, nodeValue);
            if (address[2] == 'A')
            {
                currentNodes.Add(nodeValue);
            }
        }

        var numberOfSteps = 0;
        var won = false;

        while (!won)
        {
            won = true;
            var currentDirectionIsLeft = directions[numberOfSteps++ % directions.Length] == 'L';
            for (var i = 0; i < currentNodes.Count; i++)
            {
                var nextNode = currentDirectionIsLeft ? currentNodes[i].l : currentNodes[i].r;
                currentNodes[i] = map[nextNode];
                won = won && nextNode[2] == 'Z';
            }
        }

        return numberOfSteps;
    }

    [Test]
    public void Sample()
    {
        const string input = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

        var result = Run(input.Split(Environment.NewLine));

        Assert.That(result, Is.EqualTo(6));
    }

    [Test]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc08/input.txt");

        var result = Run(input);

        Assert.That(result, Is.EqualTo(14631604759649));
    }
    
    // https://stackoverflow.com/a/74765134
    public static class MathHelpers
    {
        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static long LeastCommonMultiple(long a, long b)
            => a / GreatestCommonDivisor(a, b) * b;
    
        public static long LeastCommonMultiple(IEnumerable<long> values)
            => values.Aggregate(LeastCommonMultiple);
    }
}