using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public enum PlayerColor
    {
        White,
        Black
    }

    public class Player
    {

        public PlayerColor Color { get; private set; }
        public int Score { get; private set; }

        public Player(PlayerColor color)
        {
            Color = color;
            Score = 0;
        }

        public PlayerColor GetOpposingColor()
        {
            return Color != PlayerColor.White ? PlayerColor.White : PlayerColor.Black;
        }
    }
}
