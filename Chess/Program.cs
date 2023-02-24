using System;
using table;

namespace Chess {
    class Program {
        static void Main(string[] args) {

            Table table = new Table(8, 8);
            Screen.printTable(table);
            Console.ReadLine();
        }
    }
}