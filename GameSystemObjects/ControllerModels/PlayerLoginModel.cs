using System.ComponentModel;

namespace GameSystemObjects.ControllerModels
{
    
    public class PlayerLoginModel
    {
        [Description("player_ID")]
        public int player_ID { get; set; }
        [Description("username")]
        public string username { get; set; }
        [Description("password")]
        public string password { get; set; }
    }
}
