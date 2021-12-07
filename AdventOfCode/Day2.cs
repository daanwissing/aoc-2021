public class Day2
{
    public static async Task Run()
    {
        var input = await File.ReadAllLinesAsync("inputs/input-2.txt");

        var pos = (hor: 0, depth:0, aim: 0);

        Array.ForEach(input, action =>
            {
                var c = action.Split(" ", 2);
                var n = Convert.ToInt32(c[1]);
                switch (c[0])
                {
                    case "forward":
                        pos.hor += n;
                        pos.depth += (pos.aim * n);
                        break;
                    case "down":
                        pos.aim += n;
                        break;
                    case "up":
                        pos.aim -= n;
                        break;
                }
            });

        Console.WriteLine(pos.hor * pos.depth);
    }
}