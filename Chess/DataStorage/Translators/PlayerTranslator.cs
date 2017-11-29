using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Chess.GameEngine;
using Chess.DataStorage.Exceptions;

namespace Chess.DataStorage.Translators
{
    public class PlayerTranslator
    {
        public static XElement Translate(Player player)
        {
            return new XElement("Player",
                new XElement("Color", ((int)player.Color).ToString()));
        }

        public static Player Translate(XElement element)
        {
            int color;

            try
            {
                color = Int32.Parse(element.Element("Color").Value);
            }
            catch (NullReferenceException)
            {
                throw new TranslateToPlayerException("Invalid player in XElement.");
            }

            return new Player((PlayerColor)color);
        }
    }
}
