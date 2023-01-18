using TicTacToe.Interfaces;

namespace TicTacToe.IO;

public class ConsoleWriter : IWriter
{
    public void Write(string message)
    {
        Console.Write(message);
    }
}
