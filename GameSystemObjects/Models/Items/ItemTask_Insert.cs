using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GameSystemObjects.Models.Items
{
    [Serializable]
    public class ItemTask_Insert
    {

        public ItemTask_Insert(ItemTask i)
        {
            player_id = i.player_id;
            inventory_item = i.taskId;
            amount =  (int) i.itemAmount;
            resourceGatheringLevel = i.resourceGatheringLevel;
        }

        public int player_id { get; set; }

        public int inventory_item { get; set; }

        public int amount { get; set; }

        public int resourceGatheringLevel { get; set; }

        public Byte enabled = 0;
    }
}
