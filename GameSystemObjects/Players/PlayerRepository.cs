using Dapper;
using GameSystemObjects.Configuration;
using GameSystemObjects.ControllerModels;
using GameSystemObjects.Models.Items;
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

                var inventory = await c.QueryAsync<itemTaskModel_Return>("spSELECT_dbo_Inventory_With_Params", param: new { playerDB.player_ID } , commandType: System.Data.CommandType.StoredProcedure);

                var itemTasks = new List<ItemTask>();

                inventory.ToList().ForEach(i =>
                {
                    var it = new ItemTask(i);
                    it.player_id = playerDB.player_ID;
                    itemTasks.Add(it);
                });

                var playerReturn = new Player(itemTasks, name);
                playerReturn._id = playerDB.player_ID;
                return playerReturn;
            }
        }

        public async Task SavePlayer(Player p)
        {
            
               // Save the player's current task, not all tasks.
               // This needs to be called any time the task is switched

            using (var c = new SqlConnection(m_connectionString))
            {
                //await c.ExecuteAsync("spUPDATE_dbo_Inventory_with_TableType", items, commandType: CommandType.StoredProcedure);

                p.items.ForEach( async (i) =>
                {
                    await c.QueryAsync($@"UPDATE dbo.Inventory SET amount = {i.itemAmount} WHERE player_id = {p._id} AND inventory_item = {i.taskId}");
                });

            }
        }

        public async Task<IEnumerable<ItemTask>> GetDefaultItemsAsync()
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                var items = await c.QueryAsync<DefaultTask>("Select * FROM dbo.Items");

                var list = new List<ItemTask>();
                items.ToList().ForEach((i) => list.Add(new ItemTask(i)));

                return list;
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
            {
                var pID = await c.QueryFirstOrDefaultAsync<int>("spINSERT_dbo_Player", param: new { p.username, p.password }, commandType: System.Data.CommandType.StoredProcedure);

                var defaultItems = await GetDefaultItemsAsync();

                var items = defaultItems.ToList().Select(( ItemTask i, int idx) =>
                {
                    i.itemAmount = 1;
                    i.player_id = pID;
                    return new ItemTask_Insert(i);
                }).ToDataTable();

                try
                {
                    await c.ExecuteAsync("spINSERT_dbo_Inventory", param: new { Invent = items }, commandType: CommandType.StoredProcedure);
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                return pID;
            }
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
