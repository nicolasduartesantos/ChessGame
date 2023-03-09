using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class Queen : Piece {

        public Queen(Table table, Color color) : base(table, color) {

        }

        public override string ToString() {
            return "Q";
        }

        private bool canMove(Position pos) {
            Piece p = table.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

            //left
            pos.defineValues(position.line, position.column - 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                } 
                pos.defineValues(pos.line, pos.column - 1);
            }

            //right
            pos.defineValues(position.line, position.column + 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line, pos.column + 1);
            }

            //up
            pos.defineValues(position.line - 1, position.column);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line - 1, pos.column);
            }

            //down
            pos.defineValues(position.line + 1, position.column);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line + 1, pos.column);
            }

            //NW
            pos.defineValues(position.line - 1, position.column - 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line - 1, pos.column - 1);
            }

            //NE
            pos.defineValues(position.line - 1, position.column + 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line - 1, pos.column + 1);
            }

            //SE
            pos.defineValues(position.line + 1, position.column + 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line + 1, pos.column + 1);
            }

            //SW
            pos.defineValues(position.line + 1, position.column - 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.line + 1, pos.column - 1);
            }

            return mat;
        }
    }
}
