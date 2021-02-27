using GameSystemObjects.Players;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystemObjects
{
    public class GameLoop
    {
        public void run()
        {

            while (true)
            {
                // There are no players to run update state for...
                if (!GameState.current.players.IsEmpty)
                {

                    // Loop through all the players.
                    foreach (Player p in GameState.current.players)
                    {
                        // Get their current task
                        ItemTask currentTask = p.getEnabledTask();

                        // Increment that Item
                        if(currentTask != null)
                            p.IncrementItem(currentTask.itemName);
                    }
                }

                // Game loop....
                Thread.Sleep(33);
            }

        }
    }

    public class GameSave
    {
        IPlayerRepository playerRepository;

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

                    foreach (Player p in GameState.current.players)
                    {
                        playerRepository.SavePlayer(p);
                    }

                }

                Thread.Sleep(30000);
            }

        }   
    }

    public class Game : IHostedService
    {
        IPlayerRepository playerRepository;

        Thread gameThread;
        Thread saveThread;

        public Game(IServiceProvider serviceCollection)
        {
            // Commented out as this doesn't exist yet.
            //playerRepository = serviceCollection.GetRequiredService<IPlayerRepository>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GameLoop gl = new GameLoop();
            gameThread = new Thread(new ThreadStart(gl.run));
            gameThread.Start();

            //GameSave gs = new GameSave(playerRepository);
            //saveThread = new Thread(new ThreadStart(gs.run));
            //saveThread.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            gameThread.Suspend();
            //saveThread.Suspend();
        }
    }

    // This is a static cache object.
    public class GameState 
    {
        public static GameState current { get; set; }

        static GameState() 
        {
            //Init
            current = new GameState { players = new ConcurrentBag<Player>(), };
        }

        // Thread safe list
        public ConcurrentBag<Player> players { get; set; }

        public async Task<Player> GetPlayer(string name)
        {
            foreach(Player p in players) {

                if (p.name == name)
                    return p;

            }

            return null;
        }

    }
}
