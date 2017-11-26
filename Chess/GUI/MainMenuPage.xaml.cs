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
using Chess.DataStorage;

namespace Chess.GUI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            GamePage gamePage = new GamePage(new Game(new GameRules()), new GUIBoard(), new GUIMessage());
            gamePage.NewGame(new Player(PlayerColor.White), new Player(PlayerColor.Black));
            this.NavigationService.Navigate(gamePage);
        }
        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
            Test.ReadFile();
        }
        private void ExitGameClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            return;
        }
    }
}
