using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess
{
    class King : Piece {

        private ChessMatch match;

        public King(Table table, Color color, ChessMatch match) : base(table, color) {
            this.match = match;
        }

        public override string ToString() {
            return "K";
        }

        private bool canMove(Position pos) {
            Piece p = table.piece(pos);
            return p == null || p.color != color;
        }

        private bool testRookForCastling(Position pos) {
            Piece p = table.piece(pos);
            return p != null && p is Rook && p.color == color && p.movementQuantity == 0;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

            //up
            pos.defineValues(position.line - 1, position.column);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }

            //ne
            pos.defineValues(position.line - 1, position.column + 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }

            //right
            pos.defineValues(position.line, position.column + 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            //se
            pos.defineValues(position.line + 1, position.column + 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            //ne
            pos.defineValues(position.line + 1, position.column);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            //sw
            pos.defineValues(position.line + 1, position.column - 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            //left
            pos.defineValues(position.line, position.column - 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            //nw
            pos.defineValues(position.line - 1, position.column - 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }

            //#specialmove castling
            if (movementQuantity == 0 && !match.check) {
                //#specialmove short castling
                Position posR1 = new Position(position.line, position.column + 3);
                if (testRookForCastling(posR1)) {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (table.piece(p1) == null && table.piece(p2) == null) {
                        mat[position.line, position.column + 2] = true;
                    }
                }

                //#specialmove long castling
                Position posR2 = new Position(position.line, position.column -4);
                if (testRookForCastling(posR2)) {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (table.piece(p1) == null && table.piece(p2) == null && table.piece(p3) == null) {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
