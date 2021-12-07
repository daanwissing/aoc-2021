public class Day3
{
    public static async Task Run()
    {
        var input = await File.ReadAllLinesAsync("inputs/input-3.txt");

        var gammas = input.ToList();
        var scrub = input.ToList();
        var pos = 0;

        while (gammas.Count > 1)
        {
            gammas = FilterGamma(gammas, pos);
            pos++;
        }
        Console.WriteLine();
        pos = 0;
        while (scrub.Count > 1)
        {
            scrub = FilterScrub(scrub, pos);
            pos++;
        }

        var gamma = Convert.ToInt32(gammas.First(), 2);
        System.Console.WriteLine(gamma);
        var scr = Convert.ToInt32(scrub.First(), 2);
        System.Console.WriteLine(scr);
        Console.WriteLine(gamma * scr);

    }

    private static List<string> FilterGamma(List<string> candidates, int pos)
    {
        var count = 0;
        foreach (var item in candidates)
        {
            if (item[pos] == '1')
                count++;
        }

        var common = count >= (candidates.Count / (decimal)2) ? '1' : '0';
        Console.WriteLine($"Gamma. Pos: {pos}, Total: {candidates.Count}, Count:{count}, Common:{common}");

        return candidates.Where(c => c[pos] == common).ToList();
    }

    private static List<string> FilterScrub(List<string> candidates, int pos)
    {
        var count = 0;
        foreach (var item in candidates)
        {
            if (item[pos] == '1')
                count++;
        }
        var common = count >= (candidates.Count /  (decimal)2) ? '1' : '0';

        Console.WriteLine($"Scrub. Pos: {pos}, Total: {candidates.Count}, Count:{count}, Common:{common}");

        return candidates.Where(c => c[pos] != common).ToList();
    }
}