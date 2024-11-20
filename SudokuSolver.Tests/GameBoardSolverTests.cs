using FluentAssertions;
using SudokuSolver.Core;

namespace SudokuSolver.Tests;

[TestFixture]
public class GameBoardSolverTests
{
    [Test]
    public void GivenBoardThatIsAlreadySolved_ReturnsTrue()
    {
        var gameBoardString =
            """
            4 3 5 2 6 9 7 8 1
            6 8 2 5 7 1 4 9 3
            1 9 7 8 3 4 5 6 2
            8 2 6 1 9 5 3 4 7
            3 7 4 6 8 2 9 1 5
            9 5 1 7 4 3 6 2 8
            5 1 9 3 2 6 8 7 4
            2 4 8 9 5 7 1 3 6
            7 6 3 4 1 8 2 5 9
            """;
        
        var gameBoard = new GameBoardFactory().FromString(gameBoardString);

        var result = GameBoardSolver.Solve(gameBoard);

        result.Should().BeSameAs(gameBoard);
    }
    
    [Test]
    public void GivenBoardWithOneEmptySpace_SolvesIt()
    {
        var gameBoardString =
            """
            x 3 5 2 6 9 7 8 1
            6 8 2 5 7 1 4 9 3
            1 9 7 8 3 4 5 6 2
            8 2 6 1 9 5 3 4 7
            3 7 4 6 8 2 9 1 5
            9 5 1 7 4 3 6 2 8
            5 1 9 3 2 6 8 7 4
            2 4 8 9 5 7 1 3 6
            7 6 3 4 1 8 2 5 9
            """;
        
        var gameBoard = new GameBoardFactory().FromString(gameBoardString);

        var result = GameBoardSolver.Solve(gameBoard);

        result.IsSolved().Should().BeTrue();
        result.GetSpace(0, 0).Value.Should().Be(4);
    }
    
    [Test]
    public void GivenSimplePuzzle_SolvesIt()
    {
        var gameBoardString =
            """
            x 4 x x 2 x 8 6 5
            7 x x 6 x 8 x x x
            1 x x x x 4 7 x 2
            x 1 8 7 4 x x x x
            x x 5 2 x 9 6 x x
            x x x x 8 6 1 5 x
            9 x 1 5 x x x x 6
            x x x 8 x 2 x x 7
            8 7 3 x 6 x x 2 x
            """;
        
        var gameBoard = new GameBoardFactory().FromString(gameBoardString);
        
        var expectedSolvedGameBoardString =
            """
            3 4 9 1 2 7 8 6 5
            7 5 2 6 3 8 9 4 1
            1 8 6 9 5 4 7 3 2
            6 1 8 7 4 5 2 9 3
            4 3 5 2 1 9 6 7 8
            2 9 7 3 8 6 1 5 4
            9 2 1 5 7 3 4 8 6
            5 6 4 8 9 2 3 1 7
            8 7 3 4 6 1 5 2 9
            """; 
        
        var expectedSolvedGameBoard = new GameBoardFactory().FromString(expectedSolvedGameBoardString);

        var result = GameBoardSolver.Solve(gameBoard);

        result.IsSolved().Should().BeTrue();

        result.BoardSpaces.Should().BeEquivalentTo(expectedSolvedGameBoard.BoardSpaces);
    }
    
    [Test]
    public void GivenSimplePuzzle2_SolvesIt()
    {
        var gameBoardString =
            """
            x 3 2 x x x 5 x 7
            7 x 5 2 1 8 x x x
            x x 6 x x 3 x x 4
            3 x 8 x 6 2 x 4 5
            x x 9 1 8 x x 7 x
            x x x x x 4 2 9 x
            x 8 4 x 3 1 x x x
            2 x 7 x 4 9 x x x
            x 1 x x x 7 4 5 9
            """;
        
        var gameBoard = new GameBoardFactory().FromString(gameBoardString);
        
        var expectedSolvedGameBoardString =
            """
            8 3 2 4 9 6 5 1 7
            7 4 5 2 1 8 9 3 6
            1 9 6 7 5 3 8 2 4
            3 7 8 9 6 2 1 4 5
            4 2 9 1 8 5 6 7 3
            5 6 1 3 7 4 2 9 8
            9 8 4 5 3 1 7 6 2
            2 5 7 6 4 9 3 8 1
            6 1 3 8 2 7 4 5 9
            """; 
        
        var expectedSolvedGameBoard = new GameBoardFactory().FromString(expectedSolvedGameBoardString);
        
        var result = GameBoardSolver.Solve(gameBoard);

        result.IsSolved().Should().BeTrue();
        
        result.BoardSpaces.Should().BeEquivalentTo(expectedSolvedGameBoard.BoardSpaces);
    }
    
    [Test]
    [Ignore("Still improving the solving algorithm.")]
    public void GivenHardPuzzle_SolvesIt()
    {
        // https://www.reddit.com/r/sudoku/comments/uadzbp/very_hard_sudoku_puzzle/
        var gameBoardString =
            """
            x x x 9 x x 1 x 2
            7 x x 3 x x 6 x x
            x x 2 x x x x 3 x
            9 x x x x 8 7 x x
            3 x x x 1 x x x 9
            x x 6 5 x x x x 1
            x 1 x x x x 4 x x
            x x 4 x x 9 x x 6
            8 x 3 x x 6 x x x
            """;
        
        var gameBoard = new GameBoardFactory().FromString(gameBoardString);
        
        var result = GameBoardSolver.Solve(gameBoard);

        result.IsSolved().Should().BeTrue();
    }
}