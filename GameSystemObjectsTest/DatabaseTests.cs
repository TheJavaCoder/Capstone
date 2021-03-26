using FluentAssertions;
using GameSystemObjects.ControllerModels;
using GameSystemObjects.Players;
using System;
using System.Collections.Generic;
using System.Text;
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
                username = "laksdjhckajypasodkfadsfpoiausdfoadspiuf",
                password = "Test",
            };

            var result = await m_playerRepository.CreatePlayer(testLogin);
            result.Should().Be(0);

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

        IPlayerRepository m_playerRepository;
    }
}
