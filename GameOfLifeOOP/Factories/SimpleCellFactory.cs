namespace GameOfLifeOOP;

public class SimpleCellFactory : ICellFactory
{
    private readonly ICellStrategyProvider strategies;
    public SimpleCellFactory(ICellStrategyProvider strategies) => this.strategies = strategies;
    public Cell Create(int x, int y, CellType type) => new Cell(x, y, type, strategies);
}