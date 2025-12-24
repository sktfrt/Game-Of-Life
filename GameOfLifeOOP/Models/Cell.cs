namespace GameOfLifeOOP;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    private readonly ICellStrategyProvider provider;

    public Cell(int x, int y, CellType type, ICellStrategyProvider provider)
    {
        X = x; Y = y; Type = type; this.provider = provider;
    }

    public void PlanNext(CellContext ctx) => Type = provider.For(Type).GetNext(ctx);
}