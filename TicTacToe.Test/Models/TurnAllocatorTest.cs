using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.Models;

public class TurnAllocatorTest
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void GivenNewTurnAllocator_WhenGettingNextPlayerAfterCurrentPlayer_ThenNextPlayerIsOpposingPlayer(
        int currentPlayerId, int expectedNextPlayerId)
    {
        // Arrange
        var currentPlayer = new Player(currentPlayerId);
        var turnAllocator = new TurnAllocator();

        // Act
        var actualNextPlayer = turnAllocator.NextPlayerAfter(currentPlayer);

        // Assert
        Assert.Equal(expectedNextPlayerId, actualNextPlayer.Id);
    }
}
