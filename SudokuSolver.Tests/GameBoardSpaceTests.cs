using FluentAssertions;
using SudokuSolver.Core;

namespace SudokuSolver.Tests;

[TestFixture]
public class GameBoardSpaceTests
{
    [TestCase(-1)]
    [TestCase(9)]
    public void GivenInvalidRow_ThrowsException(int invalidRow)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _ = new GameBoardSpace(invalidRow, default, default));

        exception.ParamName.Should().Be("row");
    }
    
    [Test]
    public void GivenValidRow_AndInvalidColumn_ThrowsException(
        [Values(0)] int validRow,
        [Values(-1, 9)] int invalidColumn)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _ = new GameBoardSpace(validRow, invalidColumn, default));

        exception.ParamName.Should().Be("column");
    }
    
    [Test]
    public void GivenValidRow_AndValidColumn_AndInvalidValue_ThrowsException(
        [Values(0)] int validRow,
        [Values(0)] int validColumn,
        [Values(0, 10)] int invalidValue)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _ = new GameBoardSpace(validRow, validColumn, invalidValue));

        exception.ParamName.Should().Be("value");
    }
    
    [Test]
    public void GivenValidArgs_DoesNotThrow(
        [Values(0, 1, 2, 3, 4, 5, 6, 7, 8)] int validRow,
        [Values(0, 1, 2, 3, 4, 5, 6, 7, 8)] int validColumn,
        [Values(1, 2, 3, 4, 5, 6, 7, 8, 9)] int validValue)
    {
        Assert.DoesNotThrow(() => _ = new GameBoardSpace(validRow, validColumn, validValue));
    }
}