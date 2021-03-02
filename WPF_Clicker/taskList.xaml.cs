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

        private Window window;

        public taskList(Window w)
        {
            InitializeComponent();
            window = w;

            //TaskButton.Background = (Brush) new BrushConverter().ConvertFrom("#616161");
        }
    }
}
