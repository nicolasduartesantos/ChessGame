using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table {
    class Piece {

        public Position position { get;set;}
        public Color color { get;protected set;}
        public int movementQuantity { get; protected set; }
        public Table table { get; protected set; }

        public Piece(Position position, Table table, Color color) { 
            this.position = position;
            this.table = table;
            this.color = color;
            this.movementQuantity = 0;
        }





    }
}
