namespace Aoc2023.Aoc03;

[TestFixture]
public class Aoc032
{
    private const int ConversionMagicNumber = (int)'0';
    
    private int Run(string[] input) => Fast(input);
    
    private int Fast(string[] input)
    {
        var numbersInLines = new Dictionary<int, List<(int Value, int Start, int End)>>();
        
        var result = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] != '*')
                {
                    continue;
                }

                var gearRatio = 1;
                var adjacentNumbersCounter = 0;
               
                var currentLineNumbers = NumbersInLine(i);
                // left or right 
                foreach (var (value, start, end) in currentLineNumbers)
                {
                    if (end == j - 1 || start == j + 1)
                    {
                        adjacentNumbersCounter++;
                        gearRatio *= value;
                    }
                }
                
                // top
                if (adjacentNumbersCounter < 2 && i != 0)
                {
                    foreach (var (value, _, _) in NumbersInLine(i - 1).Where(x => 
                                 (x.Start <= j + 1) && (x.End >= j - 1)
                             ))
                    {
                        adjacentNumbersCounter++;
                        gearRatio *= value;
                    }
                }
                    
                // bottom
                if (adjacentNumbersCounter < 2 && i != input.Length - 1)
                {
                    foreach (var (value, _, _) in NumbersInLine(i + 1).Where(x => 
                                 (x.Start <= j + 1) && (x.End >= j - 1)
                             ))
                    {
                        adjacentNumbersCounter++;
                        gearRatio *= value;
                    }
                }
                
                if (adjacentNumbersCounter == 2)
                {
                    result += gearRatio;
                }
            }
        }
        return result;
        
        List<(int Value, int Start, int End)> NumbersInLine(int lineNumber)
        {
            if (numbersInLines.TryGetValue(lineNumber, out var existingValue))
            {
                return existingValue;
            }

            var numbersInLine = FindNumbersInLine(input[lineNumber]);
            numbersInLines.Add(lineNumber, numbersInLine);
            return numbersInLine;
        }
    }


    private int First(string[] input)
    {
        var adjacentNumbers = new List<int>();
        
        var result = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] != '*')
                {
                    continue;
                }
               
                // top
                if (i != 0)
                {
                    var topNumbers = FindNumbersInLine(input[i - 1]);
                    
                    adjacentNumbers.AddRange(topNumbers.Where(x => 
                        (x.Start <= j + 1) 
                        && (x.End >= j - 1)
                    
                    
                    ).Select(x => x.Value));
                }
                    
                // bottom
                if (i != input.Length - 1)
                {
                    var bottomNumbers = FindNumbersInLine(input[i + 1]);
                    
                    adjacentNumbers.AddRange(bottomNumbers.Where(x => 
                        (x.Start <= j + 1) 
                        && (x.End >= j - 1)
                    
                    
                    ).Select(x => x.Value));
                }
                    
                var currentLineNumbers = FindNumbersInLine(input[i]);
                // left
                var left = currentLineNumbers.FirstOrDefault(x => x.End == j - 1);
                if (left != default)
                {
                    adjacentNumbers.Add(left.Value);
                }
                 
                // right
                var right = currentLineNumbers.FirstOrDefault(x => x.Start == j + 1);
                if (right != default)
                {
                    adjacentNumbers.Add(right.Value);
                }
                
                if (adjacentNumbers.Count == 2)
                {
                    result += adjacentNumbers[0] * adjacentNumbers[1];
                }
                
                adjacentNumbers.Clear();
            }
        }
        return result;
    }

    private List<(int Value, int Start, int End)> FindNumbersInLine(string line)
    {
        var result = new List<(int Value, int Start, int End)>();
        var number = 0;
        var start = -1;
        for (var j = 0; j <= line.Length; j++)
        {
            if (j != line.Length && char.IsNumber(line[j]))
            {
                if (start == -1)
                {
                    start = j;
                }
                number = number * 10 + (line[j] - ConversionMagicNumber);
            }
            else if (number != 0)
            {
               result.Add(new (number, start, j-1));
               number = 0;
               start = -1;
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
        
        Assert.That(result, Is.EqualTo(467835));
    }

    [Test]
    public void Full()
    {
        // 61..388
        var input = File.ReadAllLines("Aoc03/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(91031374));
    }
}