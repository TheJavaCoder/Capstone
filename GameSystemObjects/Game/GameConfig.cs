using GameSystemObjects.Players;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSystemObjects.Game
{
    public class GameConfig 
    {
        public static Dictionary<int, ItemTask> DefaultItems { get; set; }

        private static IPlayerRepository playerRepository;

        public GameConfig(IPlayerRepository PlayerRepository)
        {
            playerRepository = PlayerRepository;
        }

        public static async Task init()
        {
            var defaultItems = await playerRepository.GetDefaultItemsAsync();
            var dictionaryOfItems = new Dictionary<int, ItemTask>();

            defaultItems.AsParallel().ForAll(it => dictionaryOfItems.Add(((ItemTask)it).taskId, (ItemTask)it));
            DefaultItems = dictionaryOfItems;
        }
    }
}
