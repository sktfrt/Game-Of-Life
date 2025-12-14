namespace GameOfLifeOOP;

public class ClassicWhiteStrategy : ICellLifeStrategy
{
    public CellType GetNext(CellContext ctx) => ctx.AliveN is 2 or 3 ? CellType.White : CellType.Empty;
}