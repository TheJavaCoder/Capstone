
using FluentAssertions;
using GameSystemObjects.Game;
using GameSystemObjects.Players;
using System.Threading.Tasks;
using Xunit;

namespace GameSystemObjectsTest
{
    public class GameConfigTest
    {

        IPlayerRepository m_playerRepository;

        public GameConfigTest()
        {
            m_playerRepository = new PlayerRepository("data source=tcp:s20.winhost.com;initial catalog=DB_111206_clicker;persist security info=True;user id=DB_111206_clicker_user;password=gtc2021;MultipleActiveResultSets=True;");
            new GameConfig(m_playerRepository);
        }

        [Fact]
        public async Task LoadConfig()
        {
            await GameConfig.init();

            GameConfig.DefaultItems.Count.Should().BeGreaterThan(0);
        }
    }
}
