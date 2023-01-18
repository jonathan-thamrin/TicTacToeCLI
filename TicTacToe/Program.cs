using TicTacToe;
using TicTacToe.Interfaces;
using TicTacToe.IO;

IReader consoleReader = new ConsoleReader();
IWriter consoleWriter = new ConsoleWriter();

var outputMessenger = new OutputMessenger(consoleWriter);
var userInput = new UserInput(consoleReader, consoleWriter);
var game = new Game(outputMessenger, userInput);

game.Start();
