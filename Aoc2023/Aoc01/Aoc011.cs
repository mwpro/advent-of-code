namespace Aoc2023.Aoc01;

[TestFixture]
public class Aoc011
{
    private int Run(string[] input)
    {
        const int conversionMagicNumber = 10*(int)'0' + (int)'0';

        // fancy
        return input.Aggregate(0, (sum, line) =>
        {
            var first = line.First(char.IsDigit);
            var last = line.Last(char.IsDigit);
            return sum + (first * 10 + last - conversionMagicNumber);
        });
        // simple
        // foreach (var line in input)
        // {
        //     var numbers = line.Where(char.IsDigit);
        //     var calibrationValue = (numbers.First() - zeroVal) * 10 + numbers.Last() - zeroVal;
        //     sum += calibrationValue;
        // }
    }

    [Test]
    public void Sample()
    {
        const string input = @"""1abc2
            pqr3stu8vwx
            a1b2c3d4e5f
            treb7uchet";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(142));
    }

    [Test]
    public void Full()
    {
        var input = File.ReadAllLines("Aoc01/input.txt");
        
        var result = Run(input);
        
        Assert.That(result, Is.EqualTo(54597));
    }
}