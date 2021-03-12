using System.Windows;
using System.Windows.Controls;

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
            mainWindow.GoBack();
        }
    }
}