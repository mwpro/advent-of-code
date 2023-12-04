namespace Aoc2023.Aoc03;

[TestFixture]
public class Aoc031
{
    private int Run(string[] input) => First(input);
    
    private int Fast(string[] input)
    {
        return 0;
    }

    private int First(string[] input)
    {
        const int conversionMagicNumber = (int)'0';
        var symbols = new HashSet<char>() { '*', '#', '$', '+', '&', '/', '@', '-', '%', '=' };
        
        var result = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var number = 0;
            for (var j = 0; j <= line.Length; j++)
            {
                if (j != line.Length && char.IsNumber(line[j]))
                {
                    number = number * 10 + (line[j] - conversionMagicNumber);
                }
                else if (number != 0)
                {
                    var numberLength = number.ToString().Length;
                    var found = false;
                    
                    //
                    // top
                    if (!found && i != 0)
                    {
                        for (var k = Math.Max(0, j-numberLength-1); k < Math.Min(line.Length, j+1); k++)
                        {
                            if (symbols.Contains(input[i - 1][k]))
                            {
                                found = true;
                            }
                        }
                    }
                    
                    // bottom
                    if (!found && i != input.Length - 1)
                    {
                        for (var k = Math.Max(0, j-numberLength-1); k < Math.Min(line.Length, j+1); k++)
                        {
                            if (symbols.Contains(input[i + 1][k]))
                            {
                                found = true;
                            }
                        }
                    }
                    
                    // left
                    if (!found && j-numberLength-1 >= 0 && symbols.Contains(line[j-numberLength-1]))
                    {
                        found = true;
                    }
                    
                    // right
                    if (!found && j < line.Length - 1 && symbols.Contains(line[j]))
                    {
                        found = true;
                    }

                    if (found)
                    {
                        result += number;
                    }
                    number = 0;
                }
            }
        }
        return result;
    }

    [Test]
    public void Sample()
    {
        const string input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(4361));
    }

    [Test]
    public void Full()
    {
        // 61..388
        var input = File.ReadAllLines("Aoc03/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(546563));
    }
}