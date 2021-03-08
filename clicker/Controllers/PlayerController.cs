using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSystemObjects;
using GameSystemObjects.Players;
using GameSystemObjects.ControllerModels;

namespace clicker.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        public PlayerController()
        {
            //m_PlayerRepository = playerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> LoginAsync(PlayerLoginModel playerLoginModel) 
        {
            Player p = new Player(new List<ItemTask> 
            { 
                new ItemTask 
                { 
                    itemName = "testItem",
                    //enabled = true,
                    itemIcon = "https://www.ctvnews.ca/polopoly_fs/1.4692108.1574174140!/httpImage/image.jpg_gen/derivatives/landscape_620/image.jpg",
                },
                new ItemTask
                {
                    itemName = "testItem",
                    //enabled = true,
                    itemIcon = "https://www.ctvnews.ca/polopoly_fs/1.4692108.1574174140!/httpImage/image.jpg_gen/derivatives/landscape_620/image.jpg",
                },
                new ItemTask
                {
                    itemName = "testItem",
                    //enabled = true,
                    itemIcon = "https://www.ctvnews.ca/polopoly_fs/1.4692108.1574174140!/httpImage/image.jpg_gen/derivatives/landscape_620/image.jpg",
                },

            }, playerLoginModel.name);

            p.lastSeenTime = DateTime.Now;

            GameState.current.players.TryAdd(p.name, p);

            return p;
        }
        //Logic for LogOut button
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
            else {
                Console.WriteLine("Player was null");
            }

            return p;
        }

        IPlayerRepository m_PlayerRepository;

    }
}
