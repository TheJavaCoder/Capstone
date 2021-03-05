﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<Player>> LoginAsync() 
        {
            Player p = new Player(new List<ItemTask>
            {
                new ItemTask
                {
                    itemName = "testItem",
                    enabled = true,
                    resourceGatheringLevel = 1,
                    timeCalc = 60000000,
                },

            }, "Test"); ;

            p.lastSeenTime = DateTime.Now;

            GameState.current.players.TryAdd(p.name, p);

            return p;
        }

        [HttpGet]
        public async Task<ActionResult<Player>> GetPlayerAsync()
        {

            Player p = await GameState.GetPlayer("Test");

            p.lastSeenTime = DateTime.Now;

            if (p != null)
            {
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
