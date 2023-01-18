using Moq;
using TicTacToe.Interfaces;
using TicTacToe.IO;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.Models;

public class GameTest
{
    private Game game;
    private Mock<IWriter> writerMock;
    private Mock<IReader> readerMock;

    public GameTest()
    {
        writerMock = new Mock<IWriter>();
        readerMock = new Mock<IReader>();
        game = new Game(
            new OutputMessenger(writerMock.Object),
            new UserInput(readerMock.Object, writerMock.Object)
        );
    }

    [Fact]
    public void GivenAGame_WhenFirstPlayerGetsThreeInARow_ThenGameEndsWithFirstPlayerAsWinner()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("1,1")
            .Returns("3,1")
            .Returns("1,2")
            .Returns("2,1")
            .Returns("1,3");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe winner is Player 1!"));
    }

    [Fact]
    public void GivenAGame_WhenSecondPlayerGetsThreeInARow_ThenGameEndsWithSecondPlayerAsWinner()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("3,1")
            .Returns("1,1")
            .Returns("2,1")
            .Returns("1,2")
            .Returns("3,2")
            .Returns("1,3");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe winner is Player 2!"));
    }

    [Fact]
    public void GivenAGame_WhenFirstPlayerForfeits_ThenGameEndsWithSecondPlayerAsWinner()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("q");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe winner is Player 2!"));
    }

    [Fact]
    public void GivenAGame_WhenSecondPlayerForfeits_ThenGameEndsWithFirstPlayerAsWinner()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("1,1")
            .Returns("q");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe winner is Player 1!"));
    }

    [Fact]
    public void GivenAGame_WhenBothPlayersDontGetThreeInARow_ThenGameEndsWithATie()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("1,1")
            .Returns("3,1")
            .Returns("2,1")
            .Returns("2,2")
            .Returns("1,3")
            .Returns("1,2")
            .Returns("2,3")
            .Returns("3,3")
            .Returns("3,2");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("\nThe game ended in a tie."));
    }

    [Fact]
    public void GivenAGame_WhenPlayerChoosesOccupiedSpace_ThenShowSpaceIsOccupiedAndPromptForNewMoveThenUpdateGrid()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("1,1")
            .Returns("1,1")
            .Returns("1,2")
            .Returns("q");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("Invalid Move. 1,1 is already occupied.\n"));
        writerMock.Verify(writer => writer.Write("\nPlayer 2 - Enter your co-ordinates x,y to place your O or 'q' to forfeit: "));
        writerMock.Verify(writer => writer.Write("\nMove accepted. The board has been updated:"));
    }

    [Fact]
    public void GivenAGame_WhenPlayerChoosesSpaceOutsideGrid_ThenShowSelectionIsOutOfBoundsAndPromptForNewMoveThenUpdateGrid()
    {
        // Arrange
        readerMock.SetupSequence(reader => reader.ReadLine())
            .Returns("9,3")
            .Returns("3,3")
            .Returns("q");

        // Act
        game.Start();

        // Assert
        writerMock.Verify(writer => writer.Write("Invalid Move. 9,3 is out of bounds.\n"));
        writerMock.Verify(writer => writer.Write("\nPlayer 1 - Enter your co-ordinates x,y to place your X or 'q' to forfeit: "));
        writerMock.Verify(writer => writer.Write("\nMove accepted. The board has been updated:"));
    }
}
