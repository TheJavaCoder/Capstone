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
