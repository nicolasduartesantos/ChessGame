using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess
{
    class ChessMatch
    {

        public Table table { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            table = new Table(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            placePieces();
        }

        public Piece doMovement(Position origin, Position destination)
        {
            Piece p = table.removePiece(origin);
            p.increaseMovQuantity();
            Piece capturedPiece = table.removePiece(destination);
            table.placePiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            //#specialmove short castling
            if (p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinationR = new Position(origin.line, origin.column + 1);
                Piece R = table.removePiece(originR);
                R.increaseMovQuantity();
                table.placePiece(R, destinationR);
            }

            //#specialmove long castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinationR = new Position(origin.line, origin.column - 1);
                Piece R = table.removePiece(originR);
                R.increaseMovQuantity();
                table.placePiece(R, destinationR);
            }

            //#specialmove en passant
            if (p is Pawn) {
                if (origin.column != destination.column && capturedPiece == null) {
                    Position posP;
                    if (p.color == Color.White) {
                        posP = new Position(destination.line + 1, destination.column);
                    }
                    else {
                        posP = new Position(destination.line - 1, destination.column);
                    }
                    capturedPiece = table.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void undoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = table.removePiece(destination);
            p.decreaseMovQuantity();
            if (capturedPiece != null)
            {
                table.placePiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            table.placePiece(p, origin);

            //#specialmove short castling
            if (p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinationR = new Position(destination.line, destination.column + 1);
                Piece R = table.removePiece(destinationR);
                R.decreaseMovQuantity();
                table.placePiece(R, originR);
            }

            //#specialmove long castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinationR = new Position(destination.line, destination.column - 1);
                Piece R = table.removePiece(destinationR);
                R.decreaseMovQuantity();
                table.placePiece(R, originR);
            }

            //#specialmove en passant
            if (p is Pawn) {
                if (origin.column != destination.column && capturedPiece == vulnerableEnPassant) {
                    Piece pawn = table.removePiece(destination);
                    Position posP;
                    if (p.color == Color.White) {
                        posP = new Position(3, destination.column);
                    }
                    else {
                        posP = new Position(4, destination.column);
                    }
                    table.placePiece(pawn, posP);
                }
            }
        }

        public void doPlay(Position origin, Position destination)
        {
            Piece capturedPiece = doMovement(origin, destination);

            if (isInCheck(currentPlayer))
            {
                undoMovement(origin, destination, capturedPiece);
                throw new TableException("You can not get yourself in check!");
            }

            Piece p = table.piece(destination);

            //#specialmove promotion
            if (p is Pawn) {
                if ((p.color == Color.White && destination.line == 0) || (p.color == Color.Black && destination.line == 7)) {
                    p = table.removePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(table, p.color);
                    table.placePiece(queen, destination);
                    pieces.Add(queen);
                }
            }

            if (isInCheck(adversary(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (testCheckMate(adversary(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            //#specialmove en passant
            if (p is Pawn && (destination.line == origin.line - 2 || destination.line == origin.line + 2)) {
                vulnerableEnPassant = p;
            }
            else {
                vulnerableEnPassant = null;
            }
        }

        public void validateOrigin(Position pos)
        {

            if (table.piece(pos) == null)
            {
                throw new TableException("There are no pieces at the chosen origin!");
            }

            if (currentPlayer != table.piece(pos).color)
            {
                throw new TableException("The chosen origin is not yours!");
            }

            if (!table.piece(pos).possibleMovesExist())
            {
                throw new TableException("There are no possible moves for the chosen piece!");
            }
        }

        public void validateDestination(Position origin, Position destination)
        {
            if (!table.piece(origin).possibleMove(destination))
            {
                throw new TableException("Invalid destination position!");
            }
        }

        private void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new TableException("There is no" + color + "king on the table");
            }
            foreach (Piece x in piecesInGame(adversary(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.line, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < table.lines; i++)
                {
                    for (int j = 0; j < table.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = doMovement(origin, destination);
                            bool checkTest = isInCheck(color);
                            undoMovement(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void placeNewPiece(char column, int line, Piece piece)
        {
            table.placePiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        public void placePieces()
        {
            placeNewPiece('a', 1, new Rook(table, Color.White));
            placeNewPiece('b', 1, new Horse(table, Color.White));
            placeNewPiece('c', 1, new Bishop(table, Color.White));
            placeNewPiece('d', 1, new Queen(table, Color.White));
            placeNewPiece('e', 1, new King(table, Color.White, this));
            placeNewPiece('f', 1, new Bishop(table, Color.White));
            placeNewPiece('g', 1, new Horse(table, Color.White));
            placeNewPiece('h', 1, new Rook(table, Color.White));
            placeNewPiece('a', 2, new Pawn(table, Color.White, this));
            placeNewPiece('b', 2, new Pawn(table, Color.White, this));
            placeNewPiece('c', 2, new Pawn(table, Color.White, this));
            placeNewPiece('d', 2, new Pawn(table, Color.White, this));
            placeNewPiece('e', 2, new Pawn(table, Color.White, this));
            placeNewPiece('f', 2, new Pawn(table, Color.White, this));
            placeNewPiece('g', 2, new Pawn(table, Color.White, this));
            placeNewPiece('h', 2, new Pawn(table, Color.White, this));

            placeNewPiece('a', 8, new Rook(table, Color.Black));
            placeNewPiece('b', 8, new Horse(table, Color.Black));
            placeNewPiece('c', 8, new Bishop(table, Color.Black));
            placeNewPiece('d', 8, new Queen(table, Color.Black));
            placeNewPiece('e', 8, new King(table, Color.Black, this));
            placeNewPiece('f', 8, new Bishop(table, Color.Black));
            placeNewPiece('g', 8, new Horse(table, Color.Black));
            placeNewPiece('h', 8, new Rook(table, Color.Black));
            placeNewPiece('a', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('b', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('c', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('d', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('e', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('f', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('g', 7, new Pawn(table, Color.Black, this));
            placeNewPiece('h', 7, new Pawn(table, Color.Black, this));
        }
    }
}
