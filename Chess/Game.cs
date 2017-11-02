using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Game
    {

        public static void Test()
        {
            Player Player1 = new Player("white");
            Player Player2 = new Player("black");

            Tile[,] TileArray = new Tile[8, 8];
            
            Board TestBoard = new Board(TileArray);

            Rook TestRook = new Rook(Player1);
            Bishop TestBishop = new Bishop(Player1);
            Queen TestQueen = new Queen(Player1);

            TestBoard.GameBoard[3, 1].Piece = TestRook;

            Coordinates From = new Coordinates(2, 2);
            Coordinates To = new Coordinates(5, 2);

            if (TestQueen.IsValidMove(TestBoard, From, To))
                System.Diagnostics.Debug.WriteLine("hejsan hejsan");
            else
                System.Diagnostics.Debug.WriteLine("nope nope nope");
        }
    }
}
