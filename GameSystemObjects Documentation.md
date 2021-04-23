# GameSystemObjects Documentation
Why it exists and why certain abstractions were created.
## Controller Models

#### PlayerLoginModel
**PlayerLoginModel** - Class to hold data used to login a user. Holds the player_ID, username, and password.


#### PlayerItemActionModel
Holds an enumerator and class.
**PlayerItemActionModel** - Class that holds data for communicating actions players take on certain task. 
enum **Action** - Represents possible actions for each task.


## Game

#### Game.cs
* **Gameloop** - Main game loop. While running, checks that there are players and loops through each player to increment all of their items.
* **UpdatePlayerGameSpeed** - Updates each players time calculation for their current item task.
* **GameSave** - Loops through each player and saves them to the player repository every 30 seconds.
* **CleanUpSessions** - Checks through each player to check if more than a minute has passed since their last seen time, if so, saves and trys to remove to player from the current repository.
* **Game** - Builds both the GameLoop and CleanUpSessions threads.
* **GameState** - Static cache object with a currnet gamestate and thread safe list.

#### GameConfig.cs
Defines objects:
* Dictionary DefaultItems
* double GameSpeed
* playerRepository
* class GameConfig
* Task init
* Task StartAsync
* Task StopAsync


#### GameStat.cs
Defines classes to track game statistics.
Class GameStat:
* numPlayer - number of players
* SessionUpTime - Players time online
* ServerUpTime - Time the server has been online
* globalItemTaskStats - Dictionary of ItemStat
* globalItemTaskLeaderBoard - Key value pair dictionary ot create a leaderboard of players items.

Class ItemStat - Defines a leaderboard using a dictionary with player ids and the amount of items.

#### GameStatRepository.cs

#### IGameStatRepository.cs

## Players

#### IPlayerRepository
PlayerRepository interface task object initialization

#### PlayerRepository
Defines task objects in PlayerRepository
* GetPlayer - Requests players information from the database
* SavePlayer - WIP
* GetDefaultItemsAsync - Requests default items from the database
* loginPlayer - Requests and compairs login information
* CreatePlayer - Adds a new player to the database
* RemovePlayer - Removes a player from the database
* GetStats - WIP

#### Player
Class for players information. Holds Task for interacting with the itemTasks

#### PlayerStats
Class, holds total time played.

### ItemTask
Object that holds all task data.
* **taskId** - Task ID for Database, int
* **itemName** - Item/Task name, string
* **itemIcon** - Location for Item/Task Icon, string
* **resourceGatheringLevel** - The upgrade level of the task, int
* **itemAmount** - Amount of this item this player has, long
* **lastStartedTime** - The last time an item was gathered, long
* **timeCalc** - The time it takes for the task to complete and add to itemAmount, long
* **enabled** - Whether the task is running or not, bool
* **upgradeGatheringLevelCost** - Method to determin the upgrade cost of this task, int
