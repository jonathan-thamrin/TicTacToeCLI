using TicTacToe.Exceptions;
using TicTacToe.IO;
using TicTacToe.Models;

namespace TicTacToe;

public class Game
{
    private bool gameEnded;
    private Grid grid;
    private UserInput userInput;
    private TurnAllocator turnAllocator;
    private OutputMessenger outputMessenger;

    public Game(OutputMessenger outputMessenger, UserInput userInput)
    {
        grid = new Grid(3, 3);
        gameEnded = false;
        turnAllocator = new TurnAllocator();
        this.userInput = userInput;
        this.outputMessenger = outputMessenger;
    }

    public void Start()
    {
        var currentPlayer = turnAllocator.Players.First();

        outputMessenger.DisplayWelcome();
        outputMessenger.DisplayGrid(grid);

        while (!gameEnded)
        {
            while (true)
            {
                try
                {
                    var coordinate = userInput.GetPlayersMove(currentPlayer, grid);

                    if (coordinate.Forfeit)
                    {
                        outputMessenger.DisplayWinner(turnAllocator.NextPlayerAfter(currentPlayer));
                        return;
                    }

                    grid.PlaceSymbol(coordinate, currentPlayer.Symbol);
                    outputMessenger.DisplayMoveAccepted();
                    outputMessenger.DisplayGrid(grid);
                }
                catch (OccupiedSpaceException occupied)
                {
                    outputMessenger.DisplayException(occupied);
                    continue;
                }
                catch (OutOfBoundsException outOfBounds)
                {
                    outputMessenger.DisplayException(outOfBounds);
                    continue;
                }

                break;
            }

            if (PatternDetector.GridHasWinner(grid))
            {
                var winningSymbol = PatternDetector.GetWinningSymbol(grid);
                var winner = turnAllocator.Players.Find(player =>
                    player.Symbol.ToString() == winningSymbol
                );
                outputMessenger.DisplayWinner(winner);
                gameEnded = true;
            }

            currentPlayer = turnAllocator.NextPlayerAfter(currentPlayer);

            if (grid.IsFull())
            {
                outputMessenger.DisplayTie();
                gameEnded = true;
            }
        }
    }
}
