using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Loot.Models
{
    public class LootTable
    {
        public Dictionary<String, decimal> entries
        {
            get
            {
                return entries;
            }
            set
            {
                if (areValidEntries(value))
                {
                    entries = value;
                    randomLookup = createRandomLookupTable(entries);
                }
                else
                {
                    throw new ArgumentException(
                        "Entry table is invalid, ensure drop chances sum to 100");
                }
            }
        }
        private Dictionary<decimal, String> randomLookup = new Dictionary<decimal, String>();
        private decimal certaintyValue = 100; // Probability expressed in %
        private Random rnd = new Random();

        public LootTable(Dictionary<String, decimal> entries)
        {
            this.entries = entries;
        }

        public String getRandomItem(String userName)
        {
            if (userName == null || !areValidEntries(entries))
            {
                return null;
            }

            // Randomly select item based on drop chance
            decimal random = rnd.Next(1, 100);
            String item = "";

            // Find the item whose range we have fallen in
            foreach (decimal cumSum in randomLookup.Keys)
            {
                if (random <= cumSum)
                {
                    item = randomLookup[cumSum];
                    break;
                }
            }
            log(userName, item);
            return item;
        }

        private bool areValidEntries(Dictionary<String, decimal> entries)
        {
            if (entries == null)
            {
                return false;
            }
            return entries.Values.Cast<decimal>().Sum() == certaintyValue;
        }

        /* Creates lookup from dice roll output to loot item */
        private Dictionary<decimal, String> createRandomLookupTable(Dictionary<String, decimal> entries)
        {
            Dictionary<decimal, String> randomLookupTable = new Dictionary<decimal, String>();
            decimal cumSum = 0;
            foreach (KeyValuePair<String, decimal> pair in entries)
            {
                cumSum += (decimal)pair.Value;
                randomLookupTable.Add(cumSum, pair.Key);
            }
            return randomLookupTable;
        }

        private void log(String userName, String item)
        {

        }
    }
}