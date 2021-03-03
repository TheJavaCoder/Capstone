using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystemObjects.ControllerModels
{
    public enum Action
    {
        ENABLE, DISABLE, BUY, SELL, UPGRADE
    }

    public class PlayerItemActionModel
    { 
        public string player { get; set; }
        public string item { get; set; }
        public Action action { get; set; }
    }
}
