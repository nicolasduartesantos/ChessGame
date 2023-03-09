using chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using table;

namespace Chess
{
    class Screen {

        public static void printMatch(ChessMatch match) {
            printTable(match.table);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.turn);
            if (!match.finished) {
                Console.WriteLine("Awaiting move by: " + match.currentPlayer);
                if (match.check) {
                    Console.WriteLine("CHECK!");
                }
            }
            else {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch match) {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            printSet(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux   ;
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> set) {
            Console.Write("[");
            foreach(Piece x in set) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void printTable(Table table) {
            for (int i = 0; i < table.lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < table.columns; j++) {
                    printPiece(table.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printTable(Table table, bool[,] possiblePositions) {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < table.lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < table.columns; j++) {
                    if (possiblePositions[i, j]) { 
                        Console.BackgroundColor = alteredBackground;
                    }
                    else {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(table.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readChessPosition() {
            String s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece) { 
            if (piece == null) {
                Console.Write("- ");
            }
            else {
                if (piece.color == Color.White) {
                    Console.Write(piece);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
            
        }
    }
}
