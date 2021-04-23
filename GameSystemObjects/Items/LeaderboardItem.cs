using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystemObjects.Items
{
    [Serializable]
    public class LeaderboardItem
    {
        public string username { get; set; }

        public string item_name { get; set; }

        public int amount { get; set; }
    }
}
