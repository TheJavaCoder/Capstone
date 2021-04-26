using FluentAssertions;
using GameSystemObjects.ControllerModels;
using GameSystemObjects.Players;
using System.Threading.Tasks;
using Xunit;

namespace GameSystemObjectsTest
{
    public class DatabaseTests
    {
        public DatabaseTests()
        {
            m_playerRepository = new PlayerRepository("data source=tcp:s20.winhost.com;initial catalog=DB_111206_clicker;persist security info=True;user id=DB_111206_clicker_user;password=gtc2021;MultipleActiveResultSets=True;");
        }

        // Known working
        [Fact]

        public async Task createPlayer()
        {
            var testLogin = new PlayerLoginModel
            {
                username = "Deleteme",
                password = "Test",
            };

            var result = await m_playerRepository.CreatePlayer(testLogin);
            result.Should().BeGreaterThan(0);

            await m_playerRepository.RemovePlayer(testLogin.username);
        }

        [Fact]
        public async Task getAllItems()
        {
            var result = await m_playerRepository.GetDefaultItemsAsync();
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task attemptLogin()
        {
            var testLogin = new PlayerLoginModel
            {
                username = "Billy",
                password = "Test",
            };

            var result = await m_playerRepository.loginPlayer(testLogin);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetPlayer_ShouldReturn()
        {
            var testLogin = new PlayerLoginModel
            {
                username = "Billy",
                password = "Test",
            };

            await m_playerRepository.loginPlayer(testLogin);

            var result = await m_playerRepository.GetPlayer(testLogin.username);

            result.Should().BeOfType(new Player().GetType());
            result.items.Should().NotBeEmpty();
        }

        [Fact]
        public async Task savePlayer()
        {
            var testLogin = new PlayerLoginModel
            {
                username = "Billy",
                password = "Test",
            };

            
            //Create the test player using the login function
            await m_playerRepository.loginPlayer(testLogin); 
            //Get the test player's items list
            var testPlayer = await m_playerRepository.GetPlayer(testLogin.username);
            //Create a Player object to send to the SavePlayer function
            testPlayer.items[0].itemAmount = 1000;
            //Save the test player into the database
            await m_playerRepository.SavePlayer(testPlayer);
            //Check to see that the player got saved
            var checkPlayer = await m_playerRepository.GetPlayer(testPlayer.name);
            checkPlayer.items.Should().BeEquivalentTo(testPlayer.items);
            //Delete the player from the database


        }

        IPlayerRepository m_playerRepository;
    }
}
