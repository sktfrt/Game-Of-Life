namespace GameOfLifeOOP;

public class FieldStatistics
{
    public int WhiteCount { get; private set; }
    public int BlackCount { get; private set; }

    public double WhiteDelta { get; private set; }
    public double BlackDelta { get; private set; }

    public TimeSpan UpdateTime { get; private set; }
    public TimeSpan ScanTime { get; private set; }

    public IReadOnlyDictionary<PatternType, int> PatternCounts { get; private set; }
        = new();

    public void UpdateFieldStatistics(
        StatsSnapshot before, 
        StatsSnapshot after, 
        TimeSpan update, 
        TimeSpan scan, 
        IReadOnlyDictionary<PatternType, int>? patterns)
    {
        WhiteDelta = Percent(before.White, after.White);
        BlackDelta = Percent(before.Black, after.Black);

        WhiteCount = after.White;
        BlackCount = after.Black;

        UpdateTime = update;
        ScanTime = scan;

        PatternCounts = new Dictionary<PatternType, int>(patterns);
    }

    private static double Percent(int before, int after)
    {
        if (before == 0)
            return after == 0 ? 0 : 100;

        return (after - before) * 100.0 / before;
    }
}
