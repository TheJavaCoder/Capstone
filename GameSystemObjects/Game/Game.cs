using GameSystemObjects.Players;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystemObjects
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
                            p.IncrementItem(currentTask.itemName);
                    }
                }

                // Game loop every 1/30th of a second.
                await Task.Delay(33);
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

        public CleanUpSessions(IPlayerRepository playerRepository)
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

                        if (p.lastSeenTime.AddMinutes(1) < DateTime.Now)
                        {
                            playerRepository.SavePlayer(p);
                            GameState.current.players.TryRemove(p.name, out _);
                        }
                    }
                }

                // Remove client if they haven't been seen (or better yet heard for more than a min) and attempt to save their current state.
                Thread.Sleep(61000);
            }

        }

    }

    public class Game : IHostedService
    {
        // Need a link to the current Repository Singleton instance
        IPlayerRepository playerRepository;

        // Threads to run and save the game
        Thread gameThread;
        Thread saveThread;
        Thread cleanUpSessions;

        public Game() { }

        // Passing in the repository instance
        public Game(IServiceProvider serviceCollection)
        {
            // Commented out as this doesn't exist yet.
            //playerRepository = serviceCollection.GetRequiredService<IPlayerRepository>();
        }


        // Building both threads.
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GameLoop gl = new GameLoop();
            gameThread = new Thread(new ThreadStart(gl.run));
            gameThread.Start();

            //GameSave gs = new GameSave(playerRepository);
            //saveThread = new Thread(new ThreadStart(gs.run));
            //saveThread.Start();


        }

        // Clean up the threads. Should never get called.
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            gameThread.Suspend();
            //saveThread.Suspend();
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
