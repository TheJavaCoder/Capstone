using GameSystemObjects;
using GameSystemObjects.Players;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace clicker.Controllers
{
    [Route("api/action")]
    [ApiController]
    public class PlayerItemActionController : ControllerBase
    {

        public PlayerItemActionController()
        {

        }

        public async Task<ActionResult<bool>> ExcuteActionAsync(PlayerItemAction playerItemAction)
        {
            Player p = await GameState.GetPlayer(playerItemAction.player);

            p.lastSeenTime = DateTime.Now;

            if (p == null)
                return false;

            bool output = true;
            
            switch(playerItemAction.action)
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
                    break;
                default:
                    return false;
            }

            return output;
        }

    }

    public enum Action
    {
        ENABLE, DISABLE, BUY, SELL, UPGRADE    
    }

    public class PlayerItemAction
    {
        public string player { get; set; }
        public string item { get; set; }
        public Action action { get; set; }
    }
}
