using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess.GameEngine;

namespace Chess.GUI
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public Game ChessGame { get; private set; }
        public GUIBoard GameBoard { get; private set; }
        public GUIMessage GameMessage { get; private set; }

        public GamePage(Game chessGame, GUIBoard gameBoard, GUIMessage message)
        {
            InitializeComponent();
            ChessGame = chessGame;
            GameBoard = gameBoard;
            GameMessage = message;

            DataContext = this;
        }

        public void NewGame(Player playerWhite, Player playerBlack)
        {
            GameMessage.Message = ChessGame.NewGame(playerWhite, playerBlack);
            GameBoard.SetUpBoard(ChessGame.GameBoard);
        }

        public void LoadGame()
        {

        }

        private void ClickOnBoard(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(sender as Canvas);
            Coordinates clickedCoordinates = GameBoard.ToCoordinates(clickedPoint);
            Point clickedTilePoint = GameBoard.ToPoint(clickedCoordinates);

            if (GameBoard.SelectedTile != null)
            {
                if (GameBoard.SelectedTile.IsSameTile(clickedTilePoint))
                {
                    GameBoard.ClearPossibleMoves();
                    GameBoard.SelectedTile = null;

                }
                else if (GameBoard.PossibleMoves.Any(move => move.IsSameTile(clickedTilePoint)))
                {
                    Coordinates selectedCoordinates = GameBoard.ToCoordinates(
                        new Point(GameBoard.SelectedTile.XPosition, GameBoard.SelectedTile.YPosition));
                    if (ChessGame.Move(selectedCoordinates, clickedCoordinates))
                    {
                        GameBoard.UpdatePieces(selectedCoordinates, clickedCoordinates);
                        GameMessage.Message = ChessGame.EndTurn();
                    }
                    GameBoard.ClearPossibleMoves();
                    GameBoard.SelectedTile = null;
                }
            }
            else if (GameBoard.IsApprovedSelection(clickedTilePoint, ChessGame.ActivePlayer.Color))
            {
                GameBoard.SelectedTile = new GUITile(clickedTilePoint);
                GameBoard.SetPossibleMoves(ChessGame.GetPossibleMoves(clickedCoordinates));
            }
                
            
        }

        private void SaveGameClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainMenuClick(object sender, RoutedEventArgs e)
        {
            MainMenuPage mainMenuPage = new MainMenuPage();
            this.NavigationService.Navigate(mainMenuPage);
        }
    }
}
