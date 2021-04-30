using GameSystemObjects.ControllerModels;
using GameSystemObjects.Game;
using GameSystemObjects.Players;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace clicker.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : Controller
    {

        public PlayerController(IPlayerRepository playerRepository)
        {
            m_PlayerRepository = playerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> LoginAsync(PlayerLoginModel playerLoginModel)
        {
            if (await m_PlayerRepository.loginPlayer(playerLoginModel) == true)
            {
                var player = await m_PlayerRepository.GetPlayer(playerLoginModel.username);

                if (player == null)
                    return null;

                player.lastSeenTime = DateTime.Now;

                GameState.current.players.TryAdd(playerLoginModel.username, player);
                GameStat.current.numPlayers++;

                return player;
            }
            return null;
        }

        [HttpGet]
        [Route("{playerName}")]
        public async Task<ActionResult<Player>> GetPlayerAsync(string playerName)
        {

            Player p = await GameState.GetPlayer(playerName);

            if (p != null)
            {
                p.lastSeenTime = DateTime.Now;
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Player was null");
            }

            return p;
        }

        [HttpPut]
        [Route("{playerName}")]
        public async Task<ActionResult<bool>> SaveAndRemove(string playerName)
        {

            Player p;
            GameState.current.players.TryRemove(playerName, out p);
            GameStat.current.numPlayers--;
            await m_PlayerRepository.SavePlayer(p);

            return true;
        }

        [HttpPost("profilePicture")]
        public async Task<bool> UploadProfilePic(IFormFile file)
        {
            var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        IPlayerRepository m_PlayerRepository;

    }
}
