namespace Aoc2023.Aoc08;

[TestFixture]
public class Aoc081
{
    private int Run(string[] input) => First(input);
    private int First(string[] input)
    {
        var directions = input[0];
        var map = new Dictionary<string, (string l, string r)>(input.Length);
        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i];
            map.Add(line.Substring(0, 3), 
                (line.Substring(7, 3), line.Substring(12, 3)));
        }

        var numberOfSteps = 0;
        var currentNode = map["AAA"];
        while (currentNode != map["ZZZ"])
        {
            currentNode = directions[numberOfSteps++ % directions.Length] == 'L' ? map[currentNode.l] : map[currentNode.r];
        }
        
        return numberOfSteps;
    }

    [Test]
    public void Sample()
    {
        const string input = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Sample2()
    {
        const string input = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(6));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc08/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(17263));
    }
}