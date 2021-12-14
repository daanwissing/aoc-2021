public class Day8 : Day
{


    public Day8() : base("inputs/input-8.txt")
    {
    }

    public override Task Run()
    {
        var sum = 0;
        foreach (var line in _input)
        {
            string input, output;
            var split = line.Split('|');
            input = split[0].Trim();
            output = split[1].Trim();
            var inputStrings = ParseNumbers(input).OrderBy(i => i.Length).ToArray();
            var outputNumbers = ParseNumbers(output);

            Dictionary<string, int> lookup = new();
            lookup[inputStrings[0]] = 1; // 2 segments
            lookup[inputStrings[1]] = 7; // 3 segments
            lookup[inputStrings[2]] = 4; // 4 segments
            lookup[inputStrings[9]] = 8; // 7 segments

            // Get 6
            var candidates6 = inputStrings.Where(n => n.Length == 6); // Gives 3 records
            string string6 = "";
            foreach (var c in candidates6)
            {
                if (c.Intersect(inputStrings[0]).Count() == 1)
                {
                    lookup[c] = 6;
                    string6 = c;
                }
            }

            // Get segment c
            var segmentC = inputStrings[0].Except(string6).First();
            var string5 = "";
            var candidates532 = inputStrings.Where(n => n.Length == 5); // 3 records
            foreach (var c in candidates532)
            {
                var intersect1 = c.Intersect(inputStrings[0]).Count();
                var intersect7 = c.Intersect(inputStrings[1]).Count();
                if (intersect7 == 3)
                {
                    // Get 3
                    lookup[c] = 3;
                }
                else if (c.Contains(segmentC))
                {
                    // Get 2
                    lookup[c] = 2;
                }
                else
                {
                    // Get 5
                    lookup[c] = 5;
                    string5 = c;
                }
            }

            foreach (var c in candidates6.Where(c => c != string6))
            {
                // Get 9
                if (c.Intersect(string5).Count() == 5)
                {
                    lookup[c] = 9;
                }
                // Get 0
                else
                {
                    lookup[c] = 0;
                }
            }
            var num = 0;
            for (int i = 0; i < outputNumbers.Length; i++)
            {
                string? s = outputNumbers[i];
                num += (lookup[s] * (int)Math.Pow(10, 3 - i));
            }
            sum += num;

        }
        Console.WriteLine(sum);
        return Task.CompletedTask;
    }

    private static string[] ParseNumbers(string input)
    {
        var inputNumbers = input.Split(' ');
        var result = new List<HashSet<char>>();
        for (int i = 0; i < inputNumbers.Length; i++)
        {
            inputNumbers[i] = string.Join("", inputNumbers[i].Trim().OrderBy(c => c));
        }

        return inputNumbers ?? new string[0];
    }
}