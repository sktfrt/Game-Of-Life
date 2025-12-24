namespace GameOfLifeOOP;

public class BlinkerPatternHorizontal : PatternBase
{
    public override PatternType Type => PatternType.Blinker;

    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

public class BlinkerPatternVertical : PatternBase
{
    public override PatternType Type => PatternType.Blinker;

    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
    };
}
