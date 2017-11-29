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
using Chess.DataStorage.Exceptions;

namespace Chess.GUI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private GamePage gamePage;
        public MainMenuPage()
        {
            InitializeComponent();

            gamePage = new GamePage(
                new Game(new GameRules(), new Board(new Piece[8, 8]), new StorageHandler()),
                new GUIBoard(),
                new GUIMessage());
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            gamePage.NewGame(new Player(PlayerColor.White), new Player(PlayerColor.Black));
            this.NavigationService.Navigate(gamePage);
        }
        private void ResumeGameClick(object sender, RoutedEventArgs e)
        {
            try
            {
                gamePage.LoadGame();
                this.NavigationService.Navigate(gamePage);
            }
            catch (LoadGameFileException error)
            {
                MessageBox.Show(error.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML File|*.xml"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                try
                {
                    gamePage.LoadGame(filename);
                    this.NavigationService.Navigate(gamePage);
                }
                catch (LoadGameFileException error)
                {
                    MessageBox.Show(error.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }

        }
        private void ExitGameClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            return;
        }
    }
}
