using Dapper;
using GameSystemObjects.Configuration;
using GameSystemObjects.ControllerModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    public class PlayerRepository : IPlayerRepository
    {

        public PlayerRepository(IOptions<CommonConfiguration> options)
        {
            m_connectionString = options.Value.DatabaseConnection;
        }

        public PlayerRepository(String conString)
        {
            m_connectionString = conString;
        }

        public async Task<Player> GetPlayer(string name)
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                var playerDB = await c.QuerySingleOrDefaultAsync<PlayerLoginModel>("spSELECT_dbo_Player_With_Params", param: new { username = name }, commandType: System.Data.CommandType.StoredProcedure);

                if (playerDB == null)
                    return null;

                var inventory = await c.QueryAsync<itemTaskModel>("spSELECT_dbo_Inventory_With_Params", param: new { playerDB.player_ID } , commandType: System.Data.CommandType.StoredProcedure);

                var itemTasks = new List<ItemTask>();

                inventory.ToList().ForEach(i => itemTasks.Add( new ItemTask(i) ));

                return new Player(itemTasks, name);
            }
        }

        public async Task SavePlayer(Player p)
        {
            // Save the player's current task, not all tasks.
            // This needs to be called any time the task is switched

            var item = p.getEnabledTask();

            using (var c = new SqlConnection(m_connectionString))
            {
                await c.QueryAsync("UPDATE dbo.Inventory " +
                             "SET amount = " + item.itemAmount + 
                             " WHERE player_id = " + p.Id +
                             " AND inventory_id = " + item.taskId);
            }
        }

        public async Task<IEnumerable<ItemTask>> GetDefaultItemsAsync()
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                var items = await c.QueryAsync<ItemTask>("Select * FROM dbo.Items");
                return items;
            }
        }

        public async Task<bool> loginPlayer(PlayerLoginModel playerLoginModel)
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                var player = await c.QuerySingleOrDefaultAsync<PlayerLoginModel>(sql: "spSELECT_dbo_Player_With_Params", param: new { username = playerLoginModel.username }, commandType: CommandType.StoredProcedure);

                if (player == null)
                {
                    await CreatePlayer(playerLoginModel);
                    return true;
                }

                if (playerLoginModel.password != player.password)
                    return false;

                return true;
            }
        }

        public async Task<int> CreatePlayer(PlayerLoginModel p)
        {
            using (var c = new SqlConnection(m_connectionString))
                return await c.QueryFirstOrDefaultAsync<int>("spINSERT_dbo_Player", param: new { p.username, p.password }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task RemovePlayer(string player)
        {
            using (var c = new SqlConnection(m_connectionString))
                await c.QueryAsync($"DELETE FROM dbo.Player WHERE username = '{player}'");
        }

        public Task GetStats(string player)
        {
            throw new NotImplementedException();
        }

        private String m_connectionString;
    }
}
