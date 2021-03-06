﻿using GameSystemObjects.ControllerModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Clicker
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        private MainWindow window;

        public loginPage(MainWindow w)
        {
            InitializeComponent();
            window = w;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Username.Text))
            {
                ErrorLabel.Content = "Empty Fields";
                return;
            }

            PlayerLoginModel plm = new PlayerLoginModel
            {
                username = Username.Text,
                password = Password.Text
            };
           
            var p = await window.loginPlayerAsync(plm);
            if (p != null)
            {
                window.player = p;
                window.Content = new taskList(window);
            }
            else
            {
                ErrorLabel.Content = "invaild login";
            }


        }

        private async void Username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                Password.Focus();
            }
        }

        private async void Password_KeyDown(object sender, KeyEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Username.Text))
            {
                ErrorLabel.Content = "Empty Fields";
                return;
            }

            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);

            }
        }
    }
}