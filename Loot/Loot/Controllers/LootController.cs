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
        private LootTable lootTable { get; set; }

        public string getLoot(String player)
        {
            if (lootTable != null)
            {
                return lootTable.getRandomItem(player);
            }
            else
            {
                return "";
            }
        }
    }
}
