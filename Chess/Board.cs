using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Board
    {
        public Tile[,] GameBoard { get; set; }

        public Board (Tile[,] gameBoard)
        {
            GameBoard = gameBoard;
            this.ClearBoard();
        }

        public bool IsTileEmpty(Coordinates coordinates)
        {
            return GameBoard[coordinates.X, coordinates.Y].IsEmpty();
        }

        public void ClearBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    GameBoard[i, j] = new Tile();
                }
            }
        }
    }
}
