using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using Chess.DataStorage.Exceptions;
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
            XDocument xmlFile = new XDocument(new XElement("Root",
                new XElement("Players", players.Select(player => PlayerTranslator.Translate(player))),
                new XElement("ActivePlayer", PlayerTranslator.Translate(
                    players.First(player => player.Color == PlayerColor.White))),
                new XElement("GameState", new XElement("Message", gameStateMessage)),
                new XElement("Pieces", pieces.Select(piece => PieceTranslator.Translate(piece)))));

            xmlFile.Save(filename);
        }

        public void SaveFile(string filename)
        {
            XDocument xmlFile = XDocument.Load(this.filename);
            xmlFile.Save(filename);
        }

        public GameData LoadFromFile(string filename)
        {
            string activeFilename = (filename ?? this.filename);

            XDocument xmlFile;

            try
            {
                xmlFile = XDocument.Load(activeFilename);
            }
            catch (FileNotFoundException)
            {
                throw new LoadGameFileException("No game file found from file.");
            }
            
            IEnumerable<XElement> xElements;

            xElements = xmlFile.Descendants("Players").Descendants("Player");
            if (xElements.Count() != 2)
                throw new LoadGameFileException("Save file is incorrect. Wrong number of players.");
            List<Player> players = xElements.Select(player => PlayerTranslator.Translate(player)).ToList();

            xElements = xmlFile.Descendants("ActivePlayer").Descendants("Player");
            if (!xElements.Any())
                throw new LoadGameFileException("Save file is incorrect. Missing Active Player.");
            PlayerColor activePlayerColor = (PlayerColor)Convert.ToInt32(
                xElements.Select(player => player.Element("Color").Value).First());
            Player activePlayer = players.First(player => player.Color == activePlayerColor);

            xElements = xmlFile.Descendants("Piece");
            if (!xElements.Any())
                throw new LoadGameFileException("Save file is incorrect. Missing pieces.");
            List<Piece> pieces = xElements.Select(piece => PieceTranslator.Translate(piece, players)).ToList();

            List<Piece> kings = pieces.FindAll(piece => piece.Type == PieceType.King);
            if (kings.Count != 2 || kings[0].Owner == kings[1].Owner)
                throw new LoadGameFileException("Save file is incorrect. Wrong piece setup.");

            xElements = xmlFile.Descendants("GameState");
            if (xElements.Count() != 1)
                throw new LoadGameFileException("Save file is incorrect. Missing single game state message.");
            string gameStateMessage = xElements
                .Select(element => element.Element("Message").Value).First();

            return new GameData(players, pieces, activePlayer, gameStateMessage);

        }

        public void UpdatePiecesInFile(List<Piece> pieces)
        {
            XDocument xmlFile = XDocument.Load(filename);

            xmlFile.Descendants("Pieces").Remove();

            xmlFile.Element("Root")
                .Add(new XElement("Pieces", pieces.Select(piece => PieceTranslator.Translate(piece))));

            xmlFile.Save(filename);
        }

        public void UpdateGameState(Player player, string gameStateMessage)
        {
            XDocument xmlFile = XDocument.Load(filename);

            xmlFile.Descendants("ActivePlayer").Remove();
            xmlFile.Descendants("GameState").Remove();

            xmlFile.Element("Root").Add(
                new XElement("ActivePlayer", PlayerTranslator.Translate(player)),
                new XElement("GameState", new XElement("Message", gameStateMessage)));

            xmlFile.Save(filename);
        }
    }
}
