namespace TicTacToe.Exceptions;

public class OccupiedSpaceException : Exception
{
    public OccupiedSpaceException(int x, int y) : base(
        $"Invalid Move. {x},{y} is already occupied.\n")
    {
    }
}
