using GameSystemObjects;
using GameSystemObjects.Players;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        private string CurrentTask = "";
        private StackPanel currentTaskPanel;

        private bool loaded = false;

        public taskList(MainWindow w)
        {
            InitializeComponent();
            window = w;
        }


        private async Task initData()
        {

            if (window.player != null)
            {
                SynchronizationContext.Current.Post(_ => initRender(window.player), null);

                //QueryPlayer(window.webRefreshToken.Token);
            }
        }

        private async Task QueryPlayer(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                // async await web call
                var p = await window.GetPlayerAsync();

                // Link back to the main thread to update values
                SynchronizationContext.Current.Post(_ => { window.player = p; }, null);
                SynchronizationContext.Current.Post(_ => { updateRender(); }, null);

                // wait loop
                await Task.Delay(300);
            }
        }

        private void updateRender()
        {
            foreach ( UIElement uI in ContentContainer.Children )
            {
                
            }
        }

        private void initRender(Player p)
        {
            foreach (ItemTask item in p.GetItems())
            {
                ContentContainer.Children.Add(CreateItemTask(item));
            }
        }

        private Border CreateItemTask(ItemTask item)
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
            b.Margin = new Thickness(0, 20, -8, 20);
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
            buttonContainer.Margin = new Thickness(-8, 10, -8, -8);

            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition());
            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition());

            Button startBtn = new Button();
            startBtn.Content = "Start";
            startBtn.ToolTip = "Begin the task";
            startBtn.Click += new RoutedEventHandler(StartBtn_Click);
            startBtn.Name = item.itemName.Replace(" ", "");
            startBtn.Padding = new Thickness(8);
            startBtn.Template = (ControlTemplate)this.FindResource("ButtonTemplate1");
            startBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#545454"));
            startBtn.Foreground = Brushes.White;
            startBtn.Cursor = System.Windows.Input.Cursors.Hand;
            Grid.SetColumn(startBtn, 0);

            Button updateBtn = new Button();
            updateBtn.Content = "Upgrade";
            updateBtn.ToolTip = "Locked";
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

        public void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            // Stop the previous progress bar

            if (currentTaskPanel != null)
            {
                currentTaskPanel.Children.RemoveAt(currentTaskPanel.Children.Count - 1);
            }

            // Start the current Progress bar and send the request off to the server
            this.CurrentTask = (e.Source as Button).Name;

            var parent = (StackPanel)((e.Source as Button).Parent as Grid).Parent;
            this.currentTaskPanel = parent;

            ProgressBar pb = new ProgressBar();
            pb.Name = this.CurrentTask + "_pb";
            pb.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3B3B3B"));
            pb.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#616161"));
            pb.Minimum = 0;
            pb.Maximum = window.player.getItem(this.CurrentTask).timeCalc;
            pb.Value = 0;
            pb.Height = 10;
            pb.Margin = new Thickness(-8, 8, -8, -8);

            Duration d = new Duration(TimeSpan.FromTicks(window.player.getItem(CurrentTask).timeCalc));
            DoubleAnimation da = new DoubleAnimation(window.player.getItem(this.CurrentTask).timeCalc, d);
            da.RepeatBehavior = RepeatBehavior.Forever;
            pb.BeginAnimation(ProgressBar.ValueProperty, da);

            window.ExcuteAction(CurrentTask, "ENABLE");
           
            parent.Children.Add(pb);
        }

        public async void UpgradeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            window.Navigate(new Settings(window));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                await initData();
                loaded = true;
            }
        }

    }
}