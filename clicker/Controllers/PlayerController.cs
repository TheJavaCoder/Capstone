using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSystemObjects;
using GameSystemObjects.Players;

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
        public async Task<ActionResult<bool>> LoginAsync() 
        {
            Player p = new Player(new List<ItemTask> 
            { 
                new ItemTask 
                { 
                    itemName = "testItem",
                },

            }, "Test");

            GameState.current.players.Add(p);

            return true;
        }

        [HttpGet]
        public async Task<ActionResult<Player>> GetPlayerAsync()
        {

            Player p = await GameState.current.GetPlayer("Test");

            if (p != null)
            {
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
