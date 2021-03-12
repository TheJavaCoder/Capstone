
using Dapper;
using GameSystemObjects.ControllerModels;
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

        public async Task<bool> loginPlayer(PlayerLoginModel playerLoginModel)
        {
            using (var c = new SqlConnection(m_connectionString))
            {
                var player = await c.QuerySingleOrDefaultAsync<PlayerLoginModel>(sql: "spSELECT_dbo_Player_With_Params", param: new { username = playerLoginModel.username }, commandType: CommandType.StoredProcedure);
                
                if (player == null || playerLoginModel.password != player.password)
                    return false;

                return true;
            }
        }

        public Task SavePlayer(Player p)
        {
            throw new NotImplementedException();
        }

        private String m_connectionString;
    }
}
