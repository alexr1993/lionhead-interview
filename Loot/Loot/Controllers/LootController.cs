using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Loot.Models;

namespace Loot.Controllers
{
    public class LootController : ApiController
    {
        public LootTable Table { get; set; }

        public string getLoot(String player)
        {
            if (Table != null)
            {
                return Table.getRandomItem(player);
            }
            else
            {
                return "";
            }
        }

        public void loadTestTable(String[] items)
        {
            int certainty = 100;
            int nItems = 4; // This must be a divisor of 100
            Dictionary<String, decimal> entries = new Dictionary<String, decimal>();
            foreach (String item in items)
            {
                entries.Add(item, certainty / nItems);
            }
            Table = new LootTable(entries);
        }
    }
}
