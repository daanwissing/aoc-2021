public class Day9 : Day
{
    public Day9() : base("inputs/input-9.txt")
    {
    }

    public override Task Run()
    {
        int dimX = _input.Length;
        int dimY = _input[0].Length;

        int[][] grid = new int[dimX][];
        int[][] basinMap = new int[dimX][];


        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = _input[i].Select(c => Convert.ToInt32(c.ToString())).ToArray();
            basinMap[i] = new int[dimY];
            Array.Fill(basinMap[i], -1); // Default ridges
        }

        var risk = 0;

        List<int> basins = new();
        var lowpoints = new List<(int x, int y)>();

        for (int x = 0; x < dimX; x++)
        {
            for (int y = 0; y < dimY; y++)
            {
                var smallest = true;
                var num = grid[x][y];
                smallest &= (x <= 0 || grid[x - 1][y] > num);
                smallest &= (x >= dimX - 1 || grid[x + 1][y] > num);
                smallest &= (y <= 0 || grid[x][y - 1] > num);
                smallest &= (y >= dimY - 1 || grid[x][y + 1] > num);

                if (smallest)
                {
                    risk += (grid[x][y] + 1);
                    lowpoints.Add((x: x, y: y));
                }
            }
        }

        var basins2 = new List<int>();
        for (int i = 0; i < lowpoints.Count; i++)
        {
            (int x, int y) lp = lowpoints[i];
            basins2.Add(0);
            HashSet<(int x, int y)> visited = new();
            HashSet<(int x, int y)> toVisit = new HashSet<(int x, int y)> { lp };
            while (toVisit.Any())
            {
                var current = toVisit.First();
                toVisit.Remove(current);
                visited.Add(current);
                basins2[i]++;
                if ((current.x > 0) && (grid[current.x - 1][current.y] != 9) && !visited.Contains((current.x - 1, current.y))) 
                    toVisit.Add((current.x - 1, current.y));
                if ((current.x < dimX - 1) && (grid[current.x + 1][current.y] != 9) && !visited.Contains((current.x + 1, current.y))) 
                    toVisit.Add((current.x + 1, current.y));
                if ((current.y > 0) && (grid[current.x][current.y - 1] != 9) && !visited.Contains((current.x, current.y - 1))) 
                    toVisit.Add((current.x, current.y - 1));
                if ((current.y < dimY - 1) && (grid[current.x][current.y + 1] != 9) && !visited.Contains((current.x, current.y + 1))) 
                    toVisit.Add((current.x, current.y + 1));

            }
        }
        var orderedBasins = basins2.OrderByDescending(b => b).ToArray();

        System.Console.WriteLine($"risk: {risk}");
        var largests = orderedBasins[0] * orderedBasins[1] * orderedBasins[2];

        System.Console.WriteLine($"total of three: {largests}");

        return Task.CompletedTask;
    }
}