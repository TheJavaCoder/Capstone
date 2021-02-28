using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    public interface IPlayerRepository
    {
        //Player Repository needs to be priotity #1
        public Task<Player> GetPlayer(string name);

        public Task SavePlayer(Player p);

    }
}
