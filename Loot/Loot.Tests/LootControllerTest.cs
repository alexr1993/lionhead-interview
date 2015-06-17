using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loot.Controllers;
using Loot.Models;

namespace Loot.Tests
{
    [TestClass]
    public class LootControllerTest
    {
        [TestMethod]
        public void invalidControllerGetLoot()
        {
            LootController lootController = new LootController();
            Assert.AreEqual(lootController.getLoot("VendorOfDoom"), "");
        }

        [TestMethod]
        public void validControllerGetLoot()
        {
            LootController lootController = new LootController();
            String[] items = new String[] { "Sword", "Shield", "Axe", "Bow" };
            lootController.loadTestTable(items);

            // We should receive one of the set items
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(Array.IndexOf(items, lootController.getLoot("VendorOfDoom")) != -1);
            }
        }
    }
}
