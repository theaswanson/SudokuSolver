namespace SudokuSolver.Core;

public static class GameLogic
{
    public const int RowCount = 9;
    public const int ColumnCount = 9;
    public const int BoxCount = 9;
    public static readonly int[] PossibleValues = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    public static int GetBoxNumber(int row, int column) =>
        row switch
        {
            >= 0 and <= 2 when column is >= 0 and <= 2 => 0,
            >= 0 and <= 2 when column is >= 3 and <= 5 => 1,
            >= 0 and <= 2 => 2,
            >= 3 and <= 5 when column is >= 0 and <= 2 => 3,
            >= 3 and <= 5 when column is >= 3 and <= 5 => 4,
            >= 3 and <= 5 => 5,
            >= 6 and <= 8 when column is >= 0 and <= 2 => 6,
            >= 6 and <= 8 when column is >= 3 and <= 5 => 7,
            >= 6 and <= 8 => 8,
            _ => throw new Exception($"Could not get box number for ({row}, {column})")
        };
    
    public static (int rowStart, int rowEnd, int columnStart, int columnEnd) GetBoxDimensions(int boxNumber) =>
        boxNumber switch
        {
            0 => (0, 2, 0, 2),
            1 => (0, 2, 3, 5),
            2 => (0, 2, 6, 8),
            3 => (3, 5, 0, 2),
            4 => (3, 5, 3, 5),
            5 => (3, 5, 6, 8),
            6 => (6, 8, 0, 2),
            7 => (6, 8, 3, 5),
            8 => (6, 8, 6, 8),
            _ => throw new ArgumentOutOfRangeException(nameof(boxNumber), boxNumber, null)
        };
}