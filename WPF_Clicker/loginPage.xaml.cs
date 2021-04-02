using GameSystemObjects.ControllerModels;
using System.Windows;
using System.Windows.Controls;

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
                //TODO display message
            }


        }
    }
}