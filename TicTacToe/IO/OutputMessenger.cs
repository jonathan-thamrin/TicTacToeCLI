using System.Text;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.IO;

public class OutputMessenger
{
    private IWriter writer;

    public OutputMessenger(IWriter writer)
    {
        this.writer = writer;
    }

    public void DisplayGrid(Grid grid)
    {
        var gridText = new StringBuilder("\n\n");
        var rowColSize = grid.Ydim();

        for (var row = 0; row < rowColSize; row++)
        {
            for (var col = 0; col < rowColSize; col++)
            {
                gridText.Append($"{grid.Cells()[row, col]}");
                if (col < rowColSize - 1) gridText.Append("   ");
            }

            gridText.Append('\n');
        }

        writer.Write(gridText.ToString());
    }

    public void DisplayException(Exception e)
    {
        writer.Write(e.Message);
    }

    public void DisplayWelcome()
    {
        writer.Write("\nWelcome to Tic Tac Toe.");
    }

    public void DisplayMoveAccepted()
    {
        writer.Write("\nMove accepted. The board has been updated:");
    }

    public void DisplayTie()
    {
        writer.Write("\nThe game ended in a tie.");
    }

    public void DisplayWinner(Player player)
    {
        writer.Write($"\nThe winner is Player {player.Id}!");
    }
}
