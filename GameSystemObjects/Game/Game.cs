using GameSystemObjects.Players;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystemObjects
{
    public class GameLoop
    {
        public void ExecuteAsync()
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

    }

    public class Game : IHostedService
    {
        Thread gameThread;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GameLoop gl = new GameLoop();
            gameThread = new Thread(new ThreadStart(gl.ExecuteAsync));
            gameThread.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            gameThread.Suspend();
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
