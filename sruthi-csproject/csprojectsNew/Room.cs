using System;
using System.Collections.Generic;

namespace csprojectsNew
{
    class Room
    {
        public string Name { get; }
        public string Description { get; }
        private Dictionary<string, (Room, bool)> exits;
        private List<Item> items;

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            exits = new Dictionary<string, (Room, bool)>();
            items = new List<Item>();
        }

        public void SetExit(string direction, Room room, bool isLocked = false)
        {
            exits[direction] = (room, isLocked);
        }

        public bool HasExit(string direction, out Room room, out bool isLocked)
        {
            if (exits.TryGetValue(direction, out (Room, bool) exitInfo))
            {
                room = exitInfo.Item1;
                isLocked = exitInfo.Item2;
                return true;
            }
            room = null;
            isLocked = false;
            return false;
        }

        public void UnlockExit(string direction)
        {
            if (exits.ContainsKey(direction))
            {
                exits[direction] = (exits[direction].Item1, false);
            }
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public Item GetItem(string itemName)
        {
            return items.Find(item => item.Name == itemName);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public bool HasItem(string itemName)
        {
            return items.Exists(item => item.Name == itemName);
        }

        public void DisplayItems()
        {
            if (items.Count > 0)
            {
                Console.WriteLine("You see:");
                foreach (var item in items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
        }
    }
}
