using System;
using System.Collections.Generic;
using System.Numerics;

namespace csprojectsNew
{
    class Game
    {
        private Player player;
        private List<Room> rooms;
        private bool isRunning;

        public Game()
        {
            player = new Player();
            rooms = new List<Room>();
            isRunning = true;

            InitializeRooms();
        }

        private void InitializeRooms()
        {
            Room room1 = new Room("Room 1", "A small room with good couch.");
            Room room2 = new Room("Room 2", "A dimly lit room with an exit to the south and a door to the east.");
            Room room3 = new Room("Room 3", "A bright room with a key on the table.");
            Room room4 = new Room("Room 4", "A large room with a locked chest.");

            room1.SetExit("north", room2);
            room2.SetExit("south", room1);
            room2.SetExit("east", room3);
            room3.SetExit("west", room2);
            room3.SetExit("south", room4, true); // Door locked

            room3.AddItem(new Item("key", "A small brass key."));
            room4.AddItem(new Item("chest", "A locked chest."));

            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);
            rooms.Add(room4);

            player.CurrentRoom = room1;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Text Adventure Game!");

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine(player.CurrentRoom.Description);
                Console.WriteLine("What do you want to do?");
                string input = Console.ReadLine();
                ProcessInput(input);
            }
        }

        private void ProcessInput(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0].ToLower();

            switch (command)
            {
                case "move":
                    if (parts.Length > 1)
                        Move(parts[1].ToLower());
                    else
                        Console.WriteLine("Move where?");
                    break;

                case "look":
                    Console.WriteLine(player.CurrentRoom.Description);
                    player.CurrentRoom.DisplayItems();
                    break;

                case "take":
                    if (parts.Length > 1)
                        Take(parts[1].ToLower());
                    else
                        Console.WriteLine("Take what?");
                    break;

                case "use":
                    if (parts.Length > 1)
                        Use(parts[1].ToLower());
                    else
                        Console.WriteLine("Use what?");
                    break;

                case "inventory":
                    player.DisplayInventory();

                    break;

                case "quit":
                    isRunning = false;
                    Console.WriteLine("Thanks for playing!");
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private void Move(string direction)
        {
            if (player.CurrentRoom.HasExit(direction, out Room nextRoom, out bool isLocked))
            {
                if (isLocked)
                {
                    Console.WriteLine("The door is locked.");
                }
                else
                {
                    player.CurrentRoom = nextRoom;
                    Console.WriteLine($"You moved {direction}.");
                }
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }

        private void Take(string itemName)
        {
            Item item = player.CurrentRoom.GetItem(itemName);
            if (item != null)
            {
                player.Inventory.Add(item);
                player.CurrentRoom.RemoveItem(item);
                Console.WriteLine($"You took the {itemName}.");
            }
            else
            {
                Console.WriteLine("There is no such item here.");
            }
        }

        private void Use(string item)
        {
            if (player.HasItem(item))
            {
                switch (item)
                {
                    case "key":
                        if (player.CurrentRoom.Name == "Room 3" && player.CurrentRoom.HasExit("south", out Room room4, out bool isLocked1) && isLocked1)
                        {
                            player.CurrentRoom.UnlockExit("south");
                            Console.WriteLine("You used the key to unlock the door to the south.");
                            player.CurrentRoom = room4;
                            Console.WriteLine(player.CurrentRoom.Description);

                        }
                       else if (player.CurrentRoom.Name =="Room 4" )
                        {
                            Console.WriteLine("You used the key to unlock the chest. Inside, you find a treasure. You win!");
                            isRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("You can't use the key here.......");
                        }
                        break;

                     case "chest":
                        Console.WriteLine("You can't use that item here.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("You don't have that item.");
            }
        }
    }
}
