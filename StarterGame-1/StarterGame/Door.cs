using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StarterGame
{
    public class Door : ICloseable 
    {
        private Room _roomA;
        private Room _roomB;
        private Boolean _isClosed;
        private ILockable _lock;
        public ILockable ALock
        {
            get { return _lock;}
            set { _lock = value;}
        }



        public Door(Room roomA, Room roomB)
        {
            this._roomA = roomA;
            this._roomB = roomB;
            _isClosed = false;
            _lock = null;
        }

        public Room RoomOnTheOtherSide (Room ofThisRoom) 
        {
            if (ofThisRoom.Equals(this._roomA))
            {
                return _roomB;
            }
            else
            {
                return _roomA;
            }
        }

        public Boolean IsOpen{ get { return !_isClosed; } }
        public Boolean IsClosed{ get { return _isClosed; }}
        public Boolean Open()//this is what allows us to open a closed door, or leave it open if it already is
        {   
            Boolean result = false;
            if(MayOpen)
            {
                if(IsClosed)
                {
                    _isClosed = false;
                    result = true;
                }
                
            }
            return result;
        }
        public Boolean Close()
        {
            Boolean result = false;
            if (MayClose)
            {
                if (IsOpen)
                {
                    _isClosed = true;
                    result = true;
                }
            
            }
            return result;
        }

        public Boolean MayOpen //this allows us to open the lock
        { get
            {return _lock ==null?true : _lock.MayOpen; } 
        }
        public Boolean MayClose //this allows us to close the lock
        { get
            {return _lock ==null?true : _lock.MayClose; } 
        }


        public static Door CreateDoor(Room firstRoom, string firstDirection, Room secondRoom, string secondDirection)//helper method that creates the door and connects the rooms.
        {
            Door door = new Door(firstRoom, secondRoom);
            // firstRoom.SetExit(firstDirection, door);
            // secondRoom.SetExit(secondDirection, door);
            secondRoom.SetExit(firstDirection, door);
            firstRoom.SetExit(secondDirection, door);
            return door;
        }
    }

}