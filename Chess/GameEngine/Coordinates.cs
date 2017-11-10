using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinates(int row, int column)
        {
            if (row > 7 || row < 0 || column > 7 || column < 0)
                throw new IndexOutOfRangeException();
            Row = row;
            Column = column;
        }
    }
}
