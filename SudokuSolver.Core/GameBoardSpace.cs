namespace SudokuSolver.Core;

public class GameBoardSpace
{
    private readonly int _row;
    private readonly int _column;
    private int? _value;

    public GameBoardSpace(int row, int column, int? value)
    {
        if (row is < 0 or > 8)
        {
            throw new ArgumentOutOfRangeException(nameof(row));
        }
        
        if (column is < 0 or > 8)
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }
        
        if (value is < 1 or > 9)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        
        _row = row;
        _column = column;
        _value = value;
    }

    public int Row => _row;
    public int Column => _column;
    public int? Value => _value;
    public void SetValue(int? value) => _value = value;
}
