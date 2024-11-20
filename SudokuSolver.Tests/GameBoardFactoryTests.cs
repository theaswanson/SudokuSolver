using FluentAssertions;
using SudokuSolver.Core;

namespace SudokuSolver.Tests;

[TestFixture]
public class GameBoardFactoryTests
{
    [Test]
    public void GivenValidBoardString_DoesNotThrow()
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

        var result = gameBoard.IsSolved();

        result.Should().BeTrue();
    }
    
    [TestCase("x")]
    [TestCase("X")]
    public void GivenSpaceDenotedWithX_UsesNullForValue(string x)
    {
        var gameBoardString =
            $"""
            {x} 3 5 2 6 9 7 8 1
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

        var space = gameBoard.GetSpace(0, 0);

        space.Value.Should().BeNull();
    }
}