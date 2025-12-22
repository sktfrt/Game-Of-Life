using System.Collections.Generic;

namespace GameOfLifeOOP;

class PatternTerrainDecorator : ITerrain
{
    private readonly ITerrain _inner;
    private readonly IEnumerable<PatternBase> _patterns;

    public HashSet<(int x, int y)> PatternCells { get; private set; }

    public PatternTerrainDecorator(ITerrain inner)
    {
        _inner = inner;
        _patterns = Patterns.patterns;
        PatternCells = new HashSet<(int, int)>();
    }

    public void Update()
    {
        _inner.Update();
        RecalculatePatterns();
    }

    public void Reinitialize(ICellFactory factory)
    {
        _inner.Reinitialize(factory);
        RecalculatePatterns();
    }

    public Cell[,] GetCells() => _inner.GetCells();

    public void RecalculatePatterns()
    {
        PatternCells.Clear();

        var cells = _inner.GetCells();
        int fieldWidth  = cells.GetLength(0);
        int fieldHeight = cells.GetLength(1);

        foreach (var pattern in _patterns)
        {
            var mask = pattern.Mask;
            int maskWidth  = mask.GetLength(0);
            int maskHeight = mask.GetLength(1);

            int found = 0;

            for (int x = 0; x <= fieldWidth - maskWidth; x++)
            for (int y = 0; y <= fieldHeight - maskHeight; y++)
            {
                var anchorCell = cells[x, y];

                if (!pattern.IsMatch(_inner, anchorCell))
                    continue;

                found++;                

                for (int dx = 0; dx < maskWidth; dx++)
                for (int dy = 0; dy < maskHeight; dy++)
                {
                    if (mask[dx, dy] == PatternCell.Any)
                        continue;

                    PatternCells.Add((x + dx, y + dy));
                }
            }

            Patterns.Counts[pattern.Type] = found;
        }
    }
}

