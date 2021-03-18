using System.Windows;
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
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow.GoBack();
        }

    }
}


//private void Button_Click(object sender, RoutedEventArgs e)
//{
//
//    window.Content = new taskList(window);
//}