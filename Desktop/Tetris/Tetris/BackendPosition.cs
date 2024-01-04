using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public BackendPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
