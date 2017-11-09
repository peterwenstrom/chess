using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class GameRules
    {
        public List<Coordinates> GetPossibleMoves(Piece piece, Board gameBoard)
        {
            List<Coordinates> Moves = new List<Coordinates>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Coordinates NewPostion = new Coordinates(i, j);
                    if (IsValidMove(gameBoard, piece, NewPostion))
                        Moves.Add(NewPostion);
                }
            }

            return Moves;
        }

        public bool IsStalemate(Board gameBoard, Player player)
        {
            foreach (var piece in gameBoard.FindPieces(player.Color))
            {
                List<Coordinates> Moves = GetPossibleMoves(piece, gameBoard);
                foreach (var move in Moves)
                {
                    Board tempBoard = gameBoard.GetCopy();
                    tempBoard.Move(piece.Position, move);
                    if (!this.IsCheck(tempBoard, player))
                        return false;
                }
            }

            return true;
        }

        public bool IsCheck(Board gameBoard, Player player)
        {
            Piece King = gameBoard.FindKing(player.Color);
            List<Piece> OpposingPieces = gameBoard.FindPieces(player.GetOpposingColor());
            return OpposingPieces.Any(piece => IsValidMove(gameBoard, piece, King.Position));
        }

        public bool IsCheckmate(Board gameBoard, Player player)
        {
            return IsStalemate(gameBoard, player) &&
                IsCheck(gameBoard, player);
        }

        public bool IsValidMove(Board gameBoard, Piece piece, Coordinates to)
        {
            if (!gameBoard.IsPositionEmpty(to) && gameBoard.IsPieceSameColor(to, piece.Owner.Color))
                return false;
            
            switch (piece.Type)
            {
                case PieceType.Bishop:
                    if (IsDiagonal(piece.Position, to) && !IsDiagonalBlocked(gameBoard, piece.Position, to))
                        return true;
                    else
                        return false;
                case PieceType.Rook:
                    if (IsStraight(piece.Position, to) && !IsStraightBlocked(gameBoard, piece.Position, to))
                        return true;
                    else
                        return false;
                case PieceType.Queen:
                    if ((IsDiagonal(piece.Position, to) && !IsDiagonalBlocked(gameBoard, piece.Position, to)) ||
                        (IsStraight(piece.Position, to) && !IsStraightBlocked(gameBoard, piece.Position, to)))
                        return true;
                    else
                        return false;
                case PieceType.Pawn:
                    if (IsPawnMove(gameBoard, piece, to))
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

        private bool IsPawnMove(Board gameBoard, Piece pawn, Coordinates to)
        {
            if (pawn.Owner.Color == PlayerColor.White)
            {
                if (pawn.Position.Y == to.Y && to.X - pawn.Position.X == 1 && gameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.X - pawn.Position.X == 1 && Math.Abs(pawn.Position.Y - to.Y) == 1 &&
                    !gameBoard.IsPositionEmpty(to) && !gameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.Y == to.Y && to.X - pawn.Position.X == 2 &&
                    !IsStraightBlocked(gameBoard, pawn.Position, to) && gameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
                    return true;
            } else if (pawn.Owner.Color == PlayerColor.Black)
            {
                if (pawn.Position.Y == to.Y && to.X - pawn.Position.X == -1 && gameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.X - pawn.Position.X == -1 && Math.Abs(pawn.Position.Y - to.Y) == 1 &&
                    !gameBoard.IsPositionEmpty(to) && !gameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.Y == to.Y && to.X - pawn.Position.X == -2 &&
                    !IsStraightBlocked(gameBoard, pawn.Position, to) && gameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
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

        private bool IsStraightBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            if (from.X == to.X)
            {
                if (from.Y > to.Y)
                {
                    for (int i = from.Y-1; i > to.Y; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(from.X, i)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Y+1; i < to.Y; ++i)
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
                    for (int i = from.X-1; i > to.X; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.X+1; i < to.X; ++i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Y)))
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
        private bool IsDiagonalBlocked(Board gameBoard, Coordinates from, Coordinates to)
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
    }
}
