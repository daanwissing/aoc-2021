using System.Text;

public class Day14 : Day
{
    public Day14() : base("inputs/input-14.txt")
    {
    }

    public override void Run()
    {
        var template = _input[0];

        Dictionary<string, char> rules = new();
        for (int i = 2; i < _input.Length; i++)
        {
            var p = _input[i].Split(" -> ");
            rules[p[0]] = p[1][0];
        }

        var pairsCounts = new Dictionary<string, long>();
        var charCounts = new Dictionary<char, long>();

        foreach (var c in template)
        {
            if (!charCounts.ContainsKey(c))
                charCounts[c] = 0;
            charCounts[c]++;
        }

        for (int index = 1; index < template.Length; index++)
        {
            string pair = template[(index - 1)..(index + 1)];
            if (!pairsCounts.ContainsKey(pair))
                pairsCounts[pair] = 0;
            pairsCounts[pair]++;
        }

            foreach (var x in pairsCounts)
                System.Console.WriteLine($"{x.Key} => {x.Value}");

        for (int round = 0; round < 40; round++)
        {
            var newPairs = new Dictionary<string, long>();


            foreach (var pair in pairsCounts)
            {
                if (rules.ContainsKey(pair.Key))
                {
                    var pair1 = $"{pair.Key[0]}{rules[pair.Key]}";
                    var pair2 = $"{rules[pair.Key]}{pair.Key[1]}";
                    if (!newPairs.ContainsKey(pair1))
                        newPairs[pair1] = 0;
                    newPairs[pair1] += pair.Value;

                    if (!newPairs.ContainsKey(pair2))
                        newPairs[pair2] = 0;
                    newPairs[pair2] += pair.Value;

                    if (!charCounts.ContainsKey(rules[pair.Key]))
                        charCounts[rules[pair.Key]] = 0;

                    charCounts[rules[pair.Key]] += pair.Value;
                }
            }

            foreach (var x in newPairs)
                System.Console.WriteLine($"Round {round}: {x.Key} => {x.Value}");


            pairsCounts = newPairs;
        }

        var max = charCounts.OrderByDescending(c => c.Value).First();
        var min = charCounts.OrderBy(c => c.Value).First();
        System.Console.WriteLine($"First: {max.Key} with {max.Value}");
        System.Console.WriteLine($"Last: {min.Key} with {min.Value}");
        System.Console.WriteLine($"Calc: {max.Value - min.Value}");
    }
}