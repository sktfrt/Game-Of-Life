namespace GameOfLifeOOP;

public class ColonyWhiteStrategy : ICellLifeStrategy
{
    public CellType GetNext(CellContext ctx)
    {
        if (ctx.BlackN > ctx.WhiteN) return CellType.Black;
        return ctx.AliveN is 2 or 3 ? CellType.White : CellType.Empty;
    }
}