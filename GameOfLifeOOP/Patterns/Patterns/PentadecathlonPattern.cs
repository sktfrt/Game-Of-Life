namespace GameOfLifeOOP;

public class PentadecathlonPattern : PatternBase
{
    public override PatternType Type => PatternType.Pentadecathlon;

    public override PatternCell[,] Mask { get; } =
    {
        {
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty
        },

        {
            PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty
        },

        {
            PatternCell.Empty,
            PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive,
            PatternCell.Alive, PatternCell.Alive, PatternCell.Alive, PatternCell.Alive,
            PatternCell.Alive, PatternCell.Alive,
            PatternCell.Empty
        },

        {
            PatternCell.Empty, PatternCell.Empty, PatternCell.Alive, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Alive, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty
        },

        {
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty,
            PatternCell.Empty, PatternCell.Empty, PatternCell.Empty, PatternCell.Empty
        }
    };
}
