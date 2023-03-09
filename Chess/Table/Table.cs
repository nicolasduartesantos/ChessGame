using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table {
    class Table {

        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Table(int lines, int columns) {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece piece(int line, int column) {
            return pieces[line, column];
        }

        public Piece piece(Position pos) {
            return pieces[pos.line, pos.column];
        }

        public bool pieceExists(Position pos) {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void placePiece(Piece p, Position pos) {
            if (pieceExists(pos)) {
                throw new TableException("Já existe uma peça nessa posição!");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public Piece removePiece(Position pos) {
            if (piece(pos) == null) {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool validPosition(Position pos) {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns) {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos) { 
            if (!validPosition(pos)) {
                throw new TableException("Posição inválida!");
            }
        }
    }
}
