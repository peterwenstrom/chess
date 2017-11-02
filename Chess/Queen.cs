using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Queen : Piece
    {
        public Queen(Player owner) : base(owner)
        {

        }

        protected override bool IsPathBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            if (!Rook.CheckStraightBlocked(gameBoard, from, to) &&
                !Bishop.CheckDiagonalBlocked(gameBoard, from, to))
            {
                return false;
            } else
            {
                return true;
            }
            
        }

        public override bool IsValidMove(Board gameBoard, Coordinates from, Coordinates to)
        {
            if ((Math.Abs(from.X - to.X) != Math.Abs(from.Y - to.Y)) &&
                (from.X != to.X && from.Y != to.Y) || ((from.X == to.X && from.Y == to.Y)))
                return false;

            if (this.IsPathBlocked(gameBoard, from, to))
                return false;
            else
                return true;
        }
    }
}
