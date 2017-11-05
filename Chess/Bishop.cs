﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Bishop
    {
        public Bishop(Player owner)
        {

        }

        public static bool CheckDiagonalBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            int Distance = Math.Abs(from.X - to.X) - 1;
            if (from.X > to.X && from.Y > to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.X + i, to.Y + i)))
                        return true;
                }
            }
            else if (from.X < to.X && from.Y > to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.X - i, to.Y + i)))
                        return true;
                }
            }
            else if (from.X < to.X && from.Y < to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.X - i, to.Y - i)))
                        return true;
                }
            }
            else if (from.X > to.X && from.Y < to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.X + i, to.Y - i)))
                        return true;
                }
            }
            return false;
        }

        protected bool IsPathBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            return CheckDiagonalBlocked(gameBoard, from, to);
        }

        public bool IsValidMove(Board gameBoard, Coordinates from, Coordinates to)
        {
            if (Math.Abs(from.X - to.X) != Math.Abs(from.Y - to.Y))
                return false;

            if (this.IsPathBlocked(gameBoard, from, to))
                return false;
            else
                return true;
        }
    }
}