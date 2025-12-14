namespace GameOfLifeOOP;

public interface ICellFactory
{
    Cell Create(int x, int y, CellType type);
}