using System;
using TicTacToe.Enums;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Test.Models;

public class PlayerTest
{
    [Theory]
    [InlineData(1, Symbol.X)]
    [InlineData(2, Symbol.O)]
    public void GivenAPlayer_WhenIdIsOneOrTwo_ThenPlayerHasRespectiveSymbol(int id, Symbol expectedSymbol)
    {
        // Arrange
        var player = new Player(id);

        // Assert
        Assert.Equal(expectedSymbol, player.Symbol);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GivenAPlayer_WhenIdIsGreaterThanTwo_ThenThrowIndexException(int id)
    {
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(id));
    }
}
