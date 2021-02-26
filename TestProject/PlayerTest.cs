using GameSystemObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    class PlayerTest
    {

        public PlayerTest() 
        {
            Console.WriteLine("---- Player Unit Tests! ----");

            testIncrementItem_Correct();
            testUpgradeGatheringLevel();
            testNegativeUpgradeGatheringLevel();
            testSwitchEnabledTask();
            testNegativeSwitchEnabledTask();
        }

        static void testIncrementItem_Correct()
        {
            Player p = new Player(new List<ItemTask> {

                new ItemTask { itemName = "Test", itemAmount = 0 },

            }, "MyName");

            _ = p.IncrementItem("Test");

            if (p.getItem("Test").itemAmount == 1)
                Console.WriteLine("testIncrementItem_Correct - Success");

        }

        static async void testUpgradeGatheringLevel()
        {
            Player p = new Player(new List<ItemTask> {

                new ItemTask { itemName = "Test", itemAmount = 0, resourceGatheringLevel = 1, },

            }, "MyName");
            bool returned = await p.UpgradeGatheringLevel("Test", 2);

            if (returned && p.getItem("Test").resourceGatheringLevel == 2)
                Console.WriteLine("testUpgradeGatheringLevel - Success");
        }

        static async void testNegativeUpgradeGatheringLevel()
        {
            Player p = new Player(new List<ItemTask>
            {
                new ItemTask { itemName = "Test", itemAmount = 0, resourceGatheringLevel = 4},

            }, "MyName");
            bool returned = await p.UpgradeGatheringLevel("Test", 1);

            if (!returned && p.getItem("Test").resourceGatheringLevel == 4)
                Console.WriteLine("testNegativeUpgradeGatheringLevel - Success");

        }

        static async void testSwitchEnabledTask()
        {
            Player p = new Player(new List<ItemTask>
            {
                new ItemTask { itemName = "Test1", enabled = false, itemAmount = 0, resourceGatheringLevel = 4},
                new ItemTask { itemName = "Test2", enabled = true, itemAmount = 0, resourceGatheringLevel = 4},

            }, "MyName");

            bool returned = await p.switchEnabledTask("Test1");

            if (returned && p.getItem("Test1").enabled && !p.getItem("Test2").enabled)
                Console.WriteLine("testSwitchEnabledTask - Success");
        }
        static async void testNegativeSwitchEnabledTask()
        {
            Player p = new Player(new List<ItemTask>
            {

            }, "MyName");

            bool returned = await p.switchEnabledTask("Test");

            if (!returned)
                Console.WriteLine("testNegativeSwitchEnabledTask - Success");
        }


    }
}
