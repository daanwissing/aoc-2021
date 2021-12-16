public class Day13 : Day
{
    public Day13() : base("inputs/input-13.txt")
    {

    }

    public override void Run()
    {
        bool inFolds = false;

        var dimX = 0;
        var dimY = 0;

        var dots = new List<(int, int)>();
        var folds = new List<(int l, bool x)>();

        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                inFolds = true;
                continue;
            }
            if (!inFolds)
            {
                var coors = line.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
                dimX = Math.Max(dimX, coors[0] + 1);
                dimY = Math.Max(dimY, coors[1] + 1);
                dots.Add((coors[0], coors[1]));
            }
            else
            {
                var l = line.Substring("fold along ".Length);
                var spl = l.Split("=");
                folds.Add((Convert.ToInt32(spl[1]), spl[0] == "x"));
            }
        }

        System.Console.WriteLine($"dimX: {dimX}, dimY: {dimY}");

        var grid = new bool[dimY][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new bool[dimX];
        }

        foreach (var (x, y) in dots)
        {
            grid[y][x] = true;
        }

        foreach (var fold in folds)
        {
            if (fold.x)
            {
                for (int y = 0; y < dimY; y++)
                {
                    for (int x = 1; x < (dimX - fold.l); x++)
                    {
                        grid[y][fold.l - x] |= grid[y][fold.l + x];
                    }
                }
                dimX = fold.l;
            }
            else
            {
                for (int y = 1; y < (dimY - fold.l); y++)
                {
                    for (int x = 0; x < dimX; x++)
                    {
                        grid[fold.l - y][x] |= grid[fold.l + y][x];
                    }
                }
                dimY = fold.l;
            }
        }

        for (int y = 0; y < dimY; y++)
        {
            for (int x = 0; x < dimX; x++)
            {
                Console.Write(grid[y][x] ? "##" : "..");
            }
            Console.WriteLine();
        }

        var count = 0;
        for (int y = 0; y < dimY; y++)
        {
            for (int x = 0; x < dimX; x++)
            {
                if (grid[y][x]) count++;
            }
        }

        System.Console.WriteLine($"Count: {count}");

    }
}