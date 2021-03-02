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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            window.Content = new taskList(window);
        }
    }
}
