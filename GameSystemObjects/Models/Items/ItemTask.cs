using GameSystemObjects.ControllerModels;
using GameSystemObjects.Models.Items;
using System;
using System.ComponentModel;

namespace GameSystemObjects
{
    [Serializable]
    public class ItemTask
    {
        [Description("inventory_item")]
        public int taskId { get; set; }

        [Description("player_id")]
        public int player_id { get; set; }

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
        
        [Description("enabled")]
        public bool enabled { get; set; }

        public ItemTask() { }

        public ItemTask(itemTaskModel_Return itemtaskModel)
        {
            this.itemName = itemtaskModel.item_name;
            this.taskId = itemtaskModel.inventory_item;
            this.resourceGatheringLevel = itemtaskModel.resourceGatheringLevel;
            this.timeCalc = long.Parse( itemtaskModel.calc );
            this.itemAmount = itemtaskModel.amount;
            this.itemIcon = itemtaskModel.icon;
        }

        public ItemTask(DefaultTask defaultTask)
        {
            this.taskId = defaultTask.items_id;
            this.itemName = defaultTask.item_name;
            this.itemIcon = defaultTask.icon;
            this.timeCalc = long.Parse(defaultTask.calc);
        }

        public int upgradeGatheringLevelCost()
        {
            return Convert.ToInt32(resourceGatheringLevel * 2);
        }

    }
}