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

namespace Chess
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public Game ChessGame { get; private set; }
        public GUIBoard GameBoard { get; private set; }
        public Coordinates SelectedCoordinates { get; set; }
        public List<Coordinates> PossibleMoves { get; set; }

        public GamePage(Game chessGame)
        {
            InitializeComponent();
            ChessGame = chessGame;
            ChessGame.NewGame(new Player(PlayerColor.White), new Player(PlayerColor.Black));
            GameBoard = new GUIBoard(ChessGame.GameBoard);

            DataContext = this;
        }

        private void ClickOnBoard(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(sender as Canvas);
            Coordinates clickedCoordinates = GameBoard.ToCoordinates(clickedPoint);
            Point tilePoint = GameBoard.ToPoint(clickedCoordinates);

            if (SelectedCoordinates != null)
            {
                if (SelectedCoordinates.Equals(clickedCoordinates))
                    SelectedCoordinates = null;
                else if (PossibleMoves.Any(move => move.Equals(clickedCoordinates)))
                {
                    if (ChessGame.Move(SelectedCoordinates, clickedCoordinates))
                    {
                        GameBoard.UpdatePieces(SelectedCoordinates, clickedCoordinates);
                        ChessGame.EndTurn();
                    }
                    PossibleMoves = null;
                    SelectedCoordinates = null;
                }
            } else if (GameBoard.IsApprovedSelection(tilePoint, ChessGame.ActivePlayer.Color))
            {
                SelectedCoordinates = clickedCoordinates;
                PossibleMoves = ChessGame.GetPossibleMoves(clickedCoordinates);
            }
                
            
        }
    }
}
