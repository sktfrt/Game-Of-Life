namespace GameOfLifeOOP;

public static class Patterns
{
    public static List<PatternBase> patterns = new List<PatternBase>
    {
        new BlockPattern(),

        new BlinkerPatternHorizontal(),
        new BlinkerPatternVertical(),

        new PlanerPatternRightDown(),
        new PlanerPatternDownLeft(),
        new PlanerPatternLeftUp(),
        new PlanerPatternUpRight(),

        new PentadecathlonPattern(),

        new BeehivePatternHorizontal(),
        new BeehivePatternVertical()
    };
}