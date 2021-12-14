public abstract class Day
{
    protected string[] _input { get; }

    public Day(string inputFile)
    {
        _input = File.ReadAllLines(inputFile);
    }

    public abstract Task Run();
}