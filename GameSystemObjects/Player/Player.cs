using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystemObjects
{

    public class Player
    {
        private string name;

        private List<ItemTask> items;

        public Player(string name)
        {
            this.name = name;
            items = new List<ItemTask>();
        }

        public Player(List<ItemTask> dbItems, string name)
        {
            this.name = name;
            items = dbItems;
        }

        public async Task IncrementItem(string item)
        {
            ItemTask foundItem = getItem(item);

            if (foundItem == null)
                return;

            foundItem.itemAmount++;
        }


        public async Task<bool> UpgradeGatheringLevel(string item, int amount)
        {
            ItemTask foundItem = getItem(item);

            if (foundItem == null)
                return false;

            if (foundItem.upgradeGatheringLevelCost() > amount)
                return false;

            foundItem.resourceGatheringLevel++;

            return true;
        }

        public async Task<List<ItemTask>> GetItems()
        {
            return items;
        }

        public ItemTask getItem(string name)
        {
            return items.Where(i => i.itemName == name).FirstOrDefault();
        }

        public ItemTask getEnabledTask()
        {
            return items.Where(i => i.enabled == true).FirstOrDefault();
        }

        public async Task<bool> switchEnabledTask(string name)
        {
            ItemTask currentlyEnabled = getEnabledTask();
            ItemTask toBeEnabled = getItem(name);

            if (currentlyEnabled == null || toBeEnabled == null)
                return false;

            currentlyEnabled.enabled = false;
            toBeEnabled.enabled = true;

            return true;

        }
    }


}
