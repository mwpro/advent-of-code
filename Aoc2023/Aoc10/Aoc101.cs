namespace Aoc2023.Aoc10;

[TestFixture]
public class Aoc101
{
    private int Run(string[] input) => First(input);
    private int First(string[] input)
    {
        var start = (y: -1, x: -1);
        for (var i = 0; i < input.Length && start == (-1, -1); i++)
        {
            var line = input[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == 'S')
                {
                    start = (i, j);
                    break;
                }
            }
        }

        var length = 0;
        var currentPoint = start;
        var previousPoint = (y: -1, x: -1);
        var tmp = start;
        //var nextTile = start;
        do
        {
            var currentTile = input[currentPoint.y][currentPoint.x];
            Console.WriteLine($"{currentPoint} {currentTile}");
            if (currentPoint.y > 0 
                && !(previousPoint.y == currentPoint.y - 1 && previousPoint.x == currentPoint.x)
                && currentTile is '|' or 'L' or 'J' or 'S')
            {
                // top
                var topTile = input[currentPoint.y - 1][currentPoint.x];
                // | 7 F S
                if (topTile is '|' or '7' or 'F' or 'S')
                {
                    tmp = currentPoint;
                    previousPoint = currentPoint;
                    currentPoint = (tmp.y - 1, tmp.x);
                    length++;
                    continue;
                }
            } 
            if (currentPoint.x != input[0].Length - 1 
                && !(previousPoint.y == currentPoint.y && previousPoint.x == currentPoint.x + 1)
                && currentTile is '-' or 'L' or 'F' or 'S')
            {
                // right
                var rightTile = input[currentPoint.y][currentPoint.x + 1];
                // - J T 7 S
                if (rightTile is '-' or 'J' or '7' or 'S')
                {
                    tmp = currentPoint;
                    previousPoint = currentPoint;
                    currentPoint = (tmp.y, tmp.x + 1);
                    length++;
                    continue;
                }
            } 
            if (currentPoint.y != input[0].Length - 1 
                && !(previousPoint.y == currentPoint.y + 1 && previousPoint.x == currentPoint.x)
                && currentTile is '|' or '7' or 'F' or 'S')
            {
                // bottom
                var bottomTile = input[currentPoint.y + 1][currentPoint.x];
                // | L J S
                if (bottomTile is '|' or 'L' or 'J' or 'S')
                {
                    tmp = currentPoint;
                    previousPoint = currentPoint;
                    currentPoint = (tmp.y + 1, tmp.x);
                    length++;
                    continue;
                }
            } 
            if (currentPoint.x != 0 
                && !(previousPoint.y == currentPoint.y && previousPoint.x == currentPoint.x - 1)
                && currentTile is '-' or '7' or 'J' or 'S')
            {
                // left
                var leftTile = input[currentPoint.y][currentPoint.x - 1];
                // - L F S
                if (leftTile is '-' or 'L' or 'F' or 'S')
                {
                    tmp = currentPoint;
                    previousPoint = currentPoint;
                    currentPoint = (tmp.y, tmp.x - 1);
                    length++;
                    continue;
                }
            }
        } while (currentPoint != start);
        
        return length / 2;
    }
    
    
    [Test]
    public void Sample()
    {
        const string input = @".....
.S-7.
.|.|.
.L-J.
.....";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    public void Sample2()
    {
        const string input = @"..F7.
.FJ|.
SJ.L7
|F--J
LJ...";

        var result = Run(input.Split(Environment.NewLine));
        
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]

    public void Full()
    {
        var input = File.ReadAllLines("Aoc10/input.txt");
        
        var result = Run(input);

        Assert.That(result, Is.EqualTo(6864));
    }
}