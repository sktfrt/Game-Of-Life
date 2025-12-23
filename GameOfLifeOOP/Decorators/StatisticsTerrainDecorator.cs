using System;
using System.Diagnostics;
using System.Drawing;

namespace GameOfLifeOOP;

public class StatisticsTerrainDecorator : ITerrain
{
    private readonly ITerrain _inner;
    public FieldStatistics Stats { get; } = new();
    private readonly PatternTerrainDecorator patternStats;

    public StatisticsTerrainDecorator(ITerrain inner, PatternTerrainDecorator patterns)
    {
        _inner = inner;
        patternStats = patterns;
    }

    public void Update()
    {
        var swUpdate = Stopwatch.StartNew();
        var before = StatsSnapshot.Take(_inner);

        var swScan = Stopwatch.StartNew();
        _inner.Update();
        swScan.Stop();

        var after = StatsSnapshot.Take(_inner);
        swUpdate.Stop();

        Stats.UpdateFieldStatistics(before, after, swUpdate.Elapsed, swScan.Elapsed, patternStats.PatternCounts);
    }

    public Cell[,] GetCells() => _inner.GetCells();

    public void Draw(Graphics g) => _inner.Draw(g);

    public void Reinitialize(ICellFactory factory) => _inner.Reinitialize(factory);
}

public class FieldStatistics
{
    public int WhiteCount { get; private set; }
    public int BlackCount { get; private set; }

    public double WhiteDelta { get; private set; }
    public double BlackDelta { get; private set; }

    public TimeSpan UpdateTime { get; private set; }
    public TimeSpan ScanTime { get; private set; }

    public IReadOnlyDictionary<PatternType, int> PatternCounts { get; private set; }
        = new Dictionary<PatternType, int>();

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

public record StatsSnapshot(int White, int Black)
{
    public static StatsSnapshot Take(ITerrain terrain)
    {
        int w = 0, b = 0;
        foreach (var cell in terrain.GetCells())
        {
            if (cell.Type == CellType.White) w++;
            if (cell.Type == CellType.Black) b++;
        }
        return new StatsSnapshot(w, b);
    }
}
