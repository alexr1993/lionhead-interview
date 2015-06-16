using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loot.Models;

namespace Loot.Tests
{
    [TestClass]
    public class LootTableTests
    {
        /* Try to create empty table */
        [TestMethod]
        public void emptyTable()
        {

        }

        /* Create one element loot table */
        [TestMethod]
        public void oneElementLootTable()
        {

        }

        [TestMethod]
        public void createSmallTable()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 50);
            entries.Add("Shield", 50);
            LootTable table = new LootTable(entries);
        }

        [TestMethod]
        public void createTableWithNegativeValues()
        {
            // adds to 100 but with negative values
        }

        [TestMethod]
        public void createTableWithLargeValues()
        {
            // Adds up to more than 100
        }

        [TestMethod]
        public void updateTableValid()
        {

        }

        [TestMethod]
        public void updateTableInvalid()
        {

        }

        [TestMethod]
        public void setEntriesToDecimalChances()
        {

        }
    }
}
