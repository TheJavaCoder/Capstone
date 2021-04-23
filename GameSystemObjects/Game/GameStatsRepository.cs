using Dapper;
using GameSystemObjects.Configuration;
using GameSystemObjects.Items;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GameSystemObjects.Game
{
    public class GameStatsRepository : IGameStatsRepository
    {
        public GameStatsRepository(IOptions<CommonConfiguration> options)
        {
            m_connectionString = options.Value.DatabaseConnection;
        }

        public GameStatsRepository(String conString)
        {
            m_connectionString = conString;
        }

        public async Task<IEnumerable<LeaderboardItem>> getLeaderboardForItem(int itemID)
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                return await c.QueryAsync<LeaderboardItem>(
                    @$"SELECT dbo.Player.username, dbo.Inventory.amount, dbo.Items.item_name FROM dbo.Inventory
                        JOIN dbo.Player ON dbo.Player.player_ID = dbo.Inventory.player_id
                        JOIN dbo.Items ON dbo.Items.items_id = dbo.Inventory.inventory_item
                        WHERE dbo.Items.items_id = {itemID}");
            }
        }

        private string m_connectionString;
    }
}
