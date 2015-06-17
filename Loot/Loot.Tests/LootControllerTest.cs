using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Loot.Controllers;

namespace Loot.Tests
{
    [TestClass]
    public class LootControllerTest
    {
        [TestMethod]
        public void invalidUse()
        {
            LootController lootController = new LootController();
            Assert.AreEqual(lootController.getLoot("VendorOfDoom"), "");
        }
    }
}
