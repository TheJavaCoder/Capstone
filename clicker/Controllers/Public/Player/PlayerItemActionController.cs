﻿using GameSystemObjects.ControllerModels;
using GameSystemObjects.Game;
using GameSystemObjects.Players;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Action = GameSystemObjects.ControllerModels.Action;

namespace clicker.Controllers
{
    [Route("api/action")]
    [ApiController]
    public class PlayerItemActionController : ControllerBase
    {

        public PlayerItemActionController()
        {

        }

        [HttpPost]
        public async Task<ActionResult<bool>> ExcuteActionAsync(PlayerItemActionModel playerItemAction)
        {
            Player p = await GameState.GetPlayer(playerItemAction.player);

            if (p == null)
                return false;

            p.lastSeenTime = DateTime.Now;

            bool output = false;

            Action action = (Action)Enum.Parse(typeof(Action), playerItemAction.action);

            switch (action)
            {
                case Action.ENABLE:
                    output = await p.switchEnabledTask(playerItemAction.item);
                    break;
                case Action.DISABLE:
                    output = await p.disableTask();
                    break;
                case Action.BUY:
                    break;
                case Action.SELL:
                    break;
                case Action.UPGRADE:
                    output = await p.UpgradeGatheringLevel(playerItemAction.item);
                    break;
                default:
                    return false;
            }

            return output;
        }

    }

}
