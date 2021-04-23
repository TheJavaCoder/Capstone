using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystemObjects.ControllerModels
{
    [Serializable]
    public class itemTaskModel_Return
    {
        public int inventory_item { get; set; }
        public string item_name { get; set; }
        public string icon { get; set; }
        public int amount { get; set; }
        public int resourceGatheringLevel { get; set; }

        public string calc { get; set; }
    }
}
