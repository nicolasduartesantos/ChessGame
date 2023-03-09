using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table {
    abstract class Piece {

        public Position position { get;set;}
        public Color color { get;protected set;}
        public int movementQuantity { get; protected set; }
        public Table table { get; protected set; }

        public Piece(Table table, Color color) { 
            this.position = null;
            this.position = null;
            this.table = table;
            this.color = color;
            this.movementQuantity = 0;
        }

        public void increaseMovQuantity() {
            movementQuantity++;
        }

        public void decreaseMovQuantity() {
            movementQuantity--;
        }

        public bool possibleMovesExist() {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < table.lines; i++) {
                for (int j = 0; j < table.columns; j++) {
                    if (mat[i, j]) return true;
                }
            }

            return false;
        }

        public bool possibleMove(Position pos) {
            return possibleMoves()[pos.line, pos.column];   
        }

        public abstract bool[,] possibleMoves();
    }
    
}
