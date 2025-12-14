namespace GameOfLifeOOP;

public class Terrain
{
    private Cell[,] cells;
    private readonly int width, height;

    public Terrain(int w, int h, ICellFactory factory)
    {
        width = w; height = h;
        cells = new Cell[w, h];
        for (int x = 0; x < w; x++)
        for (int y = 0; y < h; y++)
            cells[x, y] = factory.Create(x, y, CellType.Empty);
    }

    public void Reinitialize(ICellFactory factory)
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            cells[x, y] = factory.Create(x, y, CellType.Empty);
    }

    public void Update()
    {
        foreach (var cell in cells)
        {
            var ctx = BuildContext(cell);
            cell.PlanNext(ctx);
        }
    }

    private CellContext BuildContext(Cell cell)
    {
        int white = 0, black = 0;
        foreach (var n in GetNeighbors(cell))
        {
            if (n.Type == CellType.White) white++;
            else if (n.Type == CellType.Black) black++;
        }
        return new CellContext(cell.Type, white, black, white + black);
    }

    private IEnumerable<Cell> GetNeighbors(Cell cell)
    {
        int startX = Math.Max(0, cell.X - 1);
        int endX = Math.Min(width - 1, cell.X + 1);
        int startY = Math.Max(0, cell.Y - 1);
        int endY = Math.Min(height - 1, cell.Y + 1);

        for (int x = startX; x <= endX; x++)
        for (int y = startY; y <= endY; y++)
            if (x != cell.X || y != cell.Y)
                yield return cells[x, y];
    }


    public Cell[,] GetCells() => cells;
}