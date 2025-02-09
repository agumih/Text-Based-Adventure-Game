using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class Item : IItem
    {
        private string _name;
        private float _weight;
        private float _sellValue;
        private float _buyValue;
        
        public string Name { get { return _name; } private set { _name = value;} }
        public float Weight { get { return _weight + (_decorator == null?0:_decorator.Weight); } private set { _weight = value;} }
        public virtual string Description { get { return Name + ", " + Weight + ", sell value = " + SellValue + ", buy value = " + BuyValue; }}
        public float SellValue { get { return _sellValue + (_decorator == null ? 0 : _decorator.SellValue); } private set {_sellValue = value;}}
        public float BuyValue { get { return _buyValue + (_decorator == null ? 0 : _decorator.BuyValue); } private set {_buyValue = value;}}


        private IItem _decorator;
        

        public Item() : this("nameless") {}
        public Item (string name) : this (name, 1) {}
        public Item(string name, float weight) : this(name, weight, 1) {}
        public Item(string name, float weight, float sellValue) : this(name, weight, sellValue, 1) {}
    
        //Designated Constructor
        public Item (string name, float weight, float sellValue, float buyValue)
        {
            Name = name;
            Weight = weight;
            SellValue = sellValue;
            BuyValue = buyValue; 
            _decorator = null;
        }
        public void AddDecorator(IItem item) //the code below operates like a linked list ??
        {
            if(_decorator == null)
            {
                _decorator = item;
                //_decorator = Item;
            }
            else 
            {
                _decorator.AddDecorator(item);
            }
            
        }
        public class ItemContainer : Item, IItemContainer
        {
            private Dictionary<string, IItem> _container;

            public ItemContainer(string name) : base(name)
            //public ItemContainer(string name) : base(name, 0, 0, 0)// added today
            {
                _container = new Dictionary<string, IItem>();
            }


            public float CurrentWeight
            {
                get
                {
                    return _container.Values.Sum(item => item.Weight);
                }
            }

            public void Insert(IItem item)
            {
                _container[item.Name] = item;
            }
            public IItem Remove(string itemName)
            {
                IItem itemToRemove = null;
                _container.TryGetValue(itemName, out itemToRemove);
                _container.Remove(itemName);//added today
                return itemToRemove;
            }

            

            override
            public string Description
            {
                get 
                {
                    string description = base.Description;
                    description += "\nContents\n";
                    foreach(IItem item in _container.Values)
                    {
                        description += "\t" + item.Description + "\n";
                    }
                    return description;
                }
            }
        }
    }
}