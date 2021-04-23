using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GameSystemObjects.Game
{
    [Serializable]
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

        // Only the leaders online...
        public ConcurrentDictionary<int, ItemStat> liveLeaderBoard { get; set; } = new ConcurrentDictionary<int, ItemStat>();

        public void UpdateLiveLeaderBoard(string playerID, int itemId, int itemAmountPerTick)
        {

            var itemStat = liveLeaderBoard.GetOrAdd(itemId, (s) =>
            {
                return new ItemStat();
            });

            itemStat.numberMined +=  Convert.ToUInt64 (itemAmountPerTick);

            itemStat.UpdateOrAddToLeaderBoard(playerID, itemAmountPerTick);
        }


        //public Dictionary<int, KeyValuePair<int, long>> globalItemTaskLeaderBoard()
        //{
        //    if (GameStat.current.liveLeaderBoard == null || GameStat.current.liveLeaderBoard.Count == 0)
        //        return new Dictionary<int, KeyValuePair<int, long>>();

        //    var leaderBoardOfItems = new Dictionary<int, KeyValuePair<int, long>>();
        //    GameStat.current.liveLeaderBoard.Select(item =>
        //   {
        //       var sorted = item.Value.leaderBoard.OrderBy(k => k.Value);

        //       leaderBoardOfItems.Add(item.Key, KeyValuePair.Create(sorted.ElementAt(0).Key, sorted.ElementAt(0).Value));

        //       return item;
        //   });

        //    return leaderBoardOfItems;
        //}
    }

    [Serializable]
    public class ItemStat
    {
        public ConcurrentDictionary<string, long> leaderBoard { get; set; } = new ConcurrentDictionary<string, long>();

        public bool UpdateOrAddToLeaderBoard(string pId, long itemAmount)
        {
            if (!leaderBoard.ContainsKey(pId))
                return addToLeaderboard(pId, itemAmount);

            leaderBoard.TryGetValue(pId, out var item);

            leaderBoard.TryUpdate(pId, item + 1, item);
            return true;
        } 

        private bool addToLeaderboard(string pId, long itemAmount)
        {
            // This is if the leader board isn't full already
            if (leaderBoard.Count <= 100)
            {
                leaderBoard.TryAdd(pId, itemAmount);
                return true;
            }

            var userpassed = "";
            leaderBoard.Select(k => k.Value < itemAmount ? userpassed = k.Key : null);

            if (userpassed != null)
                return false;

            leaderBoard.TryRemove(userpassed, out _);
            leaderBoard.TryAdd(pId, itemAmount);

            return true;
        }

        public IOrderedEnumerable<KeyValuePair<string, long>> getLeaderBoard(int amount)
        {
            return leaderBoard.Take(amount).OrderBy(k => k.Value);
        }


        public ulong numberMined { get; set; }
    }

}
