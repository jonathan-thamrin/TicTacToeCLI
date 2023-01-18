using Moq;
using TicTacToe.Enums;
using TicTacToe.Exceptions;
using TicTacToe.Interfaces;
using TicTacToe.IO;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.IO;

public class OutputMessengerTest
{
    private Mock<IWriter> writerMock;
    private OutputMessenger outputMessenger;

    public OutputMessengerTest()
    {
        writerMock = new Mock<IWriter>();
        outputMessenger = new OutputMessenger(writerMock.Object);
    }

    [Fact]
    public void GivenAnOutputMessenger_ThenDisplayTheWelcomeMessage()
    {
        // Act
        outputMessenger.DisplayWelcome();

        // Assert
        writerMock.Verify(writer => writer.Write("\nWelcome to Tic Tac Toe."));
    }

    [Fact]
    public void GivenAnOutputMessenger_ThenDisplayMoveWasAccepted()
    {
        // Act
        outputMessenger.DisplayMoveAccepted();

        // Assert
        writerMock.Verify(writer => writer.Write("\nMove accepted. The board has been updated:"));
    }

    [Fact]
    public void GivenAnOutputMessenger_ThenDisplayGameEndedWithATie()
    {
        // Act
        outputMessenger.DisplayTie();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe game ended in a tie."));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void GivenAPlayerAndOutputMessenger_ThenDisplayPlayerAsWinner(int playerId)
    {
        // Arrange
        var player = new Player(playerId);

        // Act
        outputMessenger.DisplayWinner(player);

        // Assert
        writerMock.Verify(writer => writer.Write($"\nThe winner is Player {playerId}!"));
    }

    [Fact]
    public void GivenAUserInput_WhenPlayerInputsCoordinateOutsideOfGrid_ThenDisplayMoveWasOutOfBounds()
    {
        // Arrange
        var player = new Player(1);
        var grid = new Grid(3, 3);
        var readerMock = new Mock<IReader>();
        var userInput = new UserInput(readerMock.Object, writerMock.Object);
        readerMock.Setup(reader => reader.ReadLine()).Returns("8,8");

        // Act
        outputMessenger.DisplayException(
            Assert.Throws<OutOfBoundsException>(() => userInput.GetPlayersMove(player, grid)));

        // Assert
        writerMock.Verify(writer => writer.Write("Invalid Move. 8,8 is out of bounds.\n"));
    }
    
    [Fact]
    public void GivenAUserInput_WhenUserSelectsOccupiedSpace_ThenDisplayMoveChosenWasOccupiedSpace()
    {
        // Arrange
        var player = new Player(1);
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(new Coordinate(1,1), Symbol.X);
        var readerMock = new Mock<IReader>();
        var userInput = new UserInput(readerMock.Object, writerMock.Object);
        readerMock.Setup(reader => reader.ReadLine()).Returns("1,1");

        // Act
        outputMessenger.DisplayException(
            Assert.Throws<OccupiedSpaceException>(() => userInput.GetPlayersMove(player, grid)));

        // Assert
        writerMock.Verify(writer => writer.Write("Invalid Move. 1,1 is already occupied.\n"));
    }

    [Fact]
    public void
        GivenAGridAndOutputMessenger_WhenSomeSymbolsPresentOnGrid_ThenDisplayGridContainingSymbolsAndPlaceholders()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.PlaceSymbol(new Coordinate(1, 1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(1, 2), Symbol.O);
        grid.PlaceSymbol(new Coordinate(2, 1), Symbol.O);
        grid.PlaceSymbol(new Coordinate(2, 3), Symbol.O);
        grid.PlaceSymbol(new Coordinate(3, 1), Symbol.X);
        grid.PlaceSymbol(new Coordinate(3, 2), Symbol.X);

        // Act
        outputMessenger.DisplayGrid(grid);

        // Assert
        writerMock.Verify(writer => writer.Write("\n\nX   O   X\nO   .   X\n.   O   .\n"));
    }
}
