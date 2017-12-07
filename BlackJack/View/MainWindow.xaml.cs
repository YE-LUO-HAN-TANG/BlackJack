using System;
using System.Windows;
using BlackJack.Models;

namespace BlackJack
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Casino newCasino = new Casino();
        private readonly BJPlayer newPlayer = new BJPlayer();

        public MainWindow()
        {
            newPlayer.GetInCasino(newCasino);
            newPlayer.JoinTable();
            var currentTable = newPlayer.GetCurrentTable();

            InitializeComponent();
            DealerPositionUi.DataContext = currentTable.Dealerposition;
            PlayerPositionUi.DataContext = currentTable.AllPlayerPositons[0];
            GameResult.DataContext = currentTable.AllPlayerPositons[0];

            MoneyBlock.DataContext = currentTable.AllPlayerPositons[0].PlayerInfo;
            StakeBlock.DataContext = currentTable.AllPlayerPositons[0];
        }

        private void HitClick(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.Hit();
            }
            catch (Exception exception)
            {
                if (exception.Message == "您的牌点数爆了")
                {
                    newPlayer.Stand();
                    GameResultDialog.IsOpen = true;
                }
                PrintMessage(exception.Message);
            }
        }

        private void PrepareClick(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.Prepare();
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void Add10(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.AddStake(10);
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void Add50(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.AddStake(50);
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void Add100(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.AddStake(100);
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void CancelStake(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.CancelStake();
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void StandClick(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.Stand();
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void ReplayClick(object sender, RoutedEventArgs e)
        {
            try
            {
                newPlayer.Restart();
                TotalTransitoner.SelectedIndex = 0;
                newPlayer.AddStake(10);
                BJSnackbar.IsActive = false;
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var newStake = int.Parse(StakeBlock.Text.Substring(1));
                newPlayer.AddStake(newStake);
            }
            catch (Exception exception)
            {
                PrintMessage(exception.Message);
                return;
            }
            try
            {
                newPlayer.Hit();
                newPlayer.Stand();
            }
            catch (Exception exception)
            {
                if (exception.Message == "您的牌点数爆了")
                    newPlayer.Stand();
                PrintMessage(exception.Message);
            }
            finally
            {
                GameResultDialog.IsOpen = true;
            }
        }

        private void PrintMessage(string message)
        {
            BJMessage.Content = message;
            BJSnackbar.IsActive = true;
        }

        private void MessageClose(object sender, RoutedEventArgs e)
        {
            BJSnackbar.IsActive = false;
        }
    }
}