
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table {
    class TableException : Exception {
        public TableException(string msg) : base(msg) { 
        }
    }
}
