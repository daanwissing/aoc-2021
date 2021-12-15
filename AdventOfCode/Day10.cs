public class Day10 : Day
{
    public Day10() : base("inputs/input-10.txt")
    {
    }

    private static string openings = "[{(<";
    private static string closings = "]})>";


    public override void Run()
    {
        var corruptSum = 0;
        var incompletes = new List<long>();
        foreach (var line in _input)
        {
            bool corrupted = false;
            string expectence = "";
            foreach (var ch in line)
            {
                var openIndex = openings.IndexOf(ch);
                if (openings.Contains(ch))
                {
                    expectence += ch;
                }
                else
                {
                    var expected = expectence[^1];
                    if (closings.IndexOf(ch) != openings.IndexOf(expected))
                    {
                        corruptSum += ch switch
                        {
                            ']' => 57,
                            '}' => 1197,
                            ')' => 3,
                            '>' => 25137,
                            _ => throw new ArgumentException()
                        };
                        corrupted = true;
                        break;
                    }
                    else
                    {
                        expectence = expectence.Remove(expectence.Length - 1);
                    }
                }
            }
            if (!corrupted)
            {

                var score = expectence.Reverse().Select(e => calcCost(e)).Aggregate(0L, (acc, t) => (acc * 5) + t);
                incompletes.Add(score);
            }
        }
        incompletes.Sort();
        System.Console.WriteLine($"Corrupt: {corruptSum}");
        System.Console.WriteLine($"Middle score: {incompletes[incompletes.Count / 2]}");


        long calcCost(char c) => c switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4,
            _ => throw new ArgumentException()
        };
    }
}