namespace Aoc2023.Aoc06;

[TestFixture]
public class Aoc061
{
    private int Run(string[] input) => First(input);
    private int First(string[] input)
    {
        var times = input[0].Substring(6).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        var distances = input[1].Substring(10).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        var results = new List<int>(times.Length);

        for (int i = 0; i < times.Length; i++)
        {
            // Time * x - x^2 > distance => -x^2 + Time * x - distance > 0
            // d = b^2 - 4ac = b^2 = Time ^ 2 - 4distance
            // x1 = (-b + sqrt(d)) / 2a = (-Time + sqrt(d)) / -2 => (Time - sqrt(d))/2
            // x2 = (-b - sqrt(d)) / 2a =>  => (Time + sqrt(d))/2
            var time = times[i];
            var distance = distances[i];
            var deltaSqrt = Math.Sqrt(time * time - 4 * distance);
            var x1 = (time - deltaSqrt) / 2;
            var x2 = (time + deltaSqrt) / 2;
            var numberOfWinningCombinations = (int)(Math.Ceiling(x2) - Math.Ceiling(x1));
            if (x1%1==0) {
                numberOfWinningCombinations--;
            }
            results.Add(numberOfWinningCombinations);
        }

        return results.Aggregate(1, ((seed, value) => seed * value));
    }

    [Test]
    public void Sample()
    {
        const string input = @"Time:      7  15   30
Distance:  9  40  200";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(288));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc06/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(503424));
    }
}