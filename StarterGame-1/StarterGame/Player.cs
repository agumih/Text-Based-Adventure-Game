using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        private IItemContainer _inventory; // Inventory container
        private float _maxCarryWeight = 10; // the default maximum carry weight
        private Stack<Room> _roomHistory; // Back command implementation in a stack to store visited rooms


        public Player(Room room)
        {
            _currentRoom = room;
            _inventory = new Room.ItemContainer("inventory"); // Initialize inventory
            _roomHistory = new Stack<Room>(); // Initialize the room history stack

        }

        public bool CanCarry(float weight)//checks if the player can carry the items, it checks against player's current weight and the max weight
        {
            return ((Room.ItemContainer)_inventory).CurrentWeight + weight <= _maxCarryWeight;
        }


        public void AddItem(IItem item)//if an items will not exceed the weight limit, then the player can add it to the inventory.
        {
            if (CanCarry(item.Weight))
            {
                _inventory.Insert(item);
                InfoMessage($"You added {item.Name} to your inventory.");
            }
            else
            {
                throw new InvalidOperationException($"You cannot carry {item.Name}. It is too heavy.");
            }
        }



        public void WaltTo(string direction) // Move to another room
        {
            Door door = this.CurrentRoom.GetExit(direction);

            if (door != null)
            {
                if (door.IsOpen)
                {
                    _roomHistory.Push(_currentRoom); // Back command implementation: this stores current room in history

                    Room nextRoom = door.RoomOnTheOtherSide(CurrentRoom);
                    CurrentRoom = nextRoom;
                    Notification notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    NormalMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    WarningMessage("The door is closed.");
                }
            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
        }
        public void Back() // the back command implementation
        {
            if (_roomHistory.Count > 0)
            {
                Room previousRoom = _roomHistory.Pop();
                _currentRoom = previousRoom;
                InfoMessage($"You moved back to: {_currentRoom.Description()}");
                Notification notification = new Notification("PlayerDidEnterRoom", this);
                NotificationCenter.Instance.PostNotification(notification);
            }
            else
            {
                WarningMessage("There is no room to go back to.");
            }
        }

        public void Open(string exitName) // Open a door
        {
            Door door = CurrentRoom.GetExit(exitName);
            if (door != null)
            {
                Boolean state = door.IsClosed;
                if (door.Open())
                {
                    InfoMessage("The door is now open.");
                }
                else
                {
                    if (state)
                    {
                        WarningMessage("The door did not open.");
                    }
                    else
                    {
                        InfoMessage("The door was already open.");
                    }
                }
            }
            else
            {
                ErrorMessage("\nThere is no door on " + exitName);
            }
        }

        public void Unlock(string exitName) // Unlock a door
        {
            Door door = CurrentRoom.GetExit(exitName);
            if (door != null)
            {
                Boolean state = door.ALock.IsLocked;
                if (door.ALock.Unlock())
                {
                    InfoMessage("The door is now unlocked.");
                }
                else
                {
                    if (state)
                    {
                        WarningMessage("The door did not unlock.");
                    }
                    else
                    {
                        InfoMessage("The door was already unlocked.");
                    }
                }
            }
            else
            {
                ErrorMessage("\nThere is no door on " + exitName);
            }
        }

        public void Inspect(string itemName) // Inspect an item in the room
        {
            IItem item = CurrentRoom.Remove(itemName); // Temporarily remove the item
            if (item != null)
            {
                InfoMessage($"{itemName}: {item.Description}");
                CurrentRoom.Drop(item); // Place it back
            }
            else
            {
                WarningMessage($"There is no item named {itemName} here.");
            }
        }


        public void Pickup(string itemName)
        {
            IItem item = CurrentRoom.Remove(itemName); // Attempt to pick up the item from the room
            if (item != null)
            {
                if (CanCarry(item.Weight)) // Checks if the player can carry the item's weight
                {
                    _inventory.Insert(item); // Add the item to the inventory
                    InfoMessage($"You picked up {itemName}.");
                }
                else
                {
                    WarningMessage($"You cannot carry {itemName}. It is too heavy.");
                    CurrentRoom.Drop(item); // Return the item to the room if it's too heavy
                }
            }
            else
            {
                WarningMessage($"There is no item named {itemName} here.");
            }
        }


        
        public IItem RemoveItem(string itemName) // Method to remove an item from the player's inventory
        {
            return _inventory.Remove(itemName);
        }

        public void Drop(string itemName) // Drops an item into the current room
        {
            IItem item = _inventory.Remove(itemName);//checks if the item is in the inventory
            if (item != null)
            {
                CurrentRoom.Drop(item);
                InfoMessage($"You dropped {itemName}.");
            }
            else
            {
                WarningMessage($"You don't have an item named {itemName}.");
            }
        }


        public void Insert(string exitName) // Insert a universal key into a locked door
        {
            Door door = CurrentRoom.GetExit(exitName);
            if (door != null)
            {
                IKeyed keyedLock = door.ALock as IKeyed; // Check if the door has a keyed lock
                if (keyedLock != null)
                {
                    // Look for Key-rl-0 in the inventory
                    IItem key = _inventory.Remove("Key-rl-0");
                    if (key != null)
                    {
                        keyedLock.Insert(key); // Insert the universal key
                        InfoMessage($"You inserted {key.Name} into the lock on {exitName}.");
                    }
                    else
                    {
                        WarningMessage("You don't have the Key-rl-0 to insert.");
                    }
                }
                else
                {
                    WarningMessage($"The door on {exitName} doesn't have a keyed lock.");
                }
            }
            else
            {
                WarningMessage($"There is no door on {exitName}.");
            }
        }


        public void DisplayWeight()//displays the current weight the player is carrying
        {
            InfoMessage($"You are carrying {((Room.ItemContainer)_inventory).CurrentWeight} weight units.");
        }


        // public void Insert(string exitName) // Insert an item into a locked door
        // {
        //     Door door = CurrentRoom.GetExit(exitName);
        //     if (door != null)
        //     {
        //         IKeyed keyedLock = (IKeyed)door.ALock;
        //         if (keyedLock != null)
        //         {
        //             IItem item = _inventory.Remove("key"); // Assume key is named "key"
        //             if (item != null)
        //             {
        //                 keyedLock.Insert(item);
        //                 InfoMessage($"You inserted {item.Name} into the lock on {exitName}.");
        //             }
        //             else
        //             {
        //                 WarningMessage("You don't have a key to insert.");
        //             }
        //         }
        //         else
        //         {
        //             WarningMessage($"The door on {exitName} doesn't have a lock.");
        //         }
        //     }
        //     else
        //     {
        //         WarningMessage($"There is no door on {exitName}.");
        //     }
        // }

        // public void DisplayInventory() // Display inventory contents
        // {
        //     InfoMessage(_inventory.Description);
        // }




        public void DisplayInventory()//displays the items in the player's inventory
        {
            if (_inventory is Room.ItemContainer inventory)
            {
                if (inventory.CurrentWeight == 0) // Check if the inventory is empty
                {
                    InfoMessage("Your inventory is empty.");
                }
                else
                {
                    InfoMessage($"Your inventory contains:\n{inventory.Description}");
                }
            }
            
        }

        public void Say(string word) // Say a word (trap room interaction)
        {
            NormalMessage(word);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerDidSayWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }

        public void ExitBattle()
        {
            Notification notification = new Notification("PlayerDidExitBattle", this);
            NotificationCenter.Instance.PostNotification(notification);
            InfoMessage("Exiting Battle");
        }

        public void ExitTrade()
        {
            Notification notification = new Notification("PlayerDidExitTrade", this);
            NotificationCenter.Instance.PostNotification(notification);
            InfoMessage("Exiting Trade");
        }

        // Message handling
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }
}