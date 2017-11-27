using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.GameEngine;

namespace Chess.DataStorage
{
    public class GameData
    {
        public List<Player> Players { get; private set; }
        public List<Piece> Pieces { get; private set; }
        public Player ActivePlayer { get; private set; }
        public string GameStateMessage { get; private set; }

        public GameData(List<Player> players, List<Piece> pieces, Player activePlayer, string gameStateMessage)
        {
            Players = players;
            Pieces = pieces;
            ActivePlayer = activePlayer;
            GameStateMessage = gameStateMessage;
        }
    }
}
