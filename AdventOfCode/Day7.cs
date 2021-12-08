public static class Day7
{
    public static async Task Run()
    {
        var input = (await File.ReadAllTextAsync("inputs/input-7.txt"))
            .Split(',')
            .Select(x => Convert.ToInt32(x))
            .OrderBy(x => x)
            .ToList();

        var bestCost = int.MaxValue;
        for (var i = input[0]; i < input[^1]; i++)
        {
            var cost = 0;
            for (int j = 0; j < input.Count; j++)
            {
                var dist =  Math.Abs(input[j] - i);
                cost += (dist * (dist + 1)) / 2;
            }
            bestCost = Math.Min(bestCost, cost);
        }

        System.Console.WriteLine(bestCost);
    }
}