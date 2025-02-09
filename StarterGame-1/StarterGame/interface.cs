using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public interface IWorldEvent 
    {
        public Room Trigger {get;}
        public void Execute();
    }
        public interface IRoomDelegate
        {
            public Room ContainingRoom {get; set;}

            public Door RoomWillGetAnExit(string exitName, Door door);
            public string RoomWillGetExits(string exitNames);
        }

        public enum ROOM_COMPONENT {BATTLE, TRADE, CRAFT }
        public interface IRoomComponent 
        {
            public ROOM_COMPONENT ComponentType { get; }
        }


        public interface IBattleComponent : IRoomComponent
        {
            public float Attack(Player player, string enemyName);//returns the health of the enemy
            public float Defend(Player player, string enemyName);
            public string ListEnemies();
        }
        public interface ICloseable 
        {
            public Boolean IsOpen{ get; }
            public Boolean IsClosed{ get; }
            public Boolean Open();
            public Boolean Close();
            public Boolean MayOpen { get; }
            public Boolean MayClose { get; }
            
        }
        public interface ILockable
        {
            public Boolean IsLocked { get; }
            public Boolean IsUnlocked { get; }
            public Boolean Lock();
            public Boolean Unlock();
            public Boolean MayUnlock{ get; }
            public Boolean MayLock{ get; }
            public Boolean MayOpen { get; }
            public Boolean MayClose { get; }
        }

        public interface IKeyed
        {
            public IItem Insert(IItem key);
            public IItem Remove();
            public Boolean MayUnlock{ get; }
            public Boolean MayLock{ get; }
            public Boolean Validate { get; }
        }

        public interface IItem
        {
            public string Name { get; }
            public float Weight { get; }
            public float SellValue { get; }
            public float BuyValue { get; }
            public string Description { get; }

            public void AddDecorator(IItem decorator);
        }
        public interface IItemContainer : IItem 
        {
            public void Insert(IItem item);
            public IItem Remove(string itemName);
        }
    }
