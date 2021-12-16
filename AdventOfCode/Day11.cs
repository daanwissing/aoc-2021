public class Day11 : Day
{
    public Day11() : base("inputs/input-11.txt")
    {
    }
    public override void Run()
    {
        // Load octopi
        int[][] grid = new int[_input.Length][];
        int dimY = _input.Length;
        int dimX = _input[0].Length;
        for (int y = 0; y < dimY; y++)
        {
            var line = _input[y];
            grid[y] = line.Select(c => Convert.ToInt32(c.ToString())).ToArray();
        }


        var rounds = 100000;
        var totalFlashes = 0;
        for (int i = 0; i < rounds; i++)
        {
            // 1: all ++
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    ++grid[x][y];
                }
            }

            // 2: flashes
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    if (grid[x][y] >= 10)
                    {
                        grid[x][y] = -100;
                        // Increase all neighbors
                        List<(int x, int y)> toVisit = new List<(int x, int y)> {
                            (x - 1, y - 1),
                            (x, y - 1),
                            (x + 1, y - 1),
                            (x - 1, y),
                            (x + 1, y),
                            (x - 1, y + 1),
                            (x, y + 1),
                            (x + 1, y + 1)
                        };

                        while (toVisit.Any())
                        {
                            var current = toVisit.First();
                            toVisit.Remove(current);
                            if (current.x < 0 || current.x >= dimX || current.y < 0 || current.y >= dimY) continue; // out of bounds
                            ++grid[current.x][current.y];
                            if (grid[current.x][current.y] >= 10) // Becomes 10 now, so flash
                            {
                                grid[current.x][current.y] = -100;
                                toVisit.Add((current.x - 1, current.y - 1));
                                toVisit.Add((current.x, current.y - 1));
                                toVisit.Add((current.x + 1, current.y - 1));
                                toVisit.Add((current.x - 1, current.y));
                                toVisit.Add((current.x + 1, current.y));
                                toVisit.Add((current.x - 1, current.y + 1));
                                toVisit.Add((current.x, current.y + 1));
                                toVisit.Add((current.x + 1, current.y + 1));
                            };
                        }
                    }
                }
            }

            // 3: reset flashed
            int flashes = 0;
            for (int x = 0; x < dimX; x++)
            {
                for (int y = 0; y < dimY; y++)
                {
                    if (grid[x][y] < 0)
                    {
                        grid[x][y] = 0;
                        flashes++;
                    }
                }
            }

            totalFlashes += flashes;

            if (flashes == dimX * dimY)
            {
                System.Console.WriteLine($"In sync in round {i + 1}");
                break;
            }
        }

        System.Console.WriteLine($"Flashes: {totalFlashes}");
    }
}