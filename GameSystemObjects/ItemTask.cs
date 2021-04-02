using GameSystemObjects.ControllerModels;
using System;
using System.ComponentModel;

namespace GameSystemObjects
{
    [Serializable]
    public class ItemTask
    {
        [Description("inventory_item")]
        public int taskId { get; set; }

        [Description("item_name")]
        public string itemName { get; set; }

        [Description("icon")]
        public string itemIcon { get; set; }

        public int resourceGatheringLevel { get; set; } = 1;

        [Description("amount")]
        public long itemAmount { get; set; } = 0;

        // the last time an item was gathered
        public long lastStartedTime { get; set; }

        // the time it takes an item to be gathered
        [Description("calc")]
        public long timeCalc { get; set; }

        public bool enabled { get; set; }

        public ItemTask() { }

        public ItemTask(itemTaskModel itemtaskModel)
        {
            this.itemName = itemtaskModel.item_name;
            this.taskId = itemtaskModel.inventory_item;
            this.resourceGatheringLevel = itemtaskModel.resourceGatheringLevel;
            this.timeCalc = long.Parse( itemtaskModel.calc );
            this.itemAmount = itemtaskModel.amount;
            this.itemIcon = itemtaskModel.icon;
        }

        public int upgradeGatheringLevelCost()
        {
            return Convert.ToInt32(resourceGatheringLevel * 2);
        }

    }
}