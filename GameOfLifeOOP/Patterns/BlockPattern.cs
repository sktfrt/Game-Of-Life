namespace GameOfLifeOOP;

public class BlockPattern : PatternBase
{
    public override PatternType Type => PatternType.Block;

    // Расширяем маску на 1 клетку вокруг с Empty
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}



