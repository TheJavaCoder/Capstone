using GameSystemObjects.Game;
using Microsoft.AspNetCore.Mvc;
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

    }
}
