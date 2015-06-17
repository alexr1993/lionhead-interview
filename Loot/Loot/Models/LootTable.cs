using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Loot.Models
{
    public class LootTable
    {
        /* This class supports storage and access of the Loot Table entries for the in-game
         * Loot Chest.
         * 
         * Instances cannot exist without a valid set of entries; items with a drop chance summing
         * to 100, with no negative drop chances, and under {maxItems} entries.
         *
         * Client code may roll on the loot chest by calling getRandomItem(userName), returning an
         * item with the probability provided in the entries dictionary.
         */
        private Dictionary<String, decimal> entries = new Dictionary<String, decimal>();
        // Random items can be returned with O(nItems) time and O(nItems) space
        private SortedDictionary<decimal, String> randomLookup = new SortedDictionary<decimal, String>();

        private decimal certaintyValue = 100; // Probability expressed in %
        private Random rnd = new Random();
        private static String logFile = "log.txt";
        private static int maxItems = 30;

        public Dictionary<String, decimal> Entries
        {
            get
            {
                return entries;
            }
            set
            {
                randomLookup = tryCreateRandomLookupTable(value);

                if (randomLookup != null)
                {
                    entries = value;
                }
                else
                {
                    throw new ArgumentException("Entry table is invalid");
                }
            }
        }

        public LootTable(Dictionary<String, decimal> entries)
        {
            Entries = entries;
        }

        public String getRandomItem(String userName)
        {
            if (userName == null)
            {
                return null;
            }

            // Randomly select item based on drop chance
            decimal random = rnd.Next(1, 101); // random is in [1, 100]
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


        /* Validates entries, returns table to lookup the return item if valid
         * 
         * The random lookup table allows us to check which item a random number from 1-100 corresponds to
         */
        private SortedDictionary<decimal, String> tryCreateRandomLookupTable(Dictionary<String, decimal> entries)
        {
            if (entries == null || entries.Count > maxItems)
            {
                return null;
            }
            SortedDictionary<decimal, String> randomLookupTable = new SortedDictionary<decimal, String>();
            // Create a numeric milestone for each item given by cumulative sum
            decimal cumSum = 0;

            foreach (KeyValuePair<String, decimal> pair in entries)
            {
                // Check only positive values are given
                if (pair.Value <= 0)
                {
                    return null;
                }
                cumSum += (decimal)pair.Value;

                randomLookupTable.Add(cumSum, pair.Key);
            }
            // Check probability sums to certainty
            if (cumSum != certaintyValue)
            {
                return null;
            }
            return randomLookupTable;
        }

        private void log(String userName, String item)
        {
            using (StreamWriter w = new System.IO.StreamWriter(logFile, true))
            {
                String log = String.Format(
                    "{0} {1}: User {2} has been dealt item \"{3}\" from a loot table\n",
                    DateTime.Now.ToLongDateString(),
                    DateTime.Now.ToLongTimeString(),
                    userName,
                    item);
                w.Write(log);
                Trace.Write(log);

            }
        }
    }
}