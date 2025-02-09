using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace StarterGame

{
    public class NPC
{
    private string _name;
    private Item.ItemContainer _storage;
    private List<string> _requestedItems; // NPC's list of requested items

    public string Name { get { return _name; } }
    public string Description { get { return $"{_name} is here to safeguard your items."; } }

    public NPC(string name)
    {
        _name = name;
        _storage = new Item.ItemContainer($"{name}'s Storage");
        _requestedItems = new List<string> { "sandwich", "coffee", "The Book of OOP", "sword" }; // Joe's initial requests
    }

    public void TakeItem(IItem item)
    {
        _storage.Insert(item);
    }

    public string StorageContents()
    {
        return _storage.Description;
    }

    public IItem ReturnItem(string itemName)
    {
        if (_storage.Remove(itemName) is IItem item) // Successfully removed from storage
        {
            return item;
        }
        else
        {
            return null; // Item not found
        }
    }

    public string RespondToAsk()
    {
        if (_requestedItems.Count == 0) // All requests fulfilled
        {
            return $"{_name} says: You have won!";
        }
        else
        {
            string requests = string.Join(", ", _requestedItems);
            return $"{_name} says: To win, you have to bring me these items: {requests}.";
        }
    }

    public bool HasPlayerWon()
{
    return _requestedItems.Count == 0; // Player has won if all requested items are fulfilled
}

    public bool ReceiveItem(IItem item)
    {
        // Match the item with the requested list (case-insensitive comparison)
        string requestedItem = _requestedItems.Find(i => i.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        if (requestedItem != null)
        {
            _requestedItems.Remove(requestedItem); // Remove the fulfilled request
            TakeItem(item); // Add the item to NPC's storage

            // Automatically declare victory if all items are received
            if (_requestedItems.Count == 0)
            {
                Console.WriteLine($"{_name} says: You have won!, you can quit the game now.");
            }

            return true;
        }
        return false;
    }
}
}




