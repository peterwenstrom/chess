using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chess.Player;

namespace Chess
{
    public class Game
    {

        public static void Test()
        {
            Player Player1 = new Player(PlayerColor.White);
            Player Player2 = new Player(PlayerColor.Black);
            
            Board TestBoard = new Board(new Piece[8, 8]);
            GameRules Rules = new GameRules(TestBoard);
            TestBoard.NewBoard(Player1, Player2);

            Coordinates Position = new Coordinates(0, 0);

            if (TestBoard.IsPositionEmpty(Position))
                System.Diagnostics.Debug.WriteLine("hejsan hejsan");
            else
                System.Diagnostics.Debug.WriteLine("nope nope nope");
        }
    }
}
