namespace SudokuSolver.Core;

public class GameBoardFactory
{
    public GameBoard FromString(string input)
    {
        var rows = input.Split('\n');

        var spaces = new List<GameBoardSpace>();
        
        for (var rowNumber = 0; rowNumber < rows.Length; rowNumber++)
        {
            var row = rows[rowNumber];

            var values = row.Split(' ');

            for (var columnNumber = 0; columnNumber < values.Length; columnNumber++)
            {
                var space = new GameBoardSpace(rowNumber, columnNumber, int.Parse(values[columnNumber]));
                spaces.Add(space);
            }
        }

        return new GameBoard(spaces.ToArray());
    }
}