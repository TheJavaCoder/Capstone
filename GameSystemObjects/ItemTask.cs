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

        public int resourceGatheringLevel { get; set; } = 1;

        public long itemAmount { get; set; } = 0;

        // the last time an item was gathered
        public long lastStartedTime { get; set; }

        // the time it takes an item to be gathered
        public long timeCalc { get; set; }

        public bool enabled { get; set; }

        public int upgradeGatheringLevelCost()
        {
            return Convert.ToInt32(resourceGatheringLevel * 2);
        }

    }
}