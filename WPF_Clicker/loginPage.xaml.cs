using System.Windows;
using System.Windows.Controls;
using GameSystemObjects.ControllerModels;

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

            if( await window.loginPlayerAsync(plm) != null )
            {
                window.Content = new taskList(window);
            }else
            {
                //TODO display message
            }


        }
    }
}