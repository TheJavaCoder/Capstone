using System;

namespace GameSystemObjects.ControllerModels
{
    [Serializable]
    public enum Action
    {
        ENABLE, DISABLE, BUY, SELL, UPGRADE
    }

    [Serializable]
    public class PlayerItemActionModel
    {
        public string player { get; set; }
        public string item { get; set; }
        public string action { get; set; }
    }
}
