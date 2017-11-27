using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Chess.DataStorage.Translators;
using Chess.GameEngine;


namespace Chess.DataStorage
{
    public class StorageHandler
    {
        private string filename;

        public StorageHandler(string newFilename = "TempFile.xml")
        {
            filename = newFilename;
        }

        public void SetupTempFile(List<Player> players, List<Piece> pieces, string gameStateMessage)
        {
            XDocument xmlDocument = new XDocument(new XElement("Root",
                new XElement("Players", players.Select(player => PlayerTranslator.Translate(player))),
                new XElement("ActivePlayer", PlayerTranslator.Translate(
                    players.First(player => player.Color == PlayerColor.White))),
                new XElement("GameState", new XElement("Message", gameStateMessage)),
                new XElement("Pieces", pieces.Select(piece => PieceTranslator.Translate(piece)))));

            xmlDocument.Save(filename);
        }

        public GameData LoadFromFile(string filename)
        {
            string activeFilename = (filename ?? this.filename);

            XDocument xmlFile = XDocument.Load(activeFilename);

            List<Player> players = xmlFile.Descendants("Players").Descendants("Player")
                .Select(player => PlayerTranslator.Translate(player)).ToList();

            PlayerColor activePlayerColor = (PlayerColor)Convert.ToInt32(
                xmlFile.Descendants("ActivePlayer").Descendants("Player")
                .Select(player => player.Element("Color").Value).First());

            Player activePlayer = players.First(player => player.Color == activePlayerColor);

            List<Piece> pieces = xmlFile.Descendants("Piece")
                .Select(piece => PieceTranslator.Translate(piece, players)).ToList();

            string gameStateMessage = xmlFile.Descendants("GameState")
                .Select(element => element.Element("Message").Value).First();

            return new GameData(players, pieces, activePlayer, gameStateMessage);

        }
    }
}
