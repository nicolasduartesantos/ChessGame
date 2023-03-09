using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class Pawn : Piece {

        private ChessMatch match;

        public Pawn(Table table, Color color, ChessMatch match) : base(table, color) {
            this.match = match;
        }

        public override string ToString() {
            return "P";
        }

        private bool enemyExists(Position pos) {
            Piece p = table.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos) {
            return table.piece(pos) == null;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White) {
                pos.defineValues(position.line - 1, position.column);
                if (table.validPosition(pos) && free(pos)) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (table.validPosition(p2) && free(p2) && table.validPosition(pos) && free(pos) && movementQuantity == 0) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line - 1, position.column - 1);
                if (table.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line - 1, position.column + 1);
                if (table.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.line, pos.column] = true;
                }

                //#specialmove en passant
                if (position.line == 3) {
                    Position left = new Position(position.line, position.column - 1);
                    if (table.validPosition(left) && enemyExists(left) && table.piece(left) == match.vulnerableEnPassant) {
                        mat[left.line - 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (table.validPosition(right) && enemyExists(right) && table.piece(right) == match.vulnerableEnPassant) {
                        mat[right.line - 1, right.column] = true;
                    }
                }
            }
            else {
                pos.defineValues(position.line + 1, position.column);
                if (table.validPosition(pos) && free(pos)) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (table.validPosition(p2) && free(p2) && table.validPosition(pos) && free(pos) && movementQuantity == 0) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 1, position.column - 1);
                if (table.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.line, pos.column] = true;
                }
                pos.defineValues(position.line + 1, position.column + 1);
                if (table.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.line, pos.column] = true;
                }

                //#specialmove en passant
                if (position.line == 4) {
                    Position left = new Position(position.line, position.column - 1);
                    if (table.validPosition(left) && enemyExists(left) && table.piece(left) == match.vulnerableEnPassant) {
                        mat[left.line + 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (table.validPosition(right) && enemyExists(right) && table.piece(right) == match.vulnerableEnPassant) {
                        mat[right.line + 1, right.column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
