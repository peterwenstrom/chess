using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Player
    {
        public string Color { get; private set; }
        public int Score { get; private set; }

        public Player(string color)
        {
            Color = color;
            Score = 0;
        }
    }
}
