using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class Bishop : Piece {

        public Bishop(Table table, Color color) : base(table, color) {

        }

        public override string ToString() {
            return "B";
        }

        private bool canMove(Position pos) {
            Piece p = table.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

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

            ///SW
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
