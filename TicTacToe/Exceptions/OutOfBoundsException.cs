namespace TicTacToe.Exceptions;

public class OutOfBoundsException : Exception
{
    public OutOfBoundsException(int x, int y) : base(
        $"Invalid Move. {x},{y} is out of bounds.\n")
    {
    }
}
