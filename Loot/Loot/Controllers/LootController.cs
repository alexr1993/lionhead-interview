using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Loot.Models;
/**
 * Scenario:

A game designer has given you the following user story to implement:

Story
As a player
I want to open a chest and receive an item
So that I can use the item in the game
 
Acceptance criteria
Scenario: Receive item from loot table
Given I have a player
And a configured loot table
When I roll on this loot table
Then I receive a random item from the loot table
And a log is written with the players username and received item

Dev notes

C# Service hosting a REST-ful API endpoint
BDD/TDD development approach
Configurable loot tables at runtime
Idempotent

Example loot table
Item Drop chance % 
Sword 10 
Shield 10 
Health Potion 30 
Resurrection Phial 30 
Scroll of wisdom 20
 */
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
