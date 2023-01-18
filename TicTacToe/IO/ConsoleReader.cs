using TicTacToe.Interfaces;

namespace TicTacToe.IO;

public class ConsoleReader : IReader
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}
