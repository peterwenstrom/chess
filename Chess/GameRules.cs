using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Chess.Piece;

namespace Chess
{
    public class GameRules
    {
        public Board GameBoard { get; private set; }

        public GameRules(Board gameBoard)
        {
            GameBoard = gameBoard;
        }
        public bool IsInCheck(Piece king, List<Piece> opposingPieces)
        {
            return false;
        }
        public bool IsValidMove(Piece piece, Coordinates to)
        {
            if (!GameBoard.IsPositionEmpty(to) || GameBoard.IsPieceSameColor(to, piece.Owner.Color))
                return false;
            
            switch (piece.Type)
            {
                case PieceType.Bishop:
                    if (IsDiagonal(piece.Position, to) && !IsDiagonalBlocked(piece.Position, to))
                        return true;
                    else
                        return false;
                case PieceType.Rook:
                    if (IsStraight(piece.Position, to) && !IsStraightBlocked(piece.Position, to))
                        return true;
                    else
                        return false;
                case PieceType.Queen:
                    if ((IsDiagonal(piece.Position, to) && !IsDiagonalBlocked(piece.Position, to)) ||
                        (IsStraight(piece.Position, to) && !IsStraightBlocked(piece.Position, to)))
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }

        private bool IsStraight(Coordinates from, Coordinates to)
        {
            if ((from.X != to.X && from.Y != to.Y) || ((from.X == to.X && from.Y == to.Y)))
                return false;
            else
                return true;
        }

        private bool IsStraightBlocked(Coordinates from, Coordinates to)
        {
            if (from.X == to.X)
            {
                if (from.Y > to.Y)
                {
                    for (int i = from.Y; i > to.Y; --i)
                    {
                        if (!GameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Y; i < to.Y; ++i)
                    {
                        if (!GameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
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
                        if (!GameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.X; i < to.X; ++i)
                    {
                        if (!GameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
            }
            return false;
        }

        private bool IsDiagonal(Coordinates from, Coordinates to)
        {
            if (Math.Abs(from.X - to.X) != Math.Abs(from.Y - to.Y))
                return false;
            else
                return true;
        }
        private bool IsDiagonalBlocked(Coordinates from, Coordinates to)
        {
            int Distance = Math.Abs(from.X - to.X) - 1;
            if (from.X > to.X && from.Y > to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!GameBoard.IsPositionEmpty(new Coordinates(to.X + i, to.Y + i)))
                        return true;
                }
            }
            else if (from.X < to.X && from.Y > to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!GameBoard.IsPositionEmpty(new Coordinates(to.X - i, to.Y + i)))
                        return true;
                }
            }
            else if (from.X < to.X && from.Y < to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!GameBoard.IsPositionEmpty(new Coordinates(to.X - i, to.Y - i)))
                        return true;
                }
            }
            else if (from.X > to.X && from.Y < to.Y)
            {
                for (int i = Distance; i > 0; --i)
                {
                    if (!GameBoard.IsPositionEmpty(new Coordinates(to.X + i, to.Y - i)))
                        return true;
                }
            }
            return false;
        }
    }
}
