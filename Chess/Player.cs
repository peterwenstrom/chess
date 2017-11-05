using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Player
    {
        public enum PlayerColor
        {
            White,
            Black
        }

        public PlayerColor Color { get; private set; }
        public int Score { get; private set; }
        public List<Piece> Pieces { get; set; }

        public Player(PlayerColor color)
        {
            Color = color;
            Score = 0;
        }
    }
}
