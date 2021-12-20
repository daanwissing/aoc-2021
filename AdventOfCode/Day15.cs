public class Day15 : Day
{
    public Day15() : base("inputs/input-15.txt")
    {
    }

    private int[][] costs = new int[0][];

    private int dimX;
    private int dimY;


    public override void Run()
    {

        var extraSize = 5;

        dimY = _input.Length * extraSize;
        dimX = _input[0].Length * extraSize;
        costs = new int[dimY][];
        for (int y = 0; y < _input.Length; y++)
        {
            var line = _input[y].Select(c => Convert.ToInt32(char.GetNumericValue(c)));
            var calcLine = new List<int>();

            for (int i = 0; i < extraSize; i++)
            {
                calcLine.AddRange(line.Select(p => ((p + i - 1) % 9) + 1));
            }
            costs[y] = calcLine.ToArray();

            for (int i = 0; i < extraSize; i++)
            {
                var idx = y + (_input.Length * (i));
                costs[idx] = calcLine.Select(p => ((p + i - 1) % 9) + 1).ToArray();
            }
        }

        Dijkstra();
    }

    private void Dijkstra()
    {
        var dist = new int[dimY][];
        var prev = new (int, int)[dimY][];
        var prioQueue = new PriorityQueue<(int x, int y), int>();
        var left = new HashSet<(int, int)>();
        for (var y = 0; y < dimY; y++)
        {
            dist[y] = new int[dimX];
            prev[y] = new (int, int)[dimX];
            Array.Fill(dist[y], int.MaxValue);
            Array.Fill(prev[y], (-1, -1));
            for (int x = 0; x < dimX; x++)
            {
                Point item = new Point(x, y, dist[y][x]);
                left.Add((x, y));
            }
        }


        dist[0][0] = 0;
        prioQueue.Enqueue((0,0), 0);

        while (prioQueue.TryDequeue(out var current1, out _))
        {
            var current = (current1.x, current1.y);
            left.Remove(current);
            Visit(current, (x: current.x + 1, y: current.y));
            Visit(current, (x: current.x - 1, y: current.y));
            Visit(current, (x: current.x, y: current.y + 1));
            Visit(current, (x: current.x, y: current.y - 1));
        }

        System.Console.WriteLine($"Dijkstra: {dist[dimY - 1][dimX - 1]}");
        void Visit((int x, int y) current, (int x, int y) visit)
        {
            if (left.Contains(visit))
            {
                long alt = (long)dist[current.y][current.x] + costs[visit.y][visit.x];
                if (alt < dist[visit.y][visit.x])
                {
                    dist[visit.y][visit.x] = (int)alt;
                    prev[visit.y][visit.x] = current;

                    prioQueue.Enqueue(visit, (int)alt);
                }
            }
        }
    }


    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int cost { get; set; }

        public Point(int x, int y, int cost)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
        }
    }
}