using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.DataStorage;

namespace Chess.GameEngine
{
    public class Game
    {
        public GameRules Rules { get; private set; }
        public Board GameBoard { get; private set; }
        public Player PlayerWhite { get; private set; }
        public Player PlayerBlack { get; private set; }
        public Player ActivePlayer { get; private set; }
        public string GameStateMessage { get; private set; }

        private StorageHandler storageHandler;

        public Game(GameRules rules, Board gameBoard, StorageHandler handler)
        {
            Rules = rules;
            GameBoard = gameBoard;
            storageHandler = handler;
        }

        public string NewGame(Player playerWhite, Player playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;

            GameBoard.NewBoard(PlayerWhite, PlayerBlack);

            ActivePlayer = PlayerWhite;
            GameStateMessage = "White player's turn";

            storageHandler.SetupTempFile(new List<Player> { PlayerWhite, PlayerBlack },
                GameBoard.FindPieces(),
                GameStateMessage);
            return GameStateMessage;
        }

        public string LoadGame(string filename)
        {
            GameData gameData = storageHandler.LoadFromFile(filename);

            PlayerWhite = gameData.Players.First(player => player.Color == PlayerColor.White);
            PlayerBlack = gameData.Players.First(player => player.Color == PlayerColor.Black);

            GameBoard.LoadBoard(gameData.Pieces);

            ActivePlayer = gameData.ActivePlayer;
            GameStateMessage = gameData.GameStateMessage;

            return GameStateMessage;
        }

        public void SaveGame(string filename)
        {
            storageHandler.SaveFile(filename);
        }

        public bool Move(Coordinates from, Coordinates to)
        {
            if (TryMove(from, to))
            {
                GameBoard.Move(from, to);
                storageHandler.UpdatePiecesInFile(GameBoard.FindPieces());
                return true;
            }
            return false;
        }

        public string EndTurn()
        {
            ChangeActivePlayer();
            bool IsCheck = Rules.IsCheck(GameBoard, ActivePlayer);
            bool IsStalemate = Rules.IsStalemate(GameBoard, ActivePlayer);

            if (IsCheck && IsStalemate)
                GameStateMessage = ActivePlayer.GetOpposingColor().ToString() + " player won the game!";
            else if (IsCheck)
                GameStateMessage = ActivePlayer.Color.ToString() + " is in check!";
            else if (IsStalemate)
                GameStateMessage = "It's a draw...";
            else
                GameStateMessage = ActivePlayer.Color.ToString() + " player's turn";

            storageHandler.UpdateGameState(ActivePlayer, GameStateMessage);

            return GameStateMessage;
        }

        public List<Coordinates> GetPossibleMoves(Coordinates from)
        {
            return Rules.GetPossibleMoves(GameBoard.GetPiece(from), GameBoard);
        }

        private void ChangeActivePlayer()
        {
            ActivePlayer = GetOppositePlayer(ActivePlayer);
        }

        private Player GetOppositePlayer(Player player)
        {
            return player == PlayerWhite ? PlayerBlack : PlayerWhite;
        }

        private bool TryMove(Coordinates from, Coordinates to)
        {
            Board tempBoard = GameBoard.GetCopy();
            Piece piece = tempBoard.GetPiece(from);
            if (piece == null)
                return false;
            if (piece.Owner != ActivePlayer)
                return false;
            if (!Rules.IsValidMove(GameBoard, piece, to))
                return false;
            tempBoard.Move(from, to);
            if (Rules.IsCheck(tempBoard, ActivePlayer))
                return false;

            return true;
        }

    }
}
