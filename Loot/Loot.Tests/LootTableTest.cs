using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loot.Models;

namespace Loot.Tests
{
    [TestClass]
    public class LootTableTest
    {
        /* Try to create empty table */
        [TestMethod]
        public void emptyTable()
        {
            try
            {
                LootTable table = new LootTable(new Dictionary<String, decimal>());
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                pass();
            }
        }

        /* Create one element loot table */
        [TestMethod]
        public void oneElementLootTable()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 100);
            LootTable table = new LootTable(entries);

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
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", -20);
            entries.Add("Shield", 120);
            LootTable table = new LootTable(entries);

        }

        [TestMethod]
        public void createTableWithLargeValues()
        {
            // Adds up to more than 100
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 100);
            entries.Add("Shield", 120);
            LootTable table = new LootTable(entries);
        }

        [TestMethod]
        public void nonIntegerChances()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 33.3m);
            entries.Add("Shield", 33.3m);
            entries.Add("Cape", 33.4m);
            LootTable table = new LootTable(entries);
        }

        [TestMethod]
        public void updateTableValid()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 100);
            LootTable table = new LootTable(entries);
            table.entries = entries;
        }

        [TestMethod]
        public void updateTableInvalid()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 100);
            LootTable table = new LootTable(entries);
            entries.Add("Sword", 100);
            table.entries = entries;
        }

        [TestMethod]
        public void duplicateItems()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 80);
            entries.Add("Sword", 20);
            try
            {
                LootTable table = new LootTable(entries);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void zeroChance()
        {
            Dictionary<String, decimal> entries = new Dictionary<string, decimal>();
            entries.Add("Sword", 80);
            entries.Add("Shield", 20);
            entries.Add("Knife", 0);

            try
            {
                LootTable table = new LootTable(entries);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                pass();
            }
        }

        private void pass()
        {
            Assert.IsTrue(true);
        }
    }
}
