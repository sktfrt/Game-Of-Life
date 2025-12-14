namespace GameOfLifeOOP;

public class ClassicEmptyStrategy : ICellLifeStrategy
{
    public CellType GetNext(CellContext ctx) => ctx.AliveN == 3 ? CellType.White : CellType.Empty;
}