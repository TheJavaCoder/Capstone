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

        // Only the leaders online on this server during the current session
        public ConcurrentDictionary<int, ItemStat> liveServerLeaderBoard { get; set; } = new ConcurrentDictionary<int, ItemStat>();

        public void UpdateLiveLeaderBoard(string playerID, int itemId, int itemAmountPerTick)
        {
            var itemStat = liveServerLeaderBoard.GetOrAdd(itemId, (s) =>
            {
                return new ItemStat();
            });

            itemStat.numberMined +=  Convert.ToUInt64 (itemAmountPerTick);

            itemStat.UpdateOrAddToLeaderBoard(playerID, itemAmountPerTick);
        }


        // Global Across All Servers Leaderboard
        public ConcurrentDictionary<int, ItemStat> globalLeaderboard { get; set; } = new ConcurrentDictionary<int, ItemStat>();

        public DateTime lastGlobalLeaderboardUpdate { get; set; }

        public void UpdateGlobalLeaderboard()
        {
            lastGlobalLeaderboardUpdate = DateTime.Now;
        }
        
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
