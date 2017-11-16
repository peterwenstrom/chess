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
            GamePage gamePage = new GamePage(new Game(new GameRules()));
            this.NavigationService.Navigate(gamePage);
        }
        private void LoadGameClick(object sender, RoutedEventArgs e)
        {

        }
        private void ExitGameClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            return;
        }
    }
}
