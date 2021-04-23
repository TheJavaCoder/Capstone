using GameSystemObjects.Game;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clicker.Controllers.Public.GameStats
{
    [Route("api/gamestats")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {

        [HttpGet]
        public async Task<GameStat> GetAllGameStats()
        {
            return GameStat.current;
        }

        [HttpGet]
        [Route("api/gamestats/forItem")]
        public async Task<IOrderedEnumerable<KeyValuePair<string, long>>> GetGameStatsForItem(int itemId, int leaderboardRecords)
        {
            GameStat.current.liveLeaderBoard.TryGetValue(itemId, out var item);
            return await Task.FromResult(item.getLeaderBoard(leaderboardRecords));
        }

        [HttpGet]
        [Route("api/gamestats/allItems")]
        public async Task<Dictionary<int, IOrderedEnumerable<KeyValuePair<string, long>>>> GetAllItemStats(int leaderboardRecords)
        {
            var dictionary = new Dictionary<int, IOrderedEnumerable<KeyValuePair<string, long>>>();
            GameStat.current.liveLeaderBoard.All( (pair) =>
            {
                dictionary.Add(pair.Key, pair.Value.getLeaderBoard(leaderboardRecords));

                return true;
            });

            return dictionary;
        }
    }
}
