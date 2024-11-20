namespace SudokuSolver.Core;

public static class GameBoardSolver
{
    public static GameBoard Solve(GameBoard board)
    {
        if (board.IsSolved())
        {
            return board;
        }

        while (!board.IsSolved())
        {
            var solvesThisRound = AttemptToSolveBoard();

            if (solvesThisRound == 0)
            {
                throw new Exception("This board is too difficult to solve!");
            }
        }

        return board;

        int AttemptToSolveBoard()
        {
            var solvesThisRound = 0;

            foreach (var unsolvedSpace in board.GetUnsolvedSpaces())
            {
                var (row, column) = (unsolvedSpace.Row, unsolvedSpace.Column);

                var rowValues = board.GetSpacesForRow(row).GetValues();

                if (rowValues.Length == 8)
                {
                    var value = GameLogic.PossibleValues.Except(rowValues).Single();

                    SolveSpace(unsolvedSpace, value);

                    continue;
                }

                var columnValues = board.GetSpacesForColumn(column).GetValues();

                if (columnValues.Length == 8)
                {
                    var value = GameLogic.PossibleValues.Except(columnValues).Single();

                    SolveSpace(unsolvedSpace, value);

                    continue;
                }

                var boxValues = board.GetSpacesForBox(GameLogic.GetBoxNumber(row, column)).GetValues();

                if (boxValues.Length == 8)
                {
                    var value = GameLogic.PossibleValues.Except(boxValues).Single();

                    SolveSpace(unsolvedSpace, value);

                    continue;
                }
                
                var possibleValues = GameLogic.PossibleValues
                    .Except(rowValues)
                    .Except(columnValues)
                    .Except(boxValues)
                    .ToArray();

                if (possibleValues.Length == 1)
                {
                    SolveSpace(unsolvedSpace, possibleValues.Single());

                    continue;
                }
            }

            return solvesThisRound;
            
            void SolveSpace(GameBoardSpace unsolvedSpace, int value)
            {
                unsolvedSpace.SetValue(value);
                solvesThisRound++;
            }
        }
    }
}