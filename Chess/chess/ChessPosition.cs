using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace chess {
    class ChessPosition {

        public char column { get; set; }
        public int line { get; set; }

        public ChessPosition(char column, int line) { 
            this.column = column;
            this.line = line;
        }

        public Position toPosition() {
            return new Position(8 - line, column - 'a');
        }

        public override string ToString() {
            return "" + column + line;
        }
    }
}
