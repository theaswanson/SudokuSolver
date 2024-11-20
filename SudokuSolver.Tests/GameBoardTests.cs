using FluentAssertions;
using SudokuSolver.Core;

namespace SudokuSolver.Tests;

[TestFixture]
public class GameBoardTests
{
    [Test]
    public void GivenNullBoardSpaces_ThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => _ = new GameBoard(null));
    }
    
    [TestCase(80)]
    [TestCase(82)]
    public void GivenInvalidNumberOfBoardSpaces_ThrowsException(int invalidNumberOfSpaces)
    {
        var spaces = Enumerable.Range(0, invalidNumberOfSpaces).Select(i => new GameBoardSpace(Math.Min(i / 9, 8), Math.Min(i % 9, 8), null)).ToArray();
        var exception = Assert.Throws<ArgumentException>(() => _ = new GameBoard(spaces));
        
        exception.Message.Should().Be($"Invalid number of board spaces. 81 are required, got {invalidNumberOfSpaces}.");
    }
    
    [Test]
    public void GivenDuplicateSpaces_ThrowsException()
    {
        var spaces = new List<GameBoardSpace>();

        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                spaces.Add(new GameBoardSpace(i, i, null));
            }
        }
        
        var exception = Assert.Throws<ArgumentException>(() => _ = new GameBoard(spaces.ToArray()));
        
        exception.Message.Should().Be("Duplicate spaces detected. Found 9.");
    }
    
    [Test]
    public void IsSolved_GivenValidGameBoard_ReturnsTrue()
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
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void IsSolved_GivenInvalidGameBoard_ReturnsTrue()
    {
        var gameBoardString =
            """
            1 3 5 2 6 9 7 8 1
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
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void IsSolved_GivenBoardWithEmptySpace_ReturnsFalse()
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

        var result = gameBoard.IsSolved();
        
        Assert.That(result, Is.False);
    }
}