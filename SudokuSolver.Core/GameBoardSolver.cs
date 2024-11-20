namespace SudokuSolver.Core;

public class GameBoardSolver
{
    private readonly int[] _possibleValues = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    private int[] GetValues(GameBoardSpace[] spaces) =>
        spaces.Where(space => space.Value.HasValue)
            .Select(space => space.Value!.Value)
            .Distinct()
            .ToArray();

    public GameBoard Solve(GameBoard board)
    {
        if (board.IsSolved())
        {
            return board;
        }

        var possibleSolutions = new Dictionary<GameBoardSpace, int[]>();

        while (!board.IsSolved())
        {
            var solvesThisRound = 0;
            
            foreach (var unsolvedSpace in board.GetUnsolvedSpaces())
            {
                var hasPossibleSolutionsFromLastSolve = possibleSolutions.TryGetValue(unsolvedSpace, out var values);

                if (hasPossibleSolutionsFromLastSolve && values!.Length == 1)
                {
                    solvesThisRound = SolveSpace(unsolvedSpace, values.Single(), solvesThisRound);

                    continue;
                }
                
                var (row, column) = (unsolvedSpace.Row, unsolvedSpace.Column);

                var rowValues = GetValues(board.GetSpacesForRow(row));

                if (rowValues.Length == 8)
                {
                    var value = _possibleValues.Except(rowValues).Single();

                    solvesThisRound = SolveSpace(unsolvedSpace, value, solvesThisRound);

                    continue;
                }

                var columnValues = GetValues(board.GetSpacesForColumn(column));

                if (columnValues.Length == 8)
                {
                    var value = _possibleValues.Except(columnValues).Single();

                    solvesThisRound = SolveSpace(unsolvedSpace, value, solvesThisRound);

                    continue;
                }

                var boxValues = GetValues(board.GetSpacesForBox(board.GetBoxNumber(row, column)));

                if (boxValues.Length == 8)
                {
                    var value = _possibleValues.Except(boxValues).Single();

                    solvesThisRound = SolveSpace(unsolvedSpace, value, solvesThisRound);

                    continue;
                }
                
                var possibleValues = _possibleValues
                    .Except(rowValues)
                    .Except(columnValues)
                    .Except(boxValues)
                    .ToArray();

                if (possibleValues.Length == 1)
                {
                    solvesThisRound = SolveSpace(unsolvedSpace, possibleValues.Single(), solvesThisRound);

                    continue;
                }
                
                possibleSolutions.TryAdd(unsolvedSpace, possibleValues);
            }

            if (solvesThisRound == 0)
            {
                throw new Exception("This board is too difficult to solve!");
            }
        }

        return board;

        int SolveSpace(GameBoardSpace unsolvedSpace, int value, int solvesThisRound)
        {
            unsolvedSpace.SetValue(value);
            solvesThisRound++;
            return solvesThisRound;
        }
    }
}