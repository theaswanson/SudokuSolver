namespace SudokuSolver.Core;

public class GameBoardFactory
{
    public GameBoard FromString(string input)
    {
        var rows = input.Split(Environment.NewLine);

        var spaces = new List<GameBoardSpace>();
        
        for (var rowNumber = 0; rowNumber < rows.Length; rowNumber++)
        {
            var row = rows[rowNumber];

            var values = row.Split(' ');

            for (var columnNumber = 0; columnNumber < values.Length; columnNumber++)
            {
                int? value = values[columnNumber].Equals("x", StringComparison.CurrentCultureIgnoreCase)
                    ? null
                    : int.Parse(values[columnNumber]);
                
                var space = new GameBoardSpace(rowNumber, columnNumber, value);
                spaces.Add(space);
            }
        }

        return new GameBoard(spaces.ToArray());
    }
}