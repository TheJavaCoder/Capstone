using FluentAssertions;
using GameSystemObjects;
using GameSystemObjects.Game;
using GameSystemObjects.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameSystemObjectsTest
{
    public class PlayerTest
    {
        public PlayerTest()
        {

            m_game = new Game();

            Init();
        }

        public async void Init()
        {
            await m_game.StartAsync(new System.Threading.CancellationToken());
        }

        [Fact]
        public async Task IncrementItem_ShouldIncrement()
        {
            await m_player.IncrementItem("Test");

            m_player.getItem("Test").itemAmount.Should().Be(1);
        }

        [Fact]
        public async Task UpgradeGatheringLevel_ShouldIncrease()
        {
            m_player.getItem("Test").itemAmount = 3;

            await m_player.UpgradeGatheringLevel("Test");

            m_player.getItem("Test").resourceGatheringLevel.Should().Be(2);
        }

        [Fact]
        public async Task SwitchEnabledTask_Should()
        {
            m_player.items.Add(secondItem);

            m_player.getEnabledTask().itemName.Should().Be("Test");

            await m_player.switchEnabledTask(secondItem.itemName);

            m_player.getEnabledTask().itemName.Should().Be(secondItem.itemName);
        }

        private readonly ItemTask secondItem = new ItemTask
        {
            itemName = "SecondItem",
            itemAmount = 1,
            lastStartedTime = 0,
            timeCalc = 10000,
        };

        private Player m_player = new Player
        {
            items = new List<ItemTask> {
                new ItemTask { itemName = "Test", itemAmount = 0, lastStartedTime = DateTime.Now.Ticks - 10000, timeCalc = 9000, enabled = true },
            },
            name = "MyName",
        };

        Game m_game;
    }
}
