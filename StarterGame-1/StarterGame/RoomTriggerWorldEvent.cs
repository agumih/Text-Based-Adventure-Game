using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace StarterGame
{
    public class RoomTriggerWorldEvent : IWorldEvent
    {
        private Room _trigger;
        private Room _exit;
        private Room _newRoom;
        private string _ToRoom;
        private string _FromRoom;

        public ITrigger Trigger {get {return _trigger;}}

        Room IWorldEvent.Trigger => throw new NotImplementedException();

        public RoomTriggerWorldEvent(Room trigger, Room exit, Room newRoom, string toRoom, string fromRoom)
            {
                _trigger = trigger;
                _exit = exit;
                _newRoom = newRoom;
                _ToRoom = toRoom;
                _FromRoom = fromRoom;
            }

        // private void Execute()
        // {
        //     //  _exit.SetExit(_ToRoom, _newRoom);
        //     //     _newRoom.SetExit(_FromRoom, _exit);
        //     Door door = Door.CreateDoor(_newRoom, _ToRoom, _exit, _FromRoom);
        // }

        public void Execute()
        {
            // Create a door to connect the new room and the exit
            Door door = Door.CreateDoor(_newRoom, _ToRoom, _exit, _FromRoom);

            // You can also add logic to notify the player about the event or provide additional interactions
           Console.WriteLine($"A new path has been revealed! You can now go {_ToRoom} from {_exit.Tag} to {_newRoom.Tag}.");
        }
    }

    public interface ITrigger
    {
        
    }
}