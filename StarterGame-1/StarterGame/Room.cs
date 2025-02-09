using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{


    public class Room : ITrigger
    {
        private Dictionary<string, Door> _exits; 
        private string _tag;
        public string Tag { get { return _tag; } set { _tag = value; } }

        private IRoomDelegate _roomDelegate;
        public IRoomDelegate RoomDelegate 
        {
            get 
            {
                return _roomDelegate;
            } 
            set 
            {
                _roomDelegate = value;
                if (_roomDelegate != null)
                {
                    _roomDelegate.ContainingRoom = this;
                }
            }
        }

        private ItemContainer _itemContainer;
        private Dictionary<string, NPC> _npcs; // NPC storage

        public Room(string tag)
        {
            _exits = new Dictionary<string, Door>();
            _npcs = new Dictionary<string, NPC>(); // Initialize NPC dictionary
            this.Tag = tag;
            RoomDelegate = null;
            _itemContainer = new ItemContainer("floor");
        }

        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

        public Door GetExit(string exitName)
        {
            Door door = null;
            _exits.TryGetValue(exitName, out door);
            return RoomDelegate == null ? door : RoomDelegate.RoomWillGetAnExit(exitName, door);
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }
            return RoomDelegate == null ? exitNames : RoomDelegate.RoomWillGetExits(exitNames);
        }


        public virtual string Description()
            { 
                string npcDescriptions = _npcs.Count > 0 ? "\nNPCs here: " + string.Join(", ", _npcs.Values.Select(npc => npc.Name)) : "";
                return "You are " + this.Tag + ".\n *** " + this.GetExits() + "\n >>> " + _itemContainer.Description + npcDescriptions;
            }


        // Add NPC to the room
            public void AddNPC(NPC npc)
            {
                _npcs[npc.Name.ToLower()] = npc;
            }


            // Retrieve NPC by name
            public NPC GetNPC(string name)
            {
                _npcs.TryGetValue(name.ToLower(), out NPC npc);
                return npc;
            }

            // Check if a specific NPC is present
            public bool HasNPC(string name)
            {
                return _npcs.ContainsKey(name.ToLower());
            }

        public void Drop(IItem item)
        {
            _itemContainer.Insert(item);
        }

        public IItem Remove(string itemName)
        {
            return _itemContainer.Remove(itemName);
        }

        public string GetRoomInventory()
        {
            return _itemContainer.Description;
        }



        public class ItemContainer : Item, IItemContainer
        {
        private Dictionary<string, IItem> _container;
        private float _maxWeight;

        public float CurrentWeight => _container.Values.Sum(item => item.Weight);

        public ItemContainer(string name, float maxWeight = 50) : base(name)
        {
            _container = new Dictionary<string, IItem>();
            _maxWeight = maxWeight;
        }

        public void Insert(IItem item)
        {
            if (CurrentWeight + item.Weight <= _maxWeight)
            {
                _container[item.Name] = item;
            }
            else
            {
                throw new InvalidOperationException($"Cannot pick up {item.Name}. It exceeds your weight limit.");
            }
        }

        public IItem Remove(string itemName)
        {
            if (_container.TryGetValue(itemName, out IItem item))
            {
                _container.Remove(itemName);
                return item;
            }
            return null;
        }

        public override string Description
        {
            get
            {
                if (_container.Count == 0)
                {
                    return string.Empty; // Return an empty string when there are no items
                }

                return "This Room contains:\n" + string.Join("\n", _container.Values.Select(item => $"- {item.Description}"));
            }
        }
            }




        // TrapRoom Implementation
        public class TrapRoom : IRoomDelegate 
        { 
            private Boolean _active;
            private string _password;
            private Room _containingRoom;

            public Room ContainingRoom { get { return _containingRoom; } set { _containingRoom = value; } }

            public TrapRoom() : this("please") {}

            public TrapRoom(string password)
            {
                _active = true;
                _password = password;
                NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
                ContainingRoom = null;
            }

            public Door RoomWillGetAnExit(string exitName, Door door)
            { 
                return _active ? null : door;
            }

            public string RoomWillGetExits(string exitNames)
            {
                return _active ? "You are in a trap room! (enter evil laugh)" : exitNames;
            }

            public void PlayerDidSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player != null && player.CurrentRoom.Equals(ContainingRoom))
                {
                    Dictionary<string, object> userInfo = notification.UserInfo;
                    if (userInfo != null)
                    {
                        object word = null;

                        if (userInfo.TryGetValue("word", out word) && word != null)
                        {
                            if (_password.Equals(word.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                player.InfoMessage($"The word '{_password}' unlocks the exits!");
                                _active = false; // Deactivate the trap
                                NotificationCenter.Instance.PostNotification(new Notification("TrapRoomDeactivated", this));
                            }
                            else
                            {
                                player.ErrorMessage($"'{word}' is not the correct word.");
                            }
                        }
                    }
                }
            }
        }
    }
}





