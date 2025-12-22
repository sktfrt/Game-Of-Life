namespace GameOfLifeOOP;

public abstract class PatternBase
{
    public abstract PatternType Type { get; }
    public abstract PatternCell[,] Mask { get; }

    public bool IsMatch(ITerrain terrain, Cell cell)
    {
        var cells = terrain.GetCells();

        int fieldWidth  = Mask.GetLength(0);
        int fieldHeight = Mask.GetLength(1);

        for (int dx = 0; dx < fieldWidth; dx++)
        for (int dy = 0; dy < fieldHeight; dy++)
        {
            int nx = cell.X + dx;
            int ny = cell.Y + dy;

            if (nx < 0 || ny < 0 ||
                nx >= cells.GetLength(0) ||
                ny >= cells.GetLength(1))
                return false;

            var expected = Mask[dx, dy];
            var actual = cells[nx, ny].Type;

            switch (expected)
            {
                case PatternCell.Empty:
                    if (actual != CellType.Empty)
                        return false;
                    break;

                case PatternCell.Alive:
                    if (actual == CellType.Empty)
                        return false;
                    break;

                case PatternCell.Any:
                    break;
            }
        }

        return true;
    }
}

