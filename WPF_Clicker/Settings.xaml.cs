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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private MainWindow mainWindow;

        public Settings(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new taskList(mainWindow);
        }
    }
}
