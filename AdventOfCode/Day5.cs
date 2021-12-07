public static class Day5
{

    public static async Task Run()
    {
        var input = await File.ReadAllLinesAsync("inputs/input-5.txt");
        var lines = new List<Line>();
        var dimensions = (x: 0, y: 0);
        foreach (var l in input)
        {
            var pointsString = l.Split("->");

            var points = pointsString.Select(p => p.Split(",").Select(x => Convert.ToInt32(x.Trim())).ToArray()).ToArray();
            var line = new Line(points[0][0], points[0][1], points[1][0], points[1][1]);
            lines.Add(line);
            dimensions.x = Math.Max(dimensions.x, Math.Max(points[0][0], points[1][0]) + 1);
            dimensions.y = Math.Max(dimensions.y, Math.Max(points[0][1], points[1][1]) + 1);
        }

        int[][] grid = new int[dimensions.x][];
        for (int x = 0; x < dimensions.x; x++)
        {
            grid[x] = new int[dimensions.y];
        }

        foreach (var line in lines)
        {
            System.Console.WriteLine(line);
            if (line.x1 == line.x2)
            {
                for (int y = Math.Min(line.y1, line.y2); y <= Math.Max(line.y1, line.y2); y++)
                {
                    grid[line.x1][y]++;
                }
            }
            else if (line.y1 == line.y2)
            {
                for (int x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x1, line.x2); x++)
                {
                    grid[x][line.y1]++;
                }
            }
            else
            {
                var length = Math.Abs(line.x1 - line.x2) + 1; // 9
                if (line.x1 <= line.x2 && line.y1 <= line.y2)
                {
                    for (int x = 0; x < length; x++)
                    {
                        grid[line.x1 + x][line.y1 + x]++;
                    }
                }
                if (line.x1 > line.x2 && line.y1 <= line.y2)
                {
                    for (int x = 0; x < length; x++)
                    {
                        grid[line.x1 - x][line.y1 + x]++;
                    }
                }
                if (line.x1 <= line.x2 && line.y1 > line.y2)
                {
                    for (int x = 0; x < length; x++)
                    {
                        grid[line.x1 + x][line.y1 - x]++;
                    }
                }
                if (line.x1 > line.x2 && line.y1 > line.y2)
                {
                    for (int x = 0; x < length; x++)
                    {
                        grid[line.x1 - x][line.y1 - x]++;
                    }
                }

            }

        }

        int overlapping = grid.Sum(l => l.Where(p => p > 1).Count());
        System.Console.WriteLine($"Overlapping: {overlapping}");
    }

    private static void PrintGrid(int[][] grid)
    {
        foreach (var item in grid)
        {
            foreach (var p in item)
            {
                Console.Write(p);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public record Line(int x1, int y1, int x2, int y2);
}