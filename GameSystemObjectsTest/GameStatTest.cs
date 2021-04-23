using FluentAssertions;
using GameSystemObjects.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameSystemObjectsTest
{
    public class GameStatTest
    {

        [Fact]
        public Task UpdateLiveLeaderBoard_Should()
        {

            GameStat.current.UpdateLiveLeaderBoard("Test", 1, 1);
            
            GameStat.current.liveServerLeaderBoard.Count.Should().BeGreaterThan(0);

            GameStat.current.UpdateLiveLeaderBoard("Test", 1, 1);

            GameStat.current.liveServerLeaderBoard.TryGetValue(1, out var item);

            item.leaderBoard.TryGetValue("Test", out var pl);

            pl.Should().Be(2);

            return Task.CompletedTask;
        } 


    }
}
