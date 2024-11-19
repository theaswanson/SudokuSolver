namespace SudokuSolver.Core;

public class GameBoardSpace(int row, int column, int value)
{
    public int Row => row;
    public int Column => column;
    public int Value => value;
}
