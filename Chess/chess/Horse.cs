using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class Horse : Piece {

        public Horse(Table table, Color color) : base(table, color) {

        }

        public override string ToString() {
            return "H";
        }

        private bool canMove(Position pos) {
            Piece p = table.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves() {
            bool[,] mat = new bool[table.lines, table.columns];

            Position pos = new Position(0, 0);

            pos.defineValues(position.line - 1, position.column - 2);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line - 2, position.column - 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line - 2, position.column + 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line - 1, position.column + 2);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line + 1, position.column + 2);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line + 2, position.column + 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line + 2, position.column - 1);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }
            pos.defineValues(position.line + 1, position.column - 2);
            if (table.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }
    }
}
