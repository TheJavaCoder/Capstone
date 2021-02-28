using System;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    class PlayerRepository : IPlayerRepository
    {
        private string connectionSting = "";

        public PlayerRepository(string c)
        {
            connectionSting = c;
        }

        public Task<Player> GetPlayer(string name)
        {

            throw new NotImplementedException();
        }

        public Task SavePlayer(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
