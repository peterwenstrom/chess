using System;
using System.Collections.Generic;
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
        private Game ChessGame;
        private GUIBoard GameBoard;
        public GamePage(Game chessGame)
        {
            ChessGame = chessGame;
            InitializeComponent();
        }

        private void ClickOnBoard(object sender, MouseButtonEventArgs e)
        {
            Canvas boardCanvas = sender as Canvas;
            Point clickPoint = e.GetPosition(boardCanvas);
            System.Diagnostics.Debug.WriteLine(clickPoint.X);
        }
    }
}
