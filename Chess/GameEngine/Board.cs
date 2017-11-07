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

        public Board GetCopy()
        {
            Board NewBoard = new Board(new Piece[GameBoard.GetLength(0), GameBoard.GetLength(1)]);

            for (int i = 0; i < NewBoard.GameBoard.GetLength(0); ++i)
                for (int j = 0; j < NewBoard.GameBoard.GetLength(1); ++j)
                    if (GameBoard[i, j] != null)
                        NewBoard.GameBoard[i, j] = new Piece(GameBoard[i, j]);
            
            return NewBoard;
        }

        public Piece GetPiece(Coordinates position)
        {
            return GameBoard[position.X, position.Y];
        }

        public void SetPiece(Coordinates position, Piece piece = null)
        {
            GameBoard[position.X, position.Y] = piece;
        }

        public bool IsPositionEmpty(Coordinates position)
        {
            return GameBoard[position.X, position.Y] == null;
        }

        public bool IsPieceSameColor(Coordinates position, PlayerColor color)
        {
            return GameBoard[position.X, position.Y]?.Owner.Color == color;
        }

        public void Move(Coordinates from, Coordinates to)
        {
            Piece piece = GetPiece(from);
            SetPiece(from);
            SetPiece(to, piece);
        }

        public void ClearBoard()
        {
            GameBoard = new Piece[8, 8];
        }

        public void NewBoard(Player player1, Player player2)
        {
            List<Piece> PiecesPlayer1 = new List<Piece>();
            List<Piece> PiecesPlayer2 = new List<Piece>();

            for (int i = 0; i < 8; i++)
            {
                GameBoard[1, i] = new Piece(new Coordinates(1, i), player1, PieceType.Pawn);
                PiecesPlayer1.Add(GameBoard[1, i]);
                GameBoard[6, i] = new Piece(new Coordinates(6, i), player2, PieceType.Pawn);
                PiecesPlayer2.Add(GameBoard[6, i]);
            }
            GameBoard[0, 0] = new Piece(new Coordinates(0, 0), player1, PieceType.Rook);
            PiecesPlayer1.Add(GameBoard[0, 0]);
            GameBoard[0, 7] = new Piece(new Coordinates(0, 7), player1, PieceType.Rook);
            PiecesPlayer1.Add(GameBoard[0, 7]);
            GameBoard[0, 1] = new Piece(new Coordinates(0, 1), player1, PieceType.Knight);
            PiecesPlayer1.Add(GameBoard[0, 1]);
            GameBoard[0, 6] = new Piece(new Coordinates(0, 6), player1, PieceType.Knight);
            PiecesPlayer1.Add(GameBoard[0, 6]);
            GameBoard[0, 2] = new Piece(new Coordinates(0, 2), player1, PieceType.Bishop);
            PiecesPlayer1.Add(GameBoard[0, 2]);
            GameBoard[0, 5] = new Piece(new Coordinates(0, 5), player1, PieceType.Bishop);
            PiecesPlayer1.Add(GameBoard[0, 5]);
            GameBoard[0, 4] = new Piece(new Coordinates(0, 4), player1, PieceType.Queen);
            PiecesPlayer1.Add(GameBoard[0, 4]);
            GameBoard[0, 3] = new Piece(new Coordinates(0, 3), player1, PieceType.King);
            PiecesPlayer1.Add(GameBoard[0, 3]);

            GameBoard[7, 0] = new Piece(new Coordinates(7, 0), player2, PieceType.Rook);
            PiecesPlayer2.Add(GameBoard[7, 0]);
            GameBoard[7, 7] = new Piece(new Coordinates(7, 7), player2, PieceType.Rook);
            PiecesPlayer2.Add(GameBoard[7, 7]);
            GameBoard[7, 1] = new Piece(new Coordinates(7, 1), player2, PieceType.Knight);
            PiecesPlayer2.Add(GameBoard[7, 1]);
            GameBoard[7, 6] = new Piece(new Coordinates(7, 6), player2, PieceType.Knight);
            PiecesPlayer2.Add(GameBoard[7, 6]);
            GameBoard[7, 2] = new Piece(new Coordinates(7, 2), player2, PieceType.Bishop);
            PiecesPlayer2.Add(GameBoard[7, 2]);
            GameBoard[7, 5] = new Piece(new Coordinates(7, 5), player2, PieceType.Bishop);
            PiecesPlayer2.Add(GameBoard[7, 5]);
            GameBoard[7, 4] = new Piece(new Coordinates(7, 4), player2, PieceType.Queen);
            PiecesPlayer2.Add(GameBoard[7, 4]);
            GameBoard[7, 3] = new Piece(new Coordinates(7, 3), player2, PieceType.King);
            PiecesPlayer2.Add(GameBoard[7, 3]);

            player1.Pieces = PiecesPlayer1;
            player2.Pieces = PiecesPlayer2;
        }

        public void LoadBoard(List<Piece> piecesPlayer1, List<Piece> piecesPlayer2)
        {
            piecesPlayer1.ForEach(piece => SetPiece(piece.Position, piece));
            piecesPlayer2.ForEach(piece => SetPiece(piece.Position, piece));
        }
    }
}
