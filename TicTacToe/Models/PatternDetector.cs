using System.Text;
using System.Text.RegularExpressions;
using TicTacToe.Enums;

namespace TicTacToe.Models;

public static class PatternDetector
{
    public static bool GridHasWinner(Grid grid)
    {
        return !string.IsNullOrEmpty(GetWinningSymbolHorizontalOrVertical(grid)) ||
               !string.IsNullOrEmpty(GetWinningSymbolDiagonal(grid));
    }
    
    public static string GetWinningSymbol(Grid grid)
    {
        var winningSymbol = new StringBuilder();

        winningSymbol.Append(GetWinningSymbolHorizontalOrVertical(grid));
        winningSymbol.Append(GetWinningSymbolDiagonal(grid));

        return winningSymbol.ToString();
    }

    private static string GetWinningSymbolHorizontalOrVertical(Grid grid)
    {
        var inspectedRows = new StringBuilder();
        var inspectedCols = new StringBuilder();

        for (var i = 0; i < grid.Ydim(); i++)
        {
            for (var j = 0; j < grid.Xdim(); j++)
            {
                inspectedRows.Append(grid.Cells()[i, j]);
                inspectedCols.Append(grid.Cells()[j, i]);
            }

            var inspectedRowsString = inspectedRows.ToString();
            var inspectedColsString = inspectedCols.ToString();

            if (ContainsSymbolsInARow(inspectedRowsString))
                return inspectedRowsString.First().ToString();

            if (ContainsSymbolsInARow(inspectedColsString))
                return inspectedColsString.First().ToString();

            inspectedRows.Clear();
            inspectedCols.Clear();
        }

        return "";
    }

    private static string GetWinningSymbolDiagonal(Grid grid)
    {
        var downwardDiagonal = new StringBuilder();
        var upwardDiagonal = new StringBuilder();

        for (var i = 0; i < grid.Ydim(); i++)
        {
            downwardDiagonal.Append(grid.Cells()[i, i]);
            upwardDiagonal.Append(grid.Cells()[i, grid.Ydim() - 1 - i]);
        }

        var upwardDiagonalString = upwardDiagonal.ToString();
        var downwardDiagonalString = downwardDiagonal.ToString();

        if (ContainsSymbolsInARow(upwardDiagonalString))
            return upwardDiagonalString.First().ToString();

        if (ContainsSymbolsInARow(downwardDiagonalString))
            return downwardDiagonalString.First().ToString();

        return "";
    }

    private static bool ContainsSymbolsInARow(string row)
    {
        return Regex.IsMatch(row, $@"^{Symbol.X}{{3,}}$|^{Symbol.O}{{3,}}$",
            RegexOptions.IgnoreCase);
    }
}
