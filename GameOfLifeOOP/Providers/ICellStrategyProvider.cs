namespace GameOfLifeOOP;

public interface ICellStrategyProvider
{
    ICellLifeStrategy For(CellType type);
}