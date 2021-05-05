using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameSystemObjects.Players
{
    [Serializable]
    public class Player
    {
        public int _id { get; set; }

        public string name { get; set; }

        public string profilePic { get; set; }

        [Required]
        public List<ItemTask> items { get; set; }

        public PlayerStats stats { get; set; }

        public DateTime lastSeenTime { get; set; }
        


        public Player()
        {

        }

        public Player(string name)
        {
            lastSeenTime = DateTime.Now;
            this.name = name;
            items = new List<ItemTask>();
            stats = new PlayerStats();
        }

        public Player(List<ItemTask> dbItems, string name)
        {
            this.name = name;
            items = dbItems;
            stats = new PlayerStats();
        }

        public async Task<bool> IncrementItem(string item)
        {
            ItemTask foundItem = getItem(item);

            if (foundItem == null)
                return false;

            if (foundItem.lastStartedTime == null || foundItem.lastStartedTime == 0)
            {
                foundItem.lastStartedTime = DateTime.Now.Ticks;
                return false;
            }

            if (foundItem.lastStartedTime + foundItem.timeCalc < DateTime.Now.Ticks)
            {
                foundItem.itemAmount += foundItem.resourceGatheringLevel;
                foundItem.lastStartedTime = DateTime.Now.Ticks;
            }

            return true;
        }


        public async Task<bool> UpgradeGatheringLevel(string item)
        {
            ItemTask foundItem = getItem(item);

            if (foundItem == null)
                return false;

            if (foundItem.upgradeGatheringLevelCost() > foundItem.itemAmount)
                return false;

            foundItem.itemAmount -= foundItem.upgradeGatheringLevelCost();
            foundItem.resourceGatheringLevel++;

            return true;
        }

        public List<ItemTask> GetItems()
        {
            return items;
        }

        public ItemTask getItem(string name)
        {
            return items.Where(i => i.itemName.Replace(" ", "") == name.Replace(" ", "")).FirstOrDefault();
        }

        public ItemTask getEnabledTask()
        {
            return items.Where(i => i.enabled == true).FirstOrDefault();
        }

        public async Task<bool> disableTask()
        {
            ItemTask currentlyEnabled = getEnabledTask();
            if (currentlyEnabled == null)
                return false;

            currentlyEnabled.enabled = false;
            return true;
        }

        public async Task<bool> switchEnabledTask(string name)
        {
            ItemTask currentlyEnabled = getEnabledTask();
            ItemTask toBeEnabled = getItem(name);

            if (toBeEnabled == null)
                return false;

            if (currentlyEnabled == null)
            {
                toBeEnabled.enabled = true;
                return true;
            }

            currentlyEnabled.enabled = false;
            toBeEnabled.enabled = true;

            return true;

        }
    }


}
