using GameSystemObjects.ControllerModels;
using GameSystemObjects.Players;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    class DatabaseTest
    {

        private IPlayerRepository playerRepository;

        public DatabaseTest()
        {
            Console.WriteLine("---- Database Unit Tests! ----");

            playerRepository = new PlayerRepository("data source=tcp:s20.winhost.com;initial catalog=DB_111206_clicker;persist security info=True;user id=DB_111206_clicker_user;password=gtc2021;MultipleActiveResultSets=True;");

            init();   
        }

        private async void init() 
        {
            await testPasswordAsync();
        }

        public async Task testPasswordAsync()
        {
            var status = await playerRepository.loginPlayer(new PlayerLoginModel
            {
                player_ID = 0,
                username = "Billy",
                password = "Test",
            });

            Console.WriteLine("test password: " + status);

        }

    }
}
