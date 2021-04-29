﻿using System.Windows;
using System.Windows.Controls;

namespace WPF_Clicker
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private MainWindow settingsWindow;

        public Settings(MainWindow mw)
        {
            InitializeComponent();
            settingsWindow = mw;
            onlinePlayersText.Content = "35 Clicking Nerds";

        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow.GoBack();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await settingsWindow.LogoutPlayerAsync();
        }

    }
}


//private void Button_Click(object sender, RoutedEventArgs e)
//{
//
//    window.Content = new taskList(window);
//}