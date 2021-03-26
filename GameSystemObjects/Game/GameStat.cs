using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSystemObjects.Game
{
    public class GameStat
    {
        public ulong numPlayers { get; set; }

        public ulong minsPlayed { get; set; }

        public Dictionary<int, ItemStat> globalItemTaskStats;

        public Dictionary<int, KeyValuePair<int, long>> globalItemTaskLeaderBoard()
        {
            var leaderBoardOfItems = new Dictionary<int, KeyValuePair<int, long>>();
            globalItemTaskStats.Select( item =>
            {
                var sorted = item.Value.leaderBoard.OrderBy(k => k.Value);

                leaderBoardOfItems.Add(item.Key, KeyValuePair.Create(sorted.ElementAt(0).Key, sorted.ElementAt(0).Value));

                return item;
            } );

            return leaderBoardOfItems;
        }
    }

    public class ItemStat
    {
        public Dictionary<int, long> leaderBoard { get; set; }

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
