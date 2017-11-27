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
    public class PlayerTranslator
    {
        public static XElement Translate(Player player)
        {
            return new XElement("Player",
                new XElement("Color", ((int)player.Color).ToString()));
        }

        public static Player Translate(XElement element)
        {
            // Try catch block???
            return new Player((PlayerColor)Int32.Parse(element.Element("Color").Value));
        }
    }
}
