public class Day1 {

    public static async Task Run(){
        var input = await File.ReadAllLinesAsync("inputs/input-1.txt");

        int prev = Convert.ToInt32(input[0]);
        var result = input
            .Skip(1)
            .Count(i => 
            {   
                var num = Convert.ToInt32(i);
                var prev1 = prev;
                prev = num;
                return num > prev1;
            });
        Console.WriteLine($"count: {result}");

        int prev1 = Convert.ToInt32(input[0]);
        int prev2 = Convert.ToInt32(input[1]);
        int prev3 = Convert.ToInt32(input[2]);
        result = input
            .Skip(3)
            .Count(i => 
            {   
                var num = Convert.ToInt32(i);
                var comp1 = prev1 + prev2 + prev3;
                prev1 = prev2;
                prev2 = prev3;
                prev3 = num;
                var comp2 = prev1 + prev2 + prev3;
                return comp2 > comp1;
            });

        Console.WriteLine($"count: {result}");
    }
}
