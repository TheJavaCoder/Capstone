using GameSystemObjects;
using GameSystemObjects.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task initData()
        {
            Player p = await window.GetPlayerAsync("Test");
            if (p != null)
            {
                SynchronizationContext.Current.Post(_ => TaskLabel.Content = p.name, null);
                SynchronizationContext.Current.Post(_ => initRender(p), null);
            }
        }

        public void initRender(Player p)
        {
            foreach(ItemTask item in p.GetItems())
            {
                ContentContainer.Children.Add(CreateItemTask(item));
            }
        }

        public Border CreateItemTask(ItemTask item)
        {
            // For the rounded corners
            Border b = new Border();
            b.Name = item.itemName;

            // Main vertical panel
            StackPanel newItem = new StackPanel();
            newItem.Orientation = Orientation.Vertical;
            newItem.Name = item.itemName + "Container";

            b.Width = window.Width * 0.75;
            b.Margin = new Thickness(0, 20, 0, 20);
            b.Background = Brushes.White;
            b.CornerRadius = new CornerRadius(8);
            b.Padding = new Thickness(8);
            b.Child = newItem;
            
            // Task Name
            Label taskName = new Label();
            taskName.FontSize = 15;
            taskName.Content = item.itemName;
            taskName.FontWeight = FontWeight.FromOpenTypeWeight(1);
            taskName.HorizontalAlignment = HorizontalAlignment.Center;
            newItem.Children.Add(taskName);

            // Task Image
            Image taskImage = new Image();
            taskImage.Source = new BitmapImage(new Uri(item.itemIcon));
            newItem.Children.Add(taskImage);



            return b;

        }

        private async void Settings_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new Settings(window);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await initData();
        }
    }
}