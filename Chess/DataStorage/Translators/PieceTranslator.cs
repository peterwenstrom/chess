using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Chess.GameEngine;

namespace Chess.DataStorage.Translators
{
    public class PieceTranslator
    {
        public static XElement Translate(Piece piece)
        {
            return new XElement("Piece",
                new XElement("Row", piece.Position.Row.ToString()),
                new XElement("Column", piece.Position.Column.ToString()),
                new XElement("Type", ((int)piece.Type).ToString()),
                new XElement("HasMoved", Convert.ToString(piece.HasMoved)),
                new XElement("Color", ((int)piece.Owner.Color).ToString()));
        }

        public static Piece Translate(XElement element, List<Player> players)
        {
            int row = Int32.Parse(element.Element("Row").Value);
            int column = Int32.Parse(element.Element("Column").Value);
            PlayerColor color = (PlayerColor)Int32.Parse(element.Element("Color").Value);


            var piece = new Piece(
                new Coordinates(row, column),
                players.First(player => player.Color == color),
                (PieceType)Int32.Parse(element.Element("Type").Value))
            {
                HasMoved = Convert.ToBoolean(element.Element("HasMoved").Value)
            };

            return piece;
        }
    }
}
