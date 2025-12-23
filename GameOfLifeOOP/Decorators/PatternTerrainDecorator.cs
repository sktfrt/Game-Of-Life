using System.Collections.Generic;

namespace GameOfLifeOOP;

public class PatternTerrainDecorator : ITerrain
{
    private readonly ITerrain _inner;
    private readonly IEnumerable<PatternBase> _patterns;

    public HashSet<(int x, int y)> PatternCells { get; private set; }
    public Dictionary<PatternType, int> PatternCounts { get; private set; } = new();


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

    public void Draw(Graphics g)
    {
        _inner.Draw(g);
    }

    public void RecalculatePatterns()
    {
        PatternCells.Clear();
        PatternCounts.Clear();

        var cells = _inner.GetCells();
        int fieldWidth = cells.GetLength(0);
        int fieldHeight = cells.GetLength(1);

        foreach (var pattern in _patterns)
        {
            var mask = pattern.Mask;
            int maskWidth = mask.GetLength(0);
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
                
            PatternCounts[pattern.Type] = found;
        }
    }

    private int cellSize = 20;

    public void Draw(Graphics g, bool showOnlyPatterns = false)
    {
        foreach (var cell in _inner.GetCells())
        {
            DrawCell(g, cell, PatternCells.Contains((cell.X, cell.Y)));
        }
    }

    private void DrawCell(Graphics g, Cell cell, bool isPattern)
    {
        Brush brush = cell.Type switch
        {
            CellType.White => isPattern ? Brushes.Red : Brushes.White,
            CellType.Black => isPattern ? Brushes.Red : Brushes.Black,
            _ => Brushes.Gray
        };

        int px = cell.X * cellSize;
        int py = cell.Y * cellSize;

        if (cell.Type != CellType.Empty)
            g.FillRectangle(brush, px, py, cellSize, cellSize);

        g.DrawRectangle(Pens.Gray, px, py, cellSize - 1, cellSize - 1);
    }
}
