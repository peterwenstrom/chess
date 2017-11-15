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
                throw new ArgumentOutOfRangeException();
            Row = row;
            Column = column;
        }

        public override bool Equals(Object obj)
        {
            Coordinates coordinates = obj as Coordinates;
            if (Column == coordinates.Column && Row == coordinates.Row)
                return true;
            else
                return false;
        }
    }
}
