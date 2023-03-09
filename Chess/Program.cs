using System;
using table;
using chess;

namespace Chess
{
    class Program {
        static void Main(string[] args) {

            try {
                ChessMatch match = new ChessMatch();

                while (!match.finished) {
                    try {
                        Console.Clear();
                        Screen.printMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.readChessPosition().toPosition();
                        match.validateOrigin(origin);

                        bool[,] possiblePositions = match.table.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printTable(match.table, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.readChessPosition().toPosition();
                        match.validateDestination(origin, destination);

                        match.doPlay(origin, destination);
                    }
                    catch (TableException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(match);
            }
            catch (TableException e) { 
                Console.WriteLine(e.Message);
            }
        }
    }
}