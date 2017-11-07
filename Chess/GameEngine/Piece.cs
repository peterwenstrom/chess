using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine
{
    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public class Piece
    {

        public Coordinates Position { get; set; }
        public Player Owner { get; private set; }
        public PieceType Type { get; private set; }
        public bool HasMoved { get; set; }

        public Piece(Coordinates position, Player owner, PieceType type)
        {
            Position = position;
            Owner = owner;
            Type = type;
            HasMoved = false;
        }

        // Copy constructor
        public Piece(Piece piece)
        {
            Position = piece.Position;
            Owner = piece.Owner;
            Type = piece.Type;
            HasMoved = piece.HasMoved;
        }

    }
}
