namespace GameOfLifeOOP;

public class BeehivePatternHorizontal : PatternBase
{
    public override PatternType Type => PatternType.Beehive;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Alive },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

public class BeehivePatternVertical : PatternBase
{
    public override PatternType Type => PatternType.Beehive;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
    };
}
