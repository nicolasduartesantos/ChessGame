using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class Rook : Piece {

        public Rook(Table table, Color color) : base(table, color) {
        }

        public override string ToString() {
            return "R";
        }

        private bool canMove(Position pos) {
            Piece p = table.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

            //up
            pos.defineValues(position.line - 1, position.column);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.line = pos.line - 1;
            }
            //down
            pos.defineValues(position.line + 1, position.column);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.line = pos.line + 1;
            }
            //right
            pos.defineValues(position.line, position.column + 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.column = pos.column + 1;
            }
            //left
            pos.defineValues(position.line, position.column - 1);
            while (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (table.piece(pos) != null && table.piece(pos).color != color) {
                    break;
                }
                pos.column = pos.column - 1;
            }

            return mat;
        }
    }
}
