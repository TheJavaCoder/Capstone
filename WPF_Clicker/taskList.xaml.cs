using GameSystemObjects;
using GameSystemObjects.Players;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

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

            DropShadowEffect dropShadowBitmapEffect = new DropShadowEffect();
            dropShadowBitmapEffect.Opacity = 0.5;
            b.Effect = dropShadowBitmapEffect;

            // Main vertical panel
            StackPanel newItem = new StackPanel();
            newItem.Orientation = Orientation.Vertical;

            b.Width = window.Width * 0.75;
            b.Margin = new Thickness(0, 20, 0, 20);
            b.Background = Brushes.White;
            b.CornerRadius = new CornerRadius(10);
            b.Padding = new Thickness(8);
            b.Child = newItem;
            
            // Task Name
            Label taskName = new Label();
            taskName.FontSize = 15;
            taskName.Content = item.itemName;
            taskName.FontWeight = FontWeight.FromOpenTypeWeight(3);
            taskName.HorizontalAlignment = HorizontalAlignment.Center;
            newItem.Children.Add(taskName);

            // Task Image
            Image taskImage = new Image();
            taskImage.Source = new BitmapImage(new Uri(item.itemIcon));
            newItem.Children.Add(taskImage);

            // Task Amount Labels
            Label taskAmount = new Label();
            taskAmount.Content = "Inventory Amount: " + item.itemAmount;
            taskAmount.Name = item.itemName.Replace(" ", "") + "_Amount";
            taskAmount.FontWeight = FontWeight.FromOpenTypeWeight(1);
            newItem.Children.Add(taskAmount);

            Label taskLevel = new Label();
            taskLevel.Content = "Level: " + item.resourceGatheringLevel;
            taskLevel.Name = item.itemName.Replace(" ", "") + "_Lvl";
            taskLevel.FontWeight = FontWeight.FromOpenTypeWeight(1);
            newItem.Children.Add(taskLevel);

            // Action buttons
            Grid buttonContainer = new Grid();
            buttonContainer.Margin = new Thickness(0, 10, 0, 10);

            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition());
            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition());

            Button startBtn = new Button();
            startBtn.Content = "Start";
            startBtn.Click += new RoutedEventHandler(StartBtn_Click);
            startBtn.Padding = new Thickness(8);
            startBtn.Template = (ControlTemplate)this.FindResource("ButtonTemplate1");
            startBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#545454"));
            startBtn.Foreground = Brushes.White;
            startBtn.Cursor = System.Windows.Input.Cursors.Hand;
            Grid.SetColumn(startBtn, 0);

            Button updateBtn = new Button();
            updateBtn.Content = "Upgrade";
            updateBtn.Click += new RoutedEventHandler(UpgradeBtn_Click);
            updateBtn.Padding = new Thickness(8);
            updateBtn.Template = (ControlTemplate)this.FindResource("ButtonTemplate1");
            updateBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#545454"));
            updateBtn.Foreground = Brushes.White;
            updateBtn.Cursor = System.Windows.Input.Cursors.Hand;
            Grid.SetColumn(updateBtn, 1);

            buttonContainer.Children.Add(startBtn);
            buttonContainer.Children.Add(updateBtn);

            newItem.Children.Add(buttonContainer);

            return b;

        }

        public async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
        
        }

        public async void UpgradeBtn_Click(object sender, RoutedEventArgs e)
        {

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