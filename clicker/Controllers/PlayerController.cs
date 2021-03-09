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
                    itemName = "Collect Wood",
                    taskId = 1,
                    //enabled = true,
                    itemIcon = "https://localhost:44339/images/woodTex.png",
                    itemAmount = 99999,
                    timeCalc = 500,
                },
                new ItemTask
                {
                    taskId = 2,
                    itemName = "Mine Stone",
                    //enabled = true,
                    itemIcon = "https://localhost:44339/images/rockTex.png",
                    timeCalc = 60000,
                },
                new ItemTask
                {
                    taskId = 3,
                    itemName = "Meow",
                    //enabled = true,
                    itemIcon = "https://www.ctvnews.ca/polopoly_fs/1.4692108.1574174140!/httpImage/image.jpg_gen/derivatives/landscape_620/image.jpg",
                    timeCalc = 60000,

                }
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

        // [HttpPut]
        //public async Task<ActionResult<bool>> SaveAndRemove()
        //{

        //    Player p;
        //    GameState.current.players.TryRemove(p.name, out p);
        //    await m_PlayerRepository.SavePlayer(p);

        //    return true;
        //}

        IPlayerRepository m_PlayerRepository;

    }
}
