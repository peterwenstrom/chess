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

        public void NewGame()
        {
            PlayerWhite = new Player(PlayerColor.White);
            PlayerBlack = new Player(PlayerColor.Black);

            GameBoard = new Board(new Piece[8, 8]);

            GameBoard.NewBoard(PlayerWhite, PlayerBlack);

            ActivePlayer = PlayerWhite;
        }

        public void ChangeActivePlayer()
        {
            ActivePlayer = GetOppositePlayer(ActivePlayer);
        }

        private Player GetOppositePlayer(Player player)
        {
            return player == PlayerWhite ? PlayerBlack : PlayerWhite;
        }

        public static void Test()
        {
            Player Player1 = new Player(PlayerColor.White);
            Player Player2 = new Player(PlayerColor.Black);

            Board TestBoard = new Board(new Piece[8, 8]);
            GameRules Rules = new GameRules();

            TestBoard.NewBoard(Player1, Player2);

            TestBoard.PrintBoard();

            Piece piece = TestBoard.GetPiece(new Coordinates(1, 4));
            Player2.RemovePiece(piece);

            Board Board2 = TestBoard.GetCopy();
            Board2.Move(new Coordinates(1, 1), new Coordinates(2, 1));

            TestBoard.PrintBoard();

            foreach (var tempPiece in Player1.Pieces) {
                System.Diagnostics.Debug.Write(tempPiece.Position.X);
                System.Diagnostics.Debug.Write(" ");
                System.Diagnostics.Debug.WriteLine(tempPiece.Position.Y);
            }
            //Board2.PrintBoard();
            
        }
    }
}
