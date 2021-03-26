using System.Collections.Generic;
using System.Linq;

namespace GameSystemObjects.Game
{
    public class GameStat
    {
        public static GameStat current { get; set; }

        static GameStat()
        {
            current = new GameStat();
        }

        public ulong numPlayers { get; set; } = 0;

        public ulong SeesionUptime { get; set; } = 0;

        public ulong ServerUptime { get; set; } = 0;

        public void incrementUptime(ulong amount)
        {
            GameStat.current.SeesionUptime += amount;
            GameStat.current.ServerUptime += amount;
        }

        public Dictionary<int, ItemStat> globalItemTaskStats { get; set; } = new Dictionary<int, ItemStat>();

        public Dictionary<int, KeyValuePair<int, long>> globalItemTaskLeaderBoard()
        {
            if (GameStat.current.globalItemTaskStats == null || GameStat.current.globalItemTaskStats.Count == 0)
                return new Dictionary<int, KeyValuePair<int, long>>();

            var leaderBoardOfItems = new Dictionary<int, KeyValuePair<int, long>>();
            GameStat.current.globalItemTaskStats.Select(item =>
           {
               var sorted = item.Value.leaderBoard.OrderBy(k => k.Value);

               leaderBoardOfItems.Add(item.Key, KeyValuePair.Create(sorted.ElementAt(0).Key, sorted.ElementAt(0).Value));

               return item;
           });

            return leaderBoardOfItems;
        }
    }

    public class ItemStat
    {
        public Dictionary<int, long> leaderBoard { get; set; } = new Dictionary<int, long>();

        public bool madeLeaderboard(int pId, long itemAmount)
        {
            if (leaderBoard.Count <= 100)
            {
                leaderBoard.Add(pId, itemAmount);
                return true;
            }

            var userpassed = -1;
            leaderBoard.OrderBy(k => k.Value).Select(k => k.Value < itemAmount ? userpassed = k.Key : -1);

            if (userpassed == -1)
                return false;

            leaderBoard.Remove(userpassed);
            leaderBoard.Add(pId, itemAmount);

            return true;
        }

        public ulong numberMined { get; set; }
    }

}
