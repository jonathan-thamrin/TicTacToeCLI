namespace TicTacToe.Models;

public class TurnAllocator
{
    public List<Player> Players { get; }
    
    public TurnAllocator()
    {
        Players = new List<Player> {new(1), new(2)};
    }

    public Player NextPlayerAfter(Player currentPlayer)
    {
        return currentPlayer.Id % 2 == 0 ? Players.First() : Players.Last();
    }
}
