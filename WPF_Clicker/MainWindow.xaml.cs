using GameSystemObjects;
using GameSystemObjects.ControllerModels;
using GameSystemObjects.Players;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPF_Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public Player player = null;
        HttpClient client;

        public MainWindow()
        {
            InitializeComponent();

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44339/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.Content = new loginPage(this);
        }

        public async Task<Player> loginPlayerAsync(PlayerLoginModel plm)
        {
            var json = JsonConvert.SerializeObject(plm);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("api/player/", byteContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                player = await responseMessage.Content.ReadAsAsync<Player>(Formatter.MediaTypeFormatters);
            }
            return player;
        }

        public async Task<Player> GetPlayerAsync(string pName)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("api/player/" + pName);
            if (responseMessage.IsSuccessStatusCode)
            {
                player = await responseMessage.Content.ReadAsAsync<Player>(Formatter.MediaTypeFormatters);
            }
            return player;
        }


        public async Task<bool> LogoutPlayerAsync()
        {
            var json = JsonConvert.SerializeObject(player.name);


            HttpContent content = new StringContent(json);


            HttpResponseMessage responseMessage = await client.PutAsync("api/player/" + player.name, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                this.Content = new loginPage(this);
                return true;
            }
            return false;
        }
    }
}