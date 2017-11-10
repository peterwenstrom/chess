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
            List<Coordinates> moves = new List<Coordinates>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Coordinates newPostion = new Coordinates(i, j);
                    if (IsValidMove(gameBoard, piece, newPostion))
                        moves.Add(newPostion);
                }
            }

            return moves;
        }

        public bool IsStalemate(Board gameBoard, Player player)
        {
            foreach (var piece in gameBoard.FindPieces(player.Color))
            {
                List<Coordinates> moves = GetPossibleMoves(piece, gameBoard);
                foreach (var move in moves)
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
            Piece king = gameBoard.FindKing(player.Color);
            List<Piece> opposingPieces = gameBoard.FindPieces(player.GetOpposingColor());
            return opposingPieces.Any(piece => IsValidMove(gameBoard, piece, king.Position));
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
            if ((Math.Abs(knight.Position.Column - to.Column) == 2 && Math.Abs(knight.Position.Row - to.Row) == 1) ||
                (Math.Abs(knight.Position.Column - to.Column) == 1 && Math.Abs(knight.Position.Row - to.Row) == 2))
                return true;
            else
                return false;
        }

        private bool IsKingMove(Piece king, Coordinates to)
        {
            if (Math.Abs(king.Position.Column - to.Column) < 2 && Math.Abs(king.Position.Row - to.Row) < 2)
                return true;
            else
                return false;
        }

        private bool IsPawnMove(Board gameBoard, Piece pawn, Coordinates to)
        {
            if (pawn.Owner.Color == PlayerColor.White)
            {
                if (pawn.Position.Column == to.Column && to.Row - pawn.Position.Row == 1 && gameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.Row - pawn.Position.Row == 1 && Math.Abs(pawn.Position.Column - to.Column) == 1 &&
                    !gameBoard.IsPositionEmpty(to) && !gameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.Column == to.Column && to.Row - pawn.Position.Row == 2 &&
                    !IsStraightBlocked(gameBoard, pawn.Position, to) && gameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
                    return true;
            } else if (pawn.Owner.Color == PlayerColor.Black)
            {
                if (pawn.Position.Column == to.Column && to.Row - pawn.Position.Row == -1 && gameBoard.IsPositionEmpty(to))
                    return true;
                else if (to.Row - pawn.Position.Row == -1 && Math.Abs(pawn.Position.Column - to.Column) == 1 &&
                    !gameBoard.IsPositionEmpty(to) && !gameBoard.IsPieceSameColor(to, pawn.Owner.Color))
                    return true;
                else if (pawn.Position.Column == to.Column && to.Row - pawn.Position.Row == -2 &&
                    !IsStraightBlocked(gameBoard, pawn.Position, to) && gameBoard.IsPositionEmpty(to) && !pawn.HasMoved)
                    return true;
            }

            return false;
        }

        private bool IsStraight(Coordinates from, Coordinates to)
        {
            if ((from.Row != to.Row && from.Column != to.Column) || ((from.Row == to.Row && from.Column == to.Column)))
                return false;
            else
                return true;
        }

        private bool IsStraightBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            if (from.Row == to.Row)
            {
                if (from.Column > to.Column)
                {
                    for (int i = from.Column-1; i > to.Column; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(from.Row, i)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Column+1; i < to.Column; ++i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(from.Row, i)))
                            return true;
                    }
                }
            }
            else if (from.Column == to.Column)
            {
                if (from.Row > to.Row)
                {
                    for (int i = from.Row-1; i > to.Row; --i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Column)))
                            return true;
                    }
                }
                else
                {
                    for (int i = from.Row+1; i < to.Row; ++i)
                    {
                        if (!gameBoard.IsPositionEmpty(new Coordinates(i, from.Column)))
                            return true;
                    }
                }
            }
            return false;
        }

        private bool IsDiagonal(Coordinates from, Coordinates to)
        {
            if (Math.Abs(from.Row - to.Row) != Math.Abs(from.Column - to.Column))
                return false;
            else
                return true;
        }
        private bool IsDiagonalBlocked(Board gameBoard, Coordinates from, Coordinates to)
        {
            int distance = Math.Abs(from.Row - to.Row) - 1;
            if (from.Row > to.Row && from.Column > to.Column)
            {
                for (int i = distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.Row + i, to.Column + i)))
                        return true;
                }
            }
            else if (from.Row < to.Row && from.Column > to.Column)
            {
                for (int i = distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.Row - i, to.Column + i)))
                        return true;
                }
            }
            else if (from.Row < to.Row && from.Column < to.Column)
            {
                for (int i = distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.Row - i, to.Column - i)))
                        return true;
                }
            }
            else if (from.Row > to.Row && from.Column < to.Column)
            {
                for (int i = distance; i > 0; --i)
                {
                    if (!gameBoard.IsPositionEmpty(new Coordinates(to.Row + i, to.Column - i)))
                        return true;
                }
            }
            return false;
        }
    }
}
