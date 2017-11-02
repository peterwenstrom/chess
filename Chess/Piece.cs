using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class Piece
    {
        public Player Owner { get; private set; }
        public bool HasMoved { get; set; }
        public Coordinates Position { get; set; }

        public Piece(Player owner)
        {
            Owner = owner;
            HasMoved = false;
        }

        public abstract bool IsValidMove(Board gameBoard, Coordinates from, Coordinates to);
        protected abstract bool IsPathBlocked(Board gameBoard, Coordinates from, Coordinates to);

    }
}
