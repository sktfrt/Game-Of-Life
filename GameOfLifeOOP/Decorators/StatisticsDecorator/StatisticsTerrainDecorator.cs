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

