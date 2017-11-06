using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Chess.Piece;
using static Chess.Player;

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
            if (!GameBoard.IsPositionEmpty(to) && GameBoard.IsPieceSameColor(to, piece.Owner.Color))
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
                case PieceType.Pawn:
                    if (IsPawnMove(piece, to))
                        return true;
                    else
                        return false;
                case PieceType.King:
                    if (IsKingMove(piece, to))
                        return true;
                    else
                        return false;
                case PieceType.Knight:
                    if (IsKnightMove(piece, to))
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }

        private bool IsKnightMove(Piece knight, Coordinates to)
        {
            if ((Math.Abs(knight.Position.Y - to.Y) == 2 && Math.Abs(knight.Position.X - to.X) == 1) ||
                (Math.Abs(knight.Position.Y - to.Y) == 1 && Math.Abs(knight.Position.X - to.X) == 2))
                return true;
            else
                return false;
        }

        private bool IsKingMove(Piece king, Coordinates to)
        {
            if (Math.Abs(king.Position.Y - to.Y) < 2 && Math.Abs(king.Position.X - to.X) < 2)
                return true;
            else
                return false;
        }

        private bool IsPawnMove(Piece pawn, Coordinates to)
        {
            if (pawn.Owner.Color == PlayerColor.White)
            {
                if (pawn.Position.X == to.X && to.Y - pawn.Position.Y == 1 && GameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.Y - pawn.Position.Y == 1 && Math.Abs(pawn.Position.X - to.X) == 1 &&
                    !GameBoard.IsPositionEmpty(to) && !GameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.X == to.X && to.Y - pawn.Position.Y == 2 &&
                    !IsStraightBlocked(pawn.Position, to) && GameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
                    return true;
            } else if (pawn.Owner.Color == PlayerColor.Black)
            {
                if (pawn.Position.X == to.X && to.Y - pawn.Position.Y == -1 && GameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.Y - pawn.Position.Y == -1 && Math.Abs(pawn.Position.X - to.X) == 1 &&
                    !GameBoard.IsPositionEmpty(to) && !GameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.X == to.X && to.Y - pawn.Position.Y == -2 &&
                    !IsStraightBlocked(pawn.Position, to) && GameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
                    return true;
            }

            return false;
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
                    for (int i = from.Y-1; i > to.Y; --i)
                    {
                        if (!GameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Y+1; i < to.Y; ++i)
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
                    for (int i = from.X-1; i > to.X; --i)
                    {
                        if (!GameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.X+1; i < to.X; ++i)
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
