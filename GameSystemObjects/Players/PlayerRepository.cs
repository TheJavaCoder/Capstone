using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    class PlayerRepository : IPlayerRepository
    {
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
