using Dapper;
using GameSystemObjects.ControllerModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    class PlayerRepository : IPlayerRepository
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

        public bool loginPlayer(PlayerLoginModel playerLoginModel)
        {
            throw new NotImplementedException();
        }

        public Task SavePlayer(Player p)
        {
            throw new NotImplementedException();
        }

        private String m_connectionString;
    }
}
