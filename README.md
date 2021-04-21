# Capstone
Gwinnett Technical College Capstone C# dotnet core project. We are making an idle game similar to the likes of Melvor Idle https://melvoridle.com/.

# Members

John Garner, 
Tyler Stanton, 
Placidie Mugabo,
Bailey Costello,
Alan Farmer

<<<<<<< HEAD
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
=======
## Installation

If you are starting a project from scratch and will host the code on Github, hit the "Use this template" button above the code to get started. If you will host elsewhere, clone this repo and start your project from there.

## Usage

These files only change how your project behaves on github, and most of them will only take effect once merged into your default branch (usually `master` or `dev`).

Keep them up-to-date as your project evolves.

# Contents

Here's a rundown of the files included, as well as why they're important:

## clicker



## GameSystemObjects



## GameSystemObjectsTest


## WPF_Clicker

This is our GUI for our project. We are using WPF with C# ASP.net core framework to show for our front-end. 


### App.xaml


### Leaderboard.xaml


### loginPage.xaml


### MainWindow.xaml


### Settings.xaml


### Stats.xaml


### taskList.xaml

>>>>>>> a67432d (updated content for formatting)
