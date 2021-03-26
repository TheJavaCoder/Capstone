using GameSystemObjects.Game;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clicker.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        [HttpGet]
        public async Task<GameStat> GetPlayerStats(string name)
        {
            throw new NotImplementedException();
        }
    }
}
