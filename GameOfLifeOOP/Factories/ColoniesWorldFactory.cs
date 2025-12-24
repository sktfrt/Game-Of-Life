namespace GameOfLifeOOP;

public class ColoniesWorldFactory : IWorldFactory
{
    public ICellStrategyProvider CreateStrategies()
    {
        var map = new Dictionary<CellType, ICellLifeStrategy>
        {
            { CellType.Empty, new ClassicEmptyStrategy() },
            { CellType.White, new ColonyWhiteStrategy() },
            { CellType.Black, new ColonyBlackStrategy() }
        };
        return new CellStrategyProvider(map);
    }

    public ICellFactory CreateCellFactory(ICellStrategyProvider strategies) => new SimpleCellFactory(strategies);

    public ColonyManager ColonyManager { get; private set; }

    public void InitializeColonyManager(Terrain terrain, PatternTerrainDecorator patterns)
    {
        ColonyManager = new ColonyManager(terrain);
    }
}