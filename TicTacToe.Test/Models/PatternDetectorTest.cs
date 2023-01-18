using System.Collections.Generic;
using TicTacToe.Enums;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.Models;

public class PatternDetectorTest
{
    [Fact]
    public void GivenAGridAndPatternDetector_WhenNoSymbolsInARow_ThenNoWinningSymbolsExists()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(new Coordinate(1, 1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(1, 2), Symbol.X);
        grid.PlaceSymbol(new Coordinate(1, 3), Symbol.O);
        grid.PlaceSymbol(new Coordinate(2, 1), Symbol.O);
        grid.PlaceSymbol(new Coordinate(2, 2), Symbol.X);
        grid.PlaceSymbol(new Coordinate(2, 3), Symbol.X);
        grid.PlaceSymbol(new Coordinate(3, 1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(3, 2), Symbol.O);
        grid.PlaceSymbol(new Coordinate(3, 3), Symbol.O);

        // Act
        var actualWinningSymbol = PatternDetector.GetWinningSymbol(grid);

        // Assert
        Assert.True(string.IsNullOrEmpty(actualWinningSymbol));
    }

    [Theory]
    [MemberData(nameof(GetCoordinates))]
    public void GivenAGridAndPatternDetector_WhenThereAreSymbolsInARow_ThenWinningSymbolShouldExist(Coordinate posOne,
        Coordinate posTwo, Coordinate posThree, Symbol expectedWinningSymbol)
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(posOne, expectedWinningSymbol);
        grid.PlaceSymbol(posTwo, expectedWinningSymbol);
        grid.PlaceSymbol(posThree, expectedWinningSymbol);

        // Act
        var actualWinningSymbol = PatternDetector.GetWinningSymbol(grid);

        // Assert
        Assert.Equal(expectedWinningSymbol.ToString(), actualWinningSymbol);
    }

    public static IEnumerable<object[]> GetCoordinates =>
        new List<object[]>
        {
            new object[] {new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(3, 1), Symbol.X},
            new object[] {new Coordinate(1, 2), new Coordinate(2, 2), new Coordinate(3, 2), Symbol.X},
            new object[] {new Coordinate(1, 3), new Coordinate(2, 3), new Coordinate(3, 3), Symbol.X},
            new object[] {new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(3, 1), Symbol.O},
            new object[] {new Coordinate(1, 2), new Coordinate(2, 2), new Coordinate(3, 2), Symbol.O},
            new object[] {new Coordinate(1, 3), new Coordinate(2, 3), new Coordinate(3, 3), Symbol.O},

            new object[] {new Coordinate(1, 1), new Coordinate(1, 2), new Coordinate(1, 3), Symbol.X},
            new object[] {new Coordinate(2, 1), new Coordinate(2, 2), new Coordinate(2, 3), Symbol.X},
            new object[] {new Coordinate(3, 1), new Coordinate(3, 2), new Coordinate(3, 3), Symbol.X},
            new object[] {new Coordinate(1, 1), new Coordinate(1, 2), new Coordinate(1, 3), Symbol.O},
            new object[] {new Coordinate(2, 1), new Coordinate(2, 2), new Coordinate(2, 3), Symbol.O},
            new object[] {new Coordinate(3, 1), new Coordinate(3, 2), new Coordinate(3, 3), Symbol.O},

            new object[] {new Coordinate(1, 1), new Coordinate(2, 2), new Coordinate(3, 3), Symbol.X},
            new object[] {new Coordinate(3, 1), new Coordinate(2, 2), new Coordinate(1, 3), Symbol.X},
            new object[] {new Coordinate(1, 1), new Coordinate(2, 2), new Coordinate(3, 3), Symbol.O},
            new object[] {new Coordinate(3, 1), new Coordinate(2, 2), new Coordinate(1, 3), Symbol.O},
        };
}
