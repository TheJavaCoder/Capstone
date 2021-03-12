using GameSystemObjects.ControllerModels;
using System.Threading.Tasks;


namespace GameSystemObjects.Players
{
    public interface IPlayerRepository
    {
        public Task<bool> loginPlayer(PlayerLoginModel playerLoginModel);
        public Task<Player> GetPlayer(string name);
        public Task SavePlayer(Player p);

    }
}
