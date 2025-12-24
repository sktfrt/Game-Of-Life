namespace GameOfLifeOOP;

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