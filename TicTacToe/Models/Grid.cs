using TicTacToe.Enums;

namespace TicTacToe.Models;

public class Grid
{
    private readonly string[,] cells;

    public Grid(int xDim, int yDim)
    {
        cells = new string[yDim, xDim];
        Initialise(xDim, yDim);
    }

    private void Initialise(int xDim, int yDim)
    {
        for (var i = 0; i < xDim; i++)
        for (var j = 0; j < yDim; j++)
            cells[i, j] = Constants.Placeholder;
    }

    public string[,] Cells()
    {
        return cells;
    }

    public int Xdim()
    {
        return cells.GetLength(1);
    }

    public int Ydim()
    {
        return cells.GetLength(0);
    }

    public bool IsFull()
    {
        foreach (var cell in cells)
            if (cell == Constants.Placeholder)
                return false;

        return true;
    }

    public void PlaceSymbol(Coordinate pos, Symbol symbol)
    {
        cells[pos.Y - 1, pos.X - 1] = symbol.ToString();
    }
}
