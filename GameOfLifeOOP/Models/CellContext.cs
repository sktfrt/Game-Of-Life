namespace GameOfLifeOOP;

public record CellContext(
    CellType Self,
    int WhiteN,
    int BlackN,
    int AliveN
);
