using TicTacToe.Enums;

namespace TicTacToe.Models;

public class Player
{
    public int Id { get; }
    public Symbol Symbol { get; }

    public Player(int id)
    {
        Id = id;
        Symbol = Constants.Symbols[id - 1];
    }
}
