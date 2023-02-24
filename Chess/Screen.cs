using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table;

namespace Chess {
    class Screen {

        public static void printTable(Table table) {
            for (int i = 0; i < table.lines; i++) {
                for (int j = 0; j < table.columns; j++) {
                    if (table.piece(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        Console.Write(table.piece + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
