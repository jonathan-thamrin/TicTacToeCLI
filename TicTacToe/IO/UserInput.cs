using System.Text.RegularExpressions;
using TicTacToe.Exceptions;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.IO;

public class UserInput
{
    private IReader reader;
    private IWriter writer;

    public UserInput(IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    public Coordinate GetPlayersMove(Player player, Grid grid)
    {
        writer.Write($"\nPlayer {player.Id} - Enter your co-ordinates x,y to place your {player.Symbol} or 'q' to forfeit: ");
        var move = reader.ReadLine();

        while (!Regex.IsMatch(move, $@"^[1-9]+,[1-9]+$|^{Constants.Forfeit}"))
        {
            writer.Write("Please enter a valid input in the form of x,y or 'q' to forfeit: ");
            move = reader.ReadLine();
        }

        var forfeit = true;
        return move == Constants.Forfeit ? new Coordinate(forfeit) : ParseMove(move, grid);
    }

    private static Coordinate ParseMove(string move, Grid grid)
    {
        var xy = move.Split(",");
        var x = int.Parse(xy.First());
        var y = int.Parse(xy.Last());

        if (x > grid.Xdim() || y > grid.Ydim())
            throw new OutOfBoundsException(x, y);

        if (grid.Cells()[y - 1, x - 1] != Constants.Placeholder)
            throw new OccupiedSpaceException(x, y);

        return new Coordinate(x, y);
    }
}
