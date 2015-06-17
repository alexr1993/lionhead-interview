using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
                randomLookup = tryCreateRandomLookupTable(entries);

                if (randomLookup != null)
                {
                    entries = value;
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
        private static String logFile = "log.txt";

        public LootTable(Dictionary<String, decimal> entries)
        {
            this.entries = entries;
        }

        public String getRandomItem(String userName)
        {
            if (userName == null)
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


        /* Validates entries, returns table to lookup the return item if valid */
        private Dictionary<decimal, String> tryCreateRandomLookupTable(Dictionary<String, decimal> entries)
        {
            if (entries == null)
            {
                return null;
            }
            Dictionary<decimal, String> randomLookupTable = new Dictionary<decimal, String>();
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
            if (entries.Values.Cast<decimal>().Sum() != certaintyValue)
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
                    DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString(),
                    userName,
                    item);
                w.Write(log);
                Console.Write(log);

            }
        }
    }
}