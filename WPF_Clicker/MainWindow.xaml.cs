using GameSystemObjects;
using GameSystemObjects.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player player;
        HttpClient client;

        public MainWindow()
        {
            InitializeComponent();

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            PageContainer.Content = new loginPage(this);
        }

        public async Task<Player> GetPlayerAsync(string pName)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("api/player");
            if (responseMessage.IsSuccessStatusCode)
            {
                player = await responseMessage.Content.ReadAsAsync<Player>( Formatter.MediaTypeFormatters );
            }
            return player;
        }
    }
}