using TicTacToe.Enums;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.Models;

public class GridTest
{
    [Fact]
    public void GivenANewGrid_ThenOnlyContainsPlaceholders()
    {
        // Arrange
        var expectedNoOfPlaceholders = 9;
        var grid = new Grid(3, 3);

        // Act
        var actualNoOfPlaceholders = 0;

        foreach (var cell in grid.Cells())
            if (cell == Constants.Placeholder)
                actualNoOfPlaceholders++;

        // Assert
        Assert.Equal(expectedNoOfPlaceholders, actualNoOfPlaceholders);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 1)]
    [InlineData(2, 3)]
    public void GivenAGrid_WhenSymbolPlacedAtSpecificCoordinate_ThenCorrespondingCellIsUpdated(int x, int y)
    {
        // Arrange
        var expectedSymbol = Symbol.X;
        var grid = new Grid(3, 3);

        // Act
        grid.PlaceSymbol(new Coordinate(x,y), expectedSymbol);
        var actualSymbol = grid.Cells()[y - 1, x - 1];

        // Assert
        Assert.Equal(expectedSymbol.ToString(), actualSymbol);
    }

    [Fact]
    public void GivenAGrid_WhenFullyOccupiedBySymbols_ThenShowGridIsFull()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(new Coordinate(1,1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(1,2), Symbol.O);
        grid.PlaceSymbol(new Coordinate(1,3), Symbol.X);
        grid.PlaceSymbol(new Coordinate(2,1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(2,2), Symbol.O);
        grid.PlaceSymbol(new Coordinate(2,3), Symbol.O);
        grid.PlaceSymbol(new Coordinate(3,1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(3,2), Symbol.O);
        grid.PlaceSymbol(new Coordinate(3,3), Symbol.X);

        // Act
        var isFull = grid.IsFull();

        // Assert
        Assert.True(isFull);
    }

    [Fact]
    public void GivenAGrid_WhenPartiallyOccupiedBySymbols_ThenShowGridIsNotFull()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(new Coordinate(1,1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(2,2), Symbol.X);
        grid.PlaceSymbol(new Coordinate(3,2), Symbol.X);

        // Act
        var isFull = grid.IsFull();

        // Assert
        Assert.False(isFull);
    }
}
