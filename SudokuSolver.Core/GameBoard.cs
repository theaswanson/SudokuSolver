using System.Text;

namespace SudokuSolver.Core;

public class GameBoard
{
    public GameBoard(GameBoardSpace[] boardSpaces)
    {
        ArgumentNullException.ThrowIfNull(boardSpaces);

        if (boardSpaces.Length != GameLogic.RowCount * GameLogic.ColumnCount)
        {
            throw new ArgumentException($"Invalid number of board spaces. {GameLogic.RowCount * GameLogic.ColumnCount} are required, got {boardSpaces.Length}.");
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
        var (rowStart, rowEnd, columnStart, columnEnd) = GameLogic.GetBoxDimensions(boxNumber);
        
        return BoardSpaces
            .Where(space => space.Row >= rowStart
                            && space.Row <= rowEnd
                            && space.Column >= columnStart
                            && space.Column <= columnEnd)
            .ToArray();
    }

    public GameBoardSpace[] GetUnsolvedSpaces() =>
        BoardSpaces
            .Where(space => space.Value is null)
            .ToArray();

    public string Print()
    {
        var stringBuilder = new StringBuilder();
        
        for (var i = 0; i < GameLogic.RowCount; i++)
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
        if (!RowsAreValid())
        {
            return false;
        }

        if (!ColumnsAreValid())
        {
            return false;
        }

        if (!BoxesAreValid())
        {
            return false;
        }

        return true;

        bool EachValueOccursExactlyOnce(GameBoardSpace[] boardSpaces) =>
            GameLogic.PossibleValues.All(possibleValue =>
                boardSpaces.Count(space => space.Value == possibleValue) == 1);

        bool RowsAreValid()
        {
            for (var row = 0; row < GameLogic.RowCount; row++)
            {
                var boardSpaces = GetSpacesForRow(row);

                if (!EachValueOccursExactlyOnce(boardSpaces))
                {
                    return false;
                }
            }

            return true;
        }

        bool ColumnsAreValid()
        {
            for (var column = 0; column < GameLogic.ColumnCount; column++)
            {
                var boardSpaces = GetSpacesForColumn(column);

                if (!EachValueOccursExactlyOnce(boardSpaces))
                {
                    return false;
                }
            }

            return true;
        }

        bool BoxesAreValid()
        {
            for (var box = 0; box < GameLogic.BoxCount; box++)
            {
                var boardSpaces = GetSpacesForBox(box);

                if (!EachValueOccursExactlyOnce(boardSpaces))
                {
                    return false;
                }
            }

            return true;
        }
    }
}