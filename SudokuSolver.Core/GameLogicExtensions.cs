namespace SudokuSolver.Core;

public static class GameLogicExtensions
{
    public static int[] GetValues(this GameBoardSpace[] spaces) =>
        spaces.Where(space => space.Value.HasValue)
            .Select(space => space.Value!.Value)
            .Distinct()
            .ToArray();
}