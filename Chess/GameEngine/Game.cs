using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public class Game
    {

        public Board GameBoard { get; set; }
        public Player PlayerWhite { get; set; }
        public Player PlayerBlack { get; set; }
        public Player ActivePlayer { get; set; }
        public GameRules Rules { get; set; }

        public Game(GameRules rules)
        {
            Rules = rules;
        }

        public void NewGame(Player playerWhite, Player playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;

            GameBoard = new Board(new Piece[8, 8]);

            GameBoard.NewBoard(PlayerWhite, PlayerBlack);

            ActivePlayer = PlayerWhite;
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
            GameBoard.PrintBoard();
            ChangeActivePlayer();
            bool IsCheck = Rules.IsCheck(GameBoard, ActivePlayer);
            bool IsStalemate = Rules.IsStalemate(GameBoard, ActivePlayer);
            if (IsCheck && IsStalemate)
                return ActivePlayer.GetOpposingColor().ToString() + " player won the game!";
            else if (IsCheck)
                return ActivePlayer.Color.ToString() + " is in check!";
            else if (IsStalemate)
                return "It's a draw...";
            else
                return ActivePlayer.Color.ToString() + " players turn";
        }

        public void ChangeActivePlayer()
        {
            ActivePlayer = GetOppositePlayer(ActivePlayer);
        }

        public List<Coordinates> GetPossibleMoves(Coordinates from)
        {
            return Rules.GetPossibleMoves(GameBoard.GetPiece(from), GameBoard);
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

        public void SkolmattTest()
        {
            Move(new Coordinates(1, 3), new Coordinates(3, 3));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(6, 0), new Coordinates(5, 0));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(0, 2), new Coordinates(3, 5));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(7, 0), new Coordinates(6, 0));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(0, 4), new Coordinates(2, 2));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(7, 6), new Coordinates(5, 7));
            System.Diagnostics.Debug.WriteLine(EndTurn());
            Move(new Coordinates(2, 2), new Coordinates(6, 2));
            System.Diagnostics.Debug.WriteLine(EndTurn());
        }
    }
}
