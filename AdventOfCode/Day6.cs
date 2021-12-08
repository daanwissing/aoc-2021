public static class Day6
{
    public static async Task Run()
    {
        var buckets = new Dictionary<int, long>(){
            {0, 0},
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
        };
        (await File
            .ReadAllLinesAsync("inputs/input-6.txt"))
            .First()
            .Split(",")
            .Select(x => Convert.ToInt32(x))
            .ToList()
            .ForEach(x => {
                buckets[x]++;
            });

        var days = 256;
        for (int day = 0; day < days; day++)
        {
            var numNew = buckets[0];
            buckets[0] = buckets[1];
            buckets[1] = buckets[2];
            buckets[2] = buckets[3];
            buckets[3] = buckets[4];
            buckets[4] = buckets[5];
            buckets[5] = buckets[6];
            buckets[6] = buckets[7] + numNew;
            buckets[7] = buckets[8];

            buckets[8] = numNew;
        }

        var sum = buckets.Sum(b => b.Value);

        Console.WriteLine($"#Fish: {sum}");
    }
}