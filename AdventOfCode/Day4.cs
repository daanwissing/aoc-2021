public class Day4
{

    static Dictionary<int, int> drawnNumbers = new();

    public static async Task Run()
    {
        var input = await File.ReadAllLinesAsync("inputs/input-4.txt");

        var idx = 0;
        foreach (var num in input[0].Split(',').Select(x => Convert.ToInt32(x)))
        {
            drawnNumbers.Add(num, idx);
            idx++;
        };

        Board b = new Board();
        var boards = new List<Board>();
        int lineNum = 0;
        foreach (var line in input.Skip(2))
        {
            if (string.IsNullOrEmpty(line))
            {
                b.CalcWin();
                boards.Add(b);
                lineNum = 0;
                b = new Board();
                continue;
            }

            b.ReadBoardLine(line, lineNum);
            lineNum++;
        }

        var winningBoard = boards.OrderBy(b => b.WinRound).First();
        var losingBoard = boards.OrderByDescending(b => b.WinRound).First();
        System.Console.WriteLine($"Winner: {winningBoard.Score}");
        System.Console.WriteLine($"Loser : {losingBoard.Score}");
    }

    public class Board
    {
        public int[][] Numbers { get; set; }
        public int[][] CrossedAt { get; set; }

        const int DIMENSION = 5;

        public int WinRound { get; private set; } = int.MaxValue;
        public int Score { get; private set; } = 0;

        public int WinningNumber { get; private set; }

        public Board()
        {
            Numbers = new int[DIMENSION][];
            CrossedAt = new int[DIMENSION][];
        }

        public void ReadBoardLine(string line, int lineNum)
        {
            Numbers[lineNum] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
            int[] ca = new int[DIMENSION];
            int winLine = int.MinValue;
            for (int i = 0; i < DIMENSION; i++)
            {
                int num = Numbers[lineNum][i];
                ca[i] = drawnNumbers.ContainsKey(num) ? drawnNumbers[num] : int.MaxValue;
            }
            CrossedAt[lineNum] = ca;
            winLine = ca.Max();
            WinRound = Math.Min(WinRound, winLine);
        }

        public void CalcWin()
        {
            for (int i = 0; i < DIMENSION; i++)
            {
                var winCol = int.MinValue;
                for (int j = 0; j < DIMENSION; j++)
                {
                    winCol = Math.Max(winCol, CrossedAt[j][i]);
                }
                WinRound = Math.Min(WinRound, winCol);
            }

            // Calc score

            var s = 0;
            
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    if (CrossedAt[j][i] > WinRound)
                        s += Numbers[j][i];
                    if (CrossedAt[j][i] == WinRound)
                        WinningNumber = Numbers[j][i];
                }
            }
            Score = s * WinningNumber;
        }
    }
}