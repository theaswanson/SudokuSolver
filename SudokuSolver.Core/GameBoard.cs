namespace SudokuSolver.Core;

public class GameBoard(GameBoardSpace[] boardSpaces)
{
    private GameBoardSpace[] BoardSpaces { get; } = boardSpaces;

    public GameBoardSpace[] GetSpacesForRow(int rowNumber) =>
        BoardSpaces
            .Where(space => space.Row == rowNumber)
            .ToArray();
    
    public GameBoardSpace[] GetSpacesForColumn(int columnNumber) =>
        BoardSpaces
            .Where(space => space.Column == columnNumber)
            .ToArray();

    public GameBoardSpace[] GetSpacesForBox(int boxNumber)
    {
        var (rowStart, rowEnd, columnStart, columnEnd) =
            ((int rowStart, int rowEnd, int columnStart, int columnEnd))(boxNumber switch
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
            });
        
        return BoardSpaces
            .Where(space => space.Row >= rowStart && space.Row <= rowEnd && space.Column >= columnStart && space.Column <= columnEnd)
            .ToArray();
    }

    public bool Verify()
    {
        if (BoardSpaces.Length < 9 * 9)
        {
            return false;
        }

        if (BoardSpaces.Any(space => space.Row < 0 || space.Row > 9 || space.Column < 0 || space.Column > 9))
        {
            return false;
        }

        for (var row = 0; row < 9; row++)
        {
            var boardSpaces = GetSpacesForRow(row);

            for (var i = 1; i < 10; i++)
            {
                if (boardSpaces.Count(space => space.Value == i) != 1)
                {
                    return false;
                }
            }
        }
        
        for (var column = 0; column < 9; column++)
        {
            var boardSpaces = GetSpacesForColumn(column);

            for (var i = 1; i < 10; i++)
            {
                if (boardSpaces.Count(space => space.Value == i) != 1)
                {
                    return false;
                }
            }
        }
        
        for (var box = 0; box < 9; box++)
        {
            var boardSpaces = GetSpacesForBox(box);

            for (var i = 1; i < 10; i++)
            {
                if (boardSpaces.Count(space => space.Value == i) != 1)
                {
                    return false;
                }
            }
        }

        return true;
    }
}