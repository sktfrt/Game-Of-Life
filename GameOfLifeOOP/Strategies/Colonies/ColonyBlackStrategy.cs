namespace GameOfLifeOOP;

public class ColonyBlackStrategy : ICellLifeStrategy
{
    public CellType GetNext(CellContext ctx)
    {
        if (ctx.WhiteN > ctx.BlackN) return CellType.White;
        return ctx.AliveN is 1 or 2 or 3 ? CellType.Black : CellType.Empty;
    }
}
