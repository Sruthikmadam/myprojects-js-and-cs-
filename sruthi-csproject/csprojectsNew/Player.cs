
using System;
using System.Collections.Generic;

namespace csprojectsNew
{
    class Player
    {
        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; }

        public Player()
        {
            Inventory = new List<Item>();
        }

        public bool HasItem(string itemName)
        {
            return Inventory.Exists(item => item.Name == itemName);
        }

        public void DisplayInventory()
        {
            if (Inventory.Count > 0)
            {
                Console.WriteLine("You are carrying:");
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty.");
            }
        }
    }
}
