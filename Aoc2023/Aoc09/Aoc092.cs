namespace Aoc2023.Aoc09;

[TestFixture]
public class Aoc092
{
    private int Run(string[] input) => First(input);
    private int First(string[] input)
    {
        var result = 0;
        foreach (var line in input)
        {
            var values = line.Split(' ').Select(int.Parse).ToList();
            var differences = new List<List<int>>();
            differences.Add(values);
            while (values.Any(x => x != 0))
            {
                var newValues = new List<int>();
                for (var i = 0; i < values.Count - 1; i++)
                {
                    newValues.Add(values[i + 1] - values[i]);
                }
                differences.Add(newValues);
                values = newValues;
            }
            
            for (var i = differences.Count() - 2; i >= 0; i--)
            {
                differences[i].Insert(0, differences[i][0] - differences[i+1][0]);
            }

            result += differences[0].First();
        }
        
        return result;
    }

    [Test]
    public void Sample()
    {
        const string input = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc09/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(1131));
    }
}