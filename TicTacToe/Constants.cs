using TicTacToe.Enums;

namespace TicTacToe;

public static class Constants
{
    public const string Placeholder = ".";
    public const string Forfeit = "q";
    public static readonly List<Symbol> Symbols = new() {Symbol.X, Symbol.O};
}
