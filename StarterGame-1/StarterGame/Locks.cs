using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class RegularLock : IItem, ILockable, IKeyed
    {
        private string _name;
        private float _weight;
        private static int lockNumber = 0;
        private Boolean _lock;
        public Boolean IsLocked { get { return _lock;} }
        public Boolean IsUnlocked { get { return !_lock;} }
        private IItem _originalKey;
        private IItem _insertedKey;
        private IItem _decorator;
        public string Name { get { return _name; } private set { _name = value;} }
        public float Weight { get { return _weight + (_decorator == null?0:_decorator.Weight); } private set { _weight = value;} }
        public virtual string Description { get { return Name + ", " + Weight + ", sell value = " + SellValue + ", buy value = " + BuyValue; }}


        public Boolean Lock()
        {
            Boolean result = false;
            if(MayLock)
            {
                if(IsUnlocked)
                {
                    _lock = true;
                    result = true;
                }
            }
            return result;
        }
        public Boolean Unlock()
        {
            Boolean result = false;
            if(MayUnlock)
            {
                if(IsLocked)
                {
                    _lock = false;
                    result = true;
                }
            }
            return result;
        }
        public Boolean MayUnlock{ get {return Validate; } }// this return whats in the Validate method below in line 61
        public Boolean MayLock{ get{ return Validate; } }
        public Boolean MayOpen { get { return IsUnlocked; } }
        public Boolean MayClose { get { return  true; } }

        public RegularLock()
        {
            Name = "lock-rl-" + lockNumber;
            Weight = 0.3f;
            _lock = false;
            _originalKey = new Item("Key-rl-" + lockNumber++, 0.1f);//after create a lock, it will be incremented by 1 each time a new lock is create
            _insertedKey = _originalKey;
        }
        public IItem Insert(IItem key)
        {
            IItem oldKey = _insertedKey; //this allows us to save the key if a key is already in there.
            _insertedKey = key;
            return oldKey;
        }
        public IItem Remove()
        {
            return Insert(null); // this allows us to remove the key, by simply passing null to insert, so when we call insert with an empty parameter, it will be the same as just remove.
            // object oldKey = _key;
            // _key = null;
            // return oldKey;
        }
        public Boolean Validate 
        {
            get
            {
                //return _originalKey.Equals(_insertedKey);
                return _originalKey == _insertedKey;

            }
        }

        public void AddDecorator(IItem item)
        {

        }

        public float SellValue { get { return 0;}}
        public float BuyValue { get { return 0;}}

    }
}