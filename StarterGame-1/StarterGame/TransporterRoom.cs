using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class TransporterRoom : Room
    {
        private List<Room> _rooms;

        public TransporterRoom(string tag, List<Room> rooms) : base(tag)
        {
            _rooms = rooms ?? new List<Room>();
        }

        public override string Description()
        {
            string baseDescription = base.Description();
            return $"{baseDescription}\nStepping into this room may take you to another place...";
        }

        public Room GetRandomRoom()
        {
            if (_rooms.Count == 0)
            {
                throw new InvalidOperationException("No rooms available to transport to.");
            }

            Random random = new Random();
            int index = random.Next(_rooms.Count);
            return _rooms[index];
        }
    }
}