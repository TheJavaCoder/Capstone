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
    /// Interaction logic for taskList.xaml
    /// </summary>
    public partial class taskList : Page
    {

        private MainWindow window;

        public taskList(MainWindow w)
        {
            InitializeComponent();
            window = w;

            //TaskButton.Background = (Brush) new BrushConverter().ConvertFrom("#616161");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new Settings(window);
        }
    }
}