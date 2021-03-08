using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSystemObjects
{
    [Serializable]
    public class ItemTask
    {
        public int taskId { get; set; }

        public string itemName { get; set; }

        public string itemIcon { get; set; }

        public int resourceGatheringLevel { get; set; }

        public long itemAmount { get; set; }

        // the last time an item was gathered
        public long lastStartedTime { get; set; }

        // the time it takes an item to be gathered
        public string timeCalc { get; set; }

        public bool enabled { get; set; }

        public int upgradeGatheringLevelCost()
        {
            return Convert.ToInt32(resourceGatheringLevel * 2);
        }

    }
}