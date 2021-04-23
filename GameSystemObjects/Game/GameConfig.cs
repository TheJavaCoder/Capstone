using GameSystemObjects.Players;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystemObjects.Game
{
    public class GameConfig : IHostedService
    {
        public static Dictionary<int, ItemTask> DefaultItems { get; set; }

        public static double gameSpeed { get; set; } = 1;

        private static IPlayerRepository playerRepository;

        public GameConfig(IPlayerRepository PlayerRepository)
        {
            playerRepository = PlayerRepository;
        }

        public static async Task init()
        {
            var defaultItems = await playerRepository.GetDefaultItemsAsync();
            var dictionaryOfItems = new Dictionary<int, ItemTask>();

            defaultItems.All(it => {
                dictionaryOfItems.Add(((ItemTask)it).taskId, (ItemTask)it);
                return true;
                });
            DefaultItems = dictionaryOfItems;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await init();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DefaultItems = null;
        }
    }

}
