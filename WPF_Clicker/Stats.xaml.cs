﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Clicker
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Page
    {

        MainWindow window;

        public Stats(MainWindow w)
        {
            InitializeComponent();
            window = w;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            window.Navigate(new Settings(window));
        }
    }
}
