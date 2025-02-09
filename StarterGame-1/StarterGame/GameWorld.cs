using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    /*
     * Fall 2024
     */
    public class GameWorld
    {
            private static GameWorld _instance;
            public static GameWorld Instance 
            {
                get 
                {
                    if (_instance == null) 
                    {
                        _instance = new GameWorld();
                    }
                    
                    {
                        return _instance;
                    }
                }
            }


        private Dictionary<string, NPC> _npcLocations; // Declare the NPC tracking dictionary


        
        private Room CreateStartingRoom()
        {
            Room startingRoom = new Room("The Starting Room");
            // You can add any other setup for this room here.
            return startingRoom;
        }

        private GameWorld()
        {
            _events = new Dictionary<ITrigger, IWorldEvent>(); // Ensure _events is initialized
            _entrance = CreateStartingRoom(); // Create the starting room
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
            _npcLocations = new Dictionary<string, NPC>(); // Initialize the dictionary

            CreateWorld();

            // _events = new Dictionary<ITrigger, IWorldEvent>(); //commented out today
            // _entrance = CreateStartingRoom(); // Set _entrance to the starting room.
            // NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
        }



            private Room _trigger;
            private Room _entrance;
            public Room Entrance { get { return _entrance;}}
            private bool _playing;
            private Parser _parser;
            private Player _player;

            // World Event Setup

            private Dictionary <ITrigger, IWorldEvent> _events;



            public void PlayerDidEnterRoom(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player != null)
                {
                    // Checks if the player entered a TransporterRoom
                    if (player.CurrentRoom is TransporterRoom transporterRoom)
                    {
                        Room randomRoom = transporterRoom.GetRandomRoom();
                        player.CurrentRoom = randomRoom;
                        player.InfoMessage($"You have been transported to: {randomRoom.Tag}");
                        Notification newNotification = new Notification("PlayerDidEnterRoom", player);
                        NotificationCenter.Instance.PostNotification(newNotification);
                        return;
                    }

                    // World Event Checking and handling
                    if (_events.TryGetValue(player.CurrentRoom, out IWorldEvent we))
                    {
                        we.Execute();
                        player.WarningMessage("You changed the world");
                    }
                }
            }


            // public void PlayerDidEnterRoom(Notification notification)
            // {
            //     Player player = (Player) notification.Object;
            //     if (player != null)
            //         {
            //             //World Event Checking and handling
            //             IWorldEvent we = null;
            //             _events.TryGetValue(player.CurrentRoom, out we);
            //             if(we != null)
            //             {
            //                 we.Execute();
            //                 player.WarningMessage("You changed the world");
            //             }
            //         }
            // }
        

        public void CreateWorld()
        {
            Room outside = new Room("outside the main entrance of the university");
            Room scctparking = new Room("in the parking lot at SCCT");
            Room boulevard = new Room("on the boulevard");
            Room universityParking = new Room("in the parking lot at University Hall");
            Room parkingDeck = new Room("in the parking deck");
            Room scct = new Room("in the SCCT building");
            Room theGreen = new Room("in the green in front of Schuster Center");
            Room universityHall = new Room("in University Hall");
            Room schuster = new Room("in the Schuster Center");

            Room cafeteria = new Room("in the cafeteria, where you can find food");

            // Disconnected rooms
            Room davisdon = new Room("the davison");
            Room woodhall = new Room(" in Woodhall");
            Room greekCenter = new Room(" at the Greek Center");
            Room clockTower = new Room("at the clock tower");


            // linked these two room via the door. the same door will be reference by the parallel dictionaries.
            Door door = Door.CreateDoor(boulevard, "west", outside, "east");

            door = Door.CreateDoor(scctparking, "south", boulevard, "north");

            door = Door.CreateDoor(theGreen, "west", boulevard, "east");

            door = Door.CreateDoor(universityParking, "north", boulevard, "south");

            door = Door.CreateDoor(scct, "west", scctparking, "east");

            door = Door.CreateDoor(schuster, "north", scct, "south");

            door = Door.CreateDoor(universityHall, "north", schuster, "south");

            door = Door.CreateDoor(theGreen, "east", schuster, "west");

            door = Door.CreateDoor(universityParking, "east", universityHall, "west");

            door = Door.CreateDoor(parkingDeck, "north", universityParking, "south"); //in this section, the north exit will closed and will need to be unlocked using the Key-rl-0 key.

            door = Door.CreateDoor(parkingDeck, "south", cafeteria, "north");



            RegularLock rl = new RegularLock();
            door.ALock = rl;
            rl.Lock();//locks the lock
            schuster.Drop(rl.Remove());
            //rl.Insert(new Item("Dummy", 1));
            door.Close();
            


            IItem book = new Item("The Book of OOP", 5, 15, 20); // Name, Weight, Sell Value, Buy Value of the book
            universityHall.Drop(book);


            // The created NPC Joe
            NPC joe = new NPC("Joe");
            parkingDeck.AddNPC(joe); // Added Joe as an NPC to the room

            _npcLocations["Joe"] = joe; // Track Joe's location



            //Connect extra rooms
            door = Door.CreateDoor(clockTower, "west", davisdon, "east");// do this for all the other rooms that lead from and to each other
            door = Door.CreateDoor(greekCenter, "north", clockTower, "south");
            door = Door.CreateDoor(woodhall, "south", clockTower, "north");

            //special World event initialization
            RoomTriggerWorldEvent we = new RoomTriggerWorldEvent(scct, schuster, davisdon, "west", "east");
            _events [we.Trigger] = we;


            // Setup Special rooms like trap room
            Room.TrapRoom tr = new Room.TrapRoom();
            universityHall.RoomDelegate = tr;
            tr = new Room.TrapRoom ("magic");
            clockTower.RoomDelegate = tr;
            //EchoRoom er = new EchoRoom();     //this is in case we want to create an echo room, it is not a trap room, it's where the 'word' will be repeated many times, just like in an echo room.
            //parkingDeck.RoomDelegate = er;    //the echo room is also is a delegate. just like trap room. both delegates are receiving the notifications that player is doing something
            


            List<Room> allRooms = new List<Room>
            {
                outside, scctparking, boulevard, universityParking,
                parkingDeck, scct, theGreen, universityHall,
                schuster, davisdon, woodhall, greekCenter, clockTower
            };

            // Create Transporter Room
            Room transporterRoom = new TransporterRoom("a mystical transporter room", allRooms);
            //door = Door.CreateDoor(theGreen, "north", transporterRoom, "south");
            Door doorToTransporter = Door.CreateDoor(theGreen, "north", transporterRoom, "south");


            // Make the transporter room connected to other rooms
            allRooms.Add(transporterRoom);


            //Setup Item
            IItem sword = new Item ("sword", 3.5f);//the 3.5f is the weight value/
            IItem decoration = new Item("gold", 1.3f, 4.5f, 5.5f);//the 1.3f is the weight value, and 4.5f and 5.5f are the sell and buy values
            sword.AddDecorator(decoration);
            scct.Drop(sword);


            IItem sandwich = new Item("sandwich", 1.0f, 2.0f, 3.0f);
            IItem coffee = new Item("coffee", 0.5f, 1.5f, 2.0f);
            cafeteria.Drop(sandwich);
            cafeteria.Drop(coffee);


            //item container

            //uncomment this later: Nov 29
            // ItemContainer chest = new ItemContainer("chest");
            // Item spoon = new Item("spoon");
            // Item fork = new Item("fork");
            // chest.Insert(spoon);
            // chest.Insert(fork);

            // clockTower.Drop(chest);


             _entrance = outside;

        }
        
    }
}
