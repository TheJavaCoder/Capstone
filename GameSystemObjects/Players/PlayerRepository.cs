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
                var players = await c.QueryAsync<Player>("storedprocedure-name", param: new { name }, commandType: System.Data.CommandType.StoredProcedure);
                return players.SingleOrDefault();
            }
        }

        public Task SavePlayer(Player p)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemTask>> GetDefaultItemsAsync() {
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

        private String m_connectionString;
    }
}
