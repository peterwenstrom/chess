using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Rook
    {
        public Rook(Player owner)
        {
            
        }

        public static bool CheckStraightBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            if (from.X == to.X)
            {
                if (from.Y > to.Y)
                {
                    for (int i = from.Y; i > to.Y; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Y; i < to.Y; ++i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
                            return true;
                    }
                }
            }
            else if (from.Y == to.Y)
            {
                if (from.X > to.X)
                {
                    for (int i = from.X; i > to.X; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.X; i < to.X; ++i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
            }
            return false;
        }

        protected bool IsPathBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            return CheckStraightBlocked(gameBoard, from, to);
        }

        public bool IsValidMove(Board gameBoard, Coordinates from, Coordinates to)
        {
            if ((from.X != to.X && from.Y != to.Y) || ((from.X == to.X && from.Y == to.Y)))
                return false;

            if (this.IsPathBlocked(gameBoard, from, to))
                return false;
            else
                return true;
        }
    }
}
