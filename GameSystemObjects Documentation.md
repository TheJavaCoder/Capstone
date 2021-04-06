# GameStytemObjects Documentation
Why it exists and why certain abstractions were created.
## Controller Models

#### PlayerLoginModel
**PlayerLoginModel** - Class to hold data used to login a user. Holds the player_ID, username, and password.


#### PlayerItemActionModel
Holds a enumerator and class.
**PlayerItemActionModel** - Class that holds data for communicating actions players take on certain task. 
enum **Action** - Represents possible actions for each task.


## Game

#### Game.cs
* Gameloop
* UpdatePlayerGameSpeed
* GameSave
* CleanUpSessions
* Game
* GameState

#### GameConfig.cs

#### GameStat.cs
Tacks game statistics.

#### GameStatRepository.cs

#### IGameStatRepository.cs

## Players

#### IPlayerRepository
PlayerRepository interface description

#### PlayerRepository
Defines tasks in PlayerRepository

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