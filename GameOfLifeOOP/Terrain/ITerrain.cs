namespace GameOfLifeOOP;

public interface ITerrain
{
    public Cell[,] GetCells();
    
    public void Update();

    public void Reinitialize(ICellFactory factory);
}