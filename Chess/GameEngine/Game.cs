using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class Game
    {

        public Board GameBoard { get; private set; }
        public Player PlayerWhite { get; private set; }
        public Player PlayerBlack { get; private set; }
        public Player ActivePlayer { get; private set; }
        public GameRules Rules { get; private set; }
        public string GameStateMessage { get; private set; }

        public Game(GameRules rules)
        {
            Rules = rules;
        }

        public string NewGame(Player playerWhite, Player playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;

            GameBoard = new Board(new Piece[8, 8]);
            GameBoard.NewBoard(PlayerWhite, PlayerBlack);

            ActivePlayer = PlayerWhite;

            GameStateMessage = "White player's turn";
            return GameStateMessage;
        }

        public void LoadGame(Player playerWhite, Player playerBlack, List<Piece> pieces)
        {
            GameBoard = new Board(new Piece[8, 8]);
            GameBoard.LoadBoard(pieces);
        }

        public bool Move(Coordinates from, Coordinates to)
        {
            if (TryMove(from, to))
            {
                GameBoard.Move(from, to);
                return true;
            }
            return false;
        }

        public string EndTurn()
        {
            // GameBoard.PrintBoard();
            ChangeActivePlayer();
            bool IsCheck = Rules.IsCheck(GameBoard, ActivePlayer);
            bool IsStalemate = Rules.IsStalemate(GameBoard, ActivePlayer);
            if (IsCheck && IsStalemate)
                GameStateMessage = ActivePlayer.GetOpposingColor().ToString() + " player won the game!";
            else if (IsCheck)
                GameStateMessage = ActivePlayer.Color.ToString() + " is in check!";
            else if (IsStalemate)
                GameStateMessage = "It's a draw...";
            else
                GameStateMessage = ActivePlayer.Color.ToString() + " player's turn";

            return GameStateMessage;
        }

        public List<Coordinates> GetPossibleMoves(Coordinates from)
        {
            return Rules.GetPossibleMoves(GameBoard.GetPiece(from), GameBoard);
        }

        private void ChangeActivePlayer()
        {
            ActivePlayer = GetOppositePlayer(ActivePlayer);
        }

        private Player GetOppositePlayer(Player player)
        {
            return player == PlayerWhite ? PlayerBlack : PlayerWhite;
        }

        private bool TryMove(Coordinates from, Coordinates to)
        {
            Board tempBoard = GameBoard.GetCopy();
            Piece piece = tempBoard.GetPiece(from);
            if (piece == null)
                return false;
            if (piece.Owner != ActivePlayer)
                return false;
            if (!Rules.IsValidMove(GameBoard, piece, to))
                return false;
            tempBoard.Move(from, to);
            if (Rules.IsCheck(tempBoard, ActivePlayer))
                return false;

            return true;
        }

    }
}
