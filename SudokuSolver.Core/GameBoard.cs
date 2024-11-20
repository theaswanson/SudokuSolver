using System.Text;

namespace SudokuSolver.Core;

public class GameBoard
{
    public GameBoard(GameBoardSpace[] boardSpaces)
    {
        ArgumentNullException.ThrowIfNull(boardSpaces);

        if (boardSpaces.Length != 9 * 9)
        {
            throw new ArgumentException($"Invalid number of board spaces. 81 are required, got {boardSpaces.Length}.");
        }

        if (GetDuplicateSpaces().Count > 0)
        {
            throw new ArgumentException($"Duplicate spaces detected. Found {GetDuplicateSpaces().Count}.");
        }
        
        BoardSpaces = boardSpaces;
        
        return;

        Dictionary<(int row, int column), int> GetDuplicateSpaces() =>
            boardSpaces.GroupBy(space => (space.Row, space.Column))
                .Where(g => g.Count() > 1)
                .ToDictionary(g => g.Key, g => g.Count());
    }

    public GameBoardSpace[] BoardSpaces { get; }

    public GameBoardSpace GetSpace(int row, int column) =>
        BoardSpaces.Single(space => space.Row == row && space.Column == column);
    
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

    public int GetBoxNumber(int row, int column) =>
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

    public GameBoardSpace[] GetUnsolvedSpaces() => BoardSpaces.Where(space => space.Value is null).ToArray();

    public string Print()
    {
        var stringBuilder = new StringBuilder();
        
        for (var i = 0; i < 9; i++)
        {
            var rowValues = GetSpacesForRow(i)
                .Select(space => space.Value.HasValue ? space.Value.Value.ToString() : "_")
                .ToArray();
            
            stringBuilder.AppendLine(string.Join(' ', rowValues));
        }
        
        return stringBuilder.ToString();
    }
    
    public bool IsSolved()
    {
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