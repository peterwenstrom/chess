using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class Board
    {
        public Piece[,] GameBoard { get; private set; }

        public Board (Piece[,] gameBoard)
        {
            GameBoard = gameBoard;
        }

        public Board GetCopy()
        {
            Board newBoard = new Board(new Piece[GameBoard.GetLength(0), GameBoard.GetLength(1)]);

            for (int i = 0; i < newBoard.GameBoard.GetLength(0); ++i)
                for (int j = 0; j < newBoard.GameBoard.GetLength(1); ++j)
                    if (GameBoard[i, j] != null)
                        newBoard.GameBoard[i, j] = new Piece(GameBoard[i, j]);
            
            return newBoard;
        }

        public Piece GetPiece(Coordinates position)
        {
            return GameBoard[position.Row, position.Column];
        }

        public void SetPiece(Coordinates position, Piece piece = null)
        {
            GameBoard[position.Row, position.Column] = piece;
        }

        public Piece FindKing(PlayerColor color)
        {
            for (int i = 0; i < GameBoard.GetLength(0); ++i)
                for (int j = 0; j < GameBoard.GetLength(1); ++j)
                    if (GameBoard[i, j] != null && GameBoard[i, j].Type == PieceType.King && GameBoard[i, j].Owner.Color == color)
                        return GameBoard[i, j];
            return null;
        }

        public List<Piece> FindPieces(PlayerColor color)
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < GameBoard.GetLength(0); ++i)
                for (int j = 0; j < GameBoard.GetLength(1); ++j)
                    if (GameBoard[i, j] != null && GameBoard[i, j].Owner.Color == color)
                        pieces.Add(GameBoard[i, j]);
            return pieces;
        }

        public bool IsPositionEmpty(Coordinates position)
        {
            return GameBoard[position.Row, position.Column] == null;
        }

        public bool IsPieceSameColor(Coordinates position, PlayerColor color)
        {
            return GameBoard[position.Row, position.Column]?.Owner.Color == color;
        }

        public void Move(Coordinates from, Coordinates to)
        {
            Piece piece = GetPiece(from);
            SetPiece(from);
            SetPiece(to, piece);
            piece.Position = to;
        }

        public void ClearBoard()
        {
            GameBoard = new Piece[8, 8];
        }

        public void NewBoard(Player player1, Player player2)
        {
            for (int i = 0; i < 8; i++)
            {
                GameBoard[1, i] = new Piece(new Coordinates(1, i), player1, PieceType.Pawn);
                GameBoard[6, i] = new Piece(new Coordinates(6, i), player2, PieceType.Pawn);
            }
            GameBoard[0, 0] = new Piece(new Coordinates(0, 0), player1, PieceType.Rook);
            GameBoard[0, 7] = new Piece(new Coordinates(0, 7), player1, PieceType.Rook);
            GameBoard[0, 1] = new Piece(new Coordinates(0, 1), player1, PieceType.Knight);
            GameBoard[0, 6] = new Piece(new Coordinates(0, 6), player1, PieceType.Knight);
            GameBoard[0, 2] = new Piece(new Coordinates(0, 2), player1, PieceType.Bishop);
            GameBoard[0, 5] = new Piece(new Coordinates(0, 5), player1, PieceType.Bishop);
            GameBoard[0, 4] = new Piece(new Coordinates(0, 4), player1, PieceType.Queen);
            GameBoard[0, 3] = new Piece(new Coordinates(0, 3), player1, PieceType.King);

            GameBoard[7, 0] = new Piece(new Coordinates(7, 0), player2, PieceType.Rook);
            GameBoard[7, 7] = new Piece(new Coordinates(7, 7), player2, PieceType.Rook);
            GameBoard[7, 1] = new Piece(new Coordinates(7, 1), player2, PieceType.Knight);
            GameBoard[7, 6] = new Piece(new Coordinates(7, 6), player2, PieceType.Knight);
            GameBoard[7, 2] = new Piece(new Coordinates(7, 2), player2, PieceType.Bishop);
            GameBoard[7, 5] = new Piece(new Coordinates(7, 5), player2, PieceType.Bishop);
            GameBoard[7, 4] = new Piece(new Coordinates(7, 4), player2, PieceType.Queen);
            GameBoard[7, 3] = new Piece(new Coordinates(7, 3), player2, PieceType.King);
        }

        public void LoadBoard(List<Piece> piecesPlayer1, List<Piece> piecesPlayer2)
        {
            
        }

        // FOR TESTING PURPOSES
        public void PrintBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = GameBoard[i, j];

                    switch (piece?.Type)
                    {
                        case PieceType.Bishop:
                            System.Diagnostics.Debug.Write("B ");
                            break;
                        case PieceType.Rook:
                            System.Diagnostics.Debug.Write("R ");
                            break;
                        case PieceType.Queen:
                            System.Diagnostics.Debug.Write("Q ");
                            break;
                        case PieceType.Pawn:
                            System.Diagnostics.Debug.Write("P ");
                            break;
                        case PieceType.King:
                            System.Diagnostics.Debug.Write("K ");
                            break;
                        case PieceType.Knight:
                            System.Diagnostics.Debug.Write("k ");
                            break;
                        default:
                            System.Diagnostics.Debug.Write("X ");
                            break;
                    }
                }
                System.Diagnostics.Debug.Write("\n");
            }
            System.Diagnostics.Debug.Write("\n");
        }
    }
}
