using GameSystemObjects.ControllerModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    public interface IPlayerRepository
    {
        public bool loginPlayer(PlayerLoginModel playerLoginModel);
        public Task<Player> GetPlayer(string name);
        public Task SavePlayer(Player p);

    }
}
