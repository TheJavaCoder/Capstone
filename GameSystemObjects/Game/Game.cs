using GameSystemObjects.Players;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystemObjects.Game
{
    public class GameLoop
    {
        public async void run()
        {

            while (true)
            {
                // There are no players to run update state for...
                if (!GameState.current.players.IsEmpty)
                {

                    // Loop through all the players.
                    foreach (string key in GameState.current.players.Keys)
                    {
                        Player p;
                        GameState.current.players.TryGetValue(key, out p);

                        // Get their current task
                        ItemTask currentTask = p.getEnabledTask();

                        // Increment that Item
                        if (currentTask != null)
                        {
                            p.IncrementItem(currentTask.itemName);
                            GameStat.current.UpdateLiveLeaderBoard(p.name, currentTask.taskId, p.getEnabledTask().resourceGatheringLevel);
                        }
                    }
                }

                // Game loop every 1/30th of a second.
                UpdatePlayerGameSpeed();
                await Task.Delay((int)(33 * GameConfig.gameSpeed));
            }

        }

        private void UpdatePlayerGameSpeed()
        {
            if (!GameState.current.players.IsEmpty)
            {
                foreach (string key in GameState.current.players.Keys)
                {
                    Player p;
                    GameState.current.players.TryGetValue(key, out p);

                    if (p == null || p.items.Count <= 0 || GameConfig.DefaultItems == null)
                        return;

                    var firstItem = p.items[0];

                    if (!GameConfig.DefaultItems.ContainsKey(firstItem.taskId))
                        return;

                    GameConfig.DefaultItems.TryGetValue(firstItem.taskId, out var regularItem);

                    if (p.items[0].timeCalc != (long)(regularItem.timeCalc * GameConfig.gameSpeed))
                    {
                        p.items.ForEach(i => i.timeCalc = (long)(regularItem.timeCalc * GameConfig.gameSpeed));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    public class GameSave
    {
        IPlayerRepository playerRepository;

        // Getting the repository instance that this thread needs to use.
        public GameSave(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public void run()
        {

            while (true)
            {
                if (!GameState.current.players.IsEmpty)
                {

                    foreach (string key in GameState.current.players.Keys)
                    {
                        Player p;
                        GameState.current.players.TryGetValue(key, out p);
                        playerRepository.SavePlayer(p);
                    }

                }

                // Saves all the players every 30 seconds
                Thread.Sleep(30000);
            }

        }
    }

    public class CleanUpSessions
    {
        IPlayerRepository playerRepository;

        public CleanUpSessions()
        {

        }

        public CleanUpSessions(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async void run()
        {

            while (true)
            {
                if (!GameState.current.players.IsEmpty)
                {
                    foreach (string key in GameState.current.players.Keys)
                    {
                        Player p;
                        GameState.current.players.TryGetValue(key, out p);

                        if (p == null)
                            continue;

                        p.stats.totalAmountPlayed++;

                        if (p.lastSeenTime.AddMinutes(1) < DateTime.Now)
                        {
                            if (playerRepository != null)
                                playerRepository.SavePlayer(p);
                            GameState.current.players.TryRemove(p.name, out _);
                            GameStat.current.numPlayers--;
                        }
                    }
                }

                // Remove client if they haven't been seen (or better yet heard for more than a min) and attempt to save their current state.
                await Task.Delay(60000);
                GameStat.current.incrementUptime(1);
            }

        }

    }

    public class UpdateGameStats
    {

        IGameStatsRepository gameStatsRepository;

        public UpdateGameStats()
        {

        }

        public UpdateGameStats(IGameStatsRepository gameStats)
        {
            this.gameStatsRepository = gameStats;
        }

        public async void run()
        {
            while(true)
            {
                if(gameStatsRepository != null)
                {
                    //GameStat.current.globalLeaderboard = gameStatsRepository
                }

                // Once an hour update the global leaderboard
                await Task.Delay(3600000);
            }
        }

    }

    public class Game : IHostedService
    {
        // Need a link to the current Repository Singleton instance
        IPlayerRepository playerRepository;
        IGameStatsRepository GameStatsRepository;

        // Threads to run and save the game
        Thread gameThread;
        Thread saveThread;
        Thread cleanUpSessions;
        Thread updateGameStats;

        public Game() { }

        // Passing in the repository instance
        public Game(IServiceProvider serviceCollection)
        {
            
            playerRepository = serviceCollection.GetRequiredService<IPlayerRepository>();
        }


        // Building threads.
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GameLoop gl = new GameLoop();
            gameThread = new Thread(new ThreadStart(gl.run));
            gameThread.Start();

            GameSave gs = new GameSave(playerRepository);
            saveThread = new Thread(new ThreadStart(gs.run));
            saveThread.Start();

            CleanUpSessions cus = new CleanUpSessions();
            cleanUpSessions = new Thread(new ThreadStart(cus.run));
            cleanUpSessions.Start();

            UpdateGameStats stats = new UpdateGameStats();
            updateGameStats = new Thread(new ThreadStart(stats.run));
            updateGameStats.Start();

        }

        // Clean up the threads. Should never get called.
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            gameThread.Suspend();
            saveThread.Suspend();
        }
    }

    // This is a static cache object.
    public class GameState
    {
        // The current static gamestate object Called by: GameState.current
        public static GameState current { get; set; }

        static GameState()
        {
            //Init
            current = new GameState { players = new ConcurrentDictionary<string, Player>(), };
        }

        // Thread safe list
        public ConcurrentDictionary<string, Player> players { get; set; }

        public static async Task<Player> GetPlayer(string name)
        {
            Player player;
            GameState.current.players.TryGetValue(name, out player);
            return player;
        }

    }
}
