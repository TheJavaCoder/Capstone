using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystemObjects.Models.Items
{
    [Serializable]
    public class DefaultTask
    {
        public int items_id { get; set; }

        public string item_name { get; set; }

        public string icon { get; set; }

        public string calc { get; set; }

    }
}
