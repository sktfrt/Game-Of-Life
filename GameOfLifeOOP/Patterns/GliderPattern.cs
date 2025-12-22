namespace GameOfLifeOOP;

public class PlanerPatternRightDown : PatternBase
{
    public override PatternType Type => PatternType.Glider;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

public class PlanerPatternDownLeft : PatternBase
{
    public override PatternType Type => PatternType.Glider;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

public class PlanerPatternLeftUp : PatternBase
{
    public override PatternType Type => PatternType.Glider;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

public class PlanerPatternUpRight : PatternBase
{
    public override PatternType Type => PatternType.Glider;
    public override PatternCell[,] Mask => new PatternCell[,]
    {
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty, PatternCell.Alive },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Alive, PatternCell.Empty },
        { PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty }
    };
}

