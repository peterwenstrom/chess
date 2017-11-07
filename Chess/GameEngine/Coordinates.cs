using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            if (x > 7 || x < 0 || y > 7 || y < 0)
                throw new IndexOutOfRangeException();
            X = x;
            Y = y;
        }
    }
}
