namespace GameOfLifeOOP;

public interface ICellLifeStrategy
{
    CellType GetNext(CellContext ctx);
}