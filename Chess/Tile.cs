using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Tile
    {
        public Piece Piece { get; set; }

        public Tile()
        {
            Piece = null;
        }

        public bool IsEmpty()
        {
            if (Piece != null)
                return false;
            else
                return true;
        }
    }
}
