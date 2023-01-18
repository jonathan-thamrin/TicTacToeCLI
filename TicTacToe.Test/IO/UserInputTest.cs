using Moq;
using TicTacToe.Enums;
using TicTacToe.Exceptions;
using TicTacToe.Interfaces;
using TicTacToe.IO;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.IO;

public class UserInputTest
{
    private Mock<IWriter> writerMock;
    private Mock<IReader> readerMock;
    private UserInput userInput;
    private Grid grid;

    public UserInputTest()
    {
        writerMock = new Mock<IWriter>();
        readerMock = new Mock<IReader>();
        userInput = new UserInput(readerMock.Object, writerMock.Object);
        grid = new Grid(3, 3);
    }

    [Fact]
    public void GivenUserPromptedForMove_WhenValidCoordinateIsGiven_ThenCountedAsValidMoveAndNotPromptAgain()
    {
        // Arrange
        var player = new Player(1);
        var expectedCoordinate = new Coordinate(2, 1);
        readerMock.Setup(reader => reader.ReadLine()).Returns("2,1");

        // Act
        var actualCoordinate = userInput.GetPlayersMove(player, grid);

        // Assert
        writerMock.Verify(writer => writer.Write("Please enter a valid input in the form of x,y or 'q' to forfeit: "),
            Times.Never);
        Assert.Equal(expectedCoordinate, actualCoordinate);
    }

    [Fact]
    public void GivenUserPromptedForMove_WhenInvalidInputMade_ThenCountAsInvalidMoveAndPromptAgain()
    {
        // Arrange
        var expectedCoordinate = new Coordinate(3,2);
        var player = new Player(1);
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("a,2")
            .Returns("2,")
            .Returns(",2")
            .Returns("3.2")
            .Returns("3,2");

        // Act
        var actualCoordinate = userInput.GetPlayersMove(player, grid);

        // Assert
        writerMock.Verify(writer => writer.Write("Please enter a valid input in the form of x,y or 'q' to forfeit: "),
            Times.Exactly(4));
        Assert.Equal(expectedCoordinate, actualCoordinate);
    }
    
    [Fact]
    public void GivenAGridAndPlayer_WhenUserSelectsOccupiedSpace_ThenThrowOccupiedSpaceException()
    {
        // Arrange
        var player = new Player(1);
        grid.PlaceSymbol(new Coordinate(1,1), Symbol.O);
        readerMock.Setup(reader => reader.ReadLine()).Returns("1,1");

        // Assert
        Assert.Throws<OccupiedSpaceException>(() => userInput.GetPlayersMove(player, grid));
    }

    [Fact]
    public void GivenAPlayer_WhenUserSelectsCoordinateOutsideOfGrid_ThenThrowOutOfBoundsException()
    {
        // Arrange
        var player = new Player(1);
        readerMock.Setup(reader => reader.ReadLine()).Returns("9,9");

        // Assert
        Assert.Throws<OutOfBoundsException>(() => userInput.GetPlayersMove(player, grid));
    }
}
