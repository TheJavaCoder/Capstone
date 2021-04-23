using GameSystemObjects.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSystemObjects.Game
{
    public interface IGameStatsRepository
    {

        Task<IEnumerable<LeaderboardItem>> getLeaderboardForItem(int itemID);

    }
}
