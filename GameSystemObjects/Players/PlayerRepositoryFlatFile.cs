using GameSystemObjects.ControllerModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    class PlayerRepositoryFlatFile : IPlayerRepository
    {
        private string filePath = "";
        private IEnumerable<ItemTask> items;

        public PlayerRepositoryFlatFile(string filePath)
        {
            this.filePath = filePath;
            this.items = new List<ItemTask>();
        }

        private void init()
        {
            // Check to see what files exist and which ones need to be.
        }

        private void createRequiredFiles()
        {
            var filesList = new List<string>
            {
                "Players",
                "Inventory",
                "Items",
            };
        }

        public Task<int> CreatePlayer(PlayerLoginModel p)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemTask>> GetDefaultItemsAsync()
        {
            return (Task<IEnumerable<ItemTask>>)items;
        }

        public Task<Player> GetPlayer(string name)
        {
            throw new NotImplementedException();
        }

        public Task GetStats(string player)
        {
            throw new NotImplementedException();
        }

        public Task<bool> loginPlayer(PlayerLoginModel playerLoginModel)
        {
            throw new NotImplementedException();
        }

        public Task RemovePlayer(string player)
        {
            throw new NotImplementedException();
        }

        public Task SavePlayer(Player p)
        {
            throw new NotImplementedException();
        }

        private class Props
        {
            public Props(Type type, string property)
            {
                Type = type;
                Property = property == null ? null : typeof(Player).GetProperty(property);
            }

            public Type Type { get; }

            public PropertyInfo Property { get; }
        }

        private Dictionary<string, List<Props>> tableDefinitions = new Dictionary<string, List<Props>>()
        {
            {
                "Players", new List<Props>()
                {
                    new Props(typeof(int), nameof(PlayerLoginModel.player_ID)),
                    new Props(typeof(string), nameof(PlayerLoginModel.username)),
                    new Props(typeof(string), nameof(PlayerLoginModel.password))
                }
            },
            {
                "Items", new List<Props>()
                {
                    new Props(typeof(int), nameof(ItemTask.taskId)),
                    new Props(typeof(string), nameof(ItemTask.itemName)),
                    new Props(typeof(string), nameof(ItemTask.itemIcon)),
                    new Props(typeof(string), nameof(ItemTask.timeCalc)),
                }
            },
            {
                "Inventory", new List<Props>()
                {
                    new Props(typeof(int), nameof(Player._id)),
                    new Props(typeof(int), nameof(ItemTask.taskId)),
                    new Props(typeof(int), nameof(ItemTask.itemAmount)),
                    new Props(typeof(int), nameof(ItemTask.resourceGatheringLevel)),
                }
            }
        };
    }
}
