using System;
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
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Page
    {
        public Leaderboard()
        {
            InitializeComponent();
        }

        private void spLineUp(object sender, RoutedEventArgs e)
        {
            ((IScrollInfo)sp1).LineUp();
        }

        private void spLineDown(object sender, RoutedEventArgs e)
        {
            ((IScrollInfo)sp1).LineDown();
        }
    }
}
