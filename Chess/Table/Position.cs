using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table {
    class Position {

        public int line { get; set; }
        public int column { get; set; }

        public Position(int line, int column) {
            this.line = line;
            this.column = column;

        }

        public void defineValues(int line, int column) {
            this.line = line;
            this.column= column;
        }

        public override string ToString() {
            return line + ", " + column ;
        }
    }
}
