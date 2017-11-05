using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Chess.Piece;
using static Chess.Player;

namespace Chess
{
    public class Board
    {
        public Piece[,] GameBoard { get; private set; }

        public Board (Piece[,] gameBoard)
        {
            GameBoard = gameBoard;
        }

        public bool IsPositionEmpty(Coordinates position)
        {
            return GameBoard[position.X, position.Y] == null;
        }

        public bool IsPieceSameColor(Coordinates position, PlayerColor color)
        {
            return GameBoard[position.X, position.Y].Owner.Color == color;
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
                GameBoard[i, 1] = new Piece(new Coordinates(i, 1), player1, PieceType.Pawn);
                PiecesPlayer1.Add(GameBoard[i, 1]);
                GameBoard[i, 6] = new Piece(new Coordinates(i, 6), player2, PieceType.Pawn);
                PiecesPlayer2.Add(GameBoard[i, 6]);
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
            piecesPlayer1.Select(piece => GameBoard[piece.Position.X, piece.Position.Y] = piece);
            piecesPlayer2.Select(piece => GameBoard[piece.Position.X, piece.Position.Y] = piece);
        }
    }
}
