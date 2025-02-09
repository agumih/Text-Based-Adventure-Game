using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class RetrieveCommand : Command
    {
        public RetrieveCommand()
        {
            this.Name = "retrieve";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                string itemName = this.SecondWord;
                NPC joe = player.CurrentRoom.GetNPC("Joe");

                if (joe != null)
                {
                    IItem item = joe.ReturnItem(itemName);
                    if (item != null)
                    {
                        if (player.CanCarry(item.Weight))
                        {
                            player.AddItem(item);
                            player.InfoMessage($"You retrieved {item.Name} from {joe.Name}.");
                        }
                        else
                        {
                            // If the player cannot carry the item, return it to Joe
                            joe.TakeItem(item);
                            player.WarningMessage($"You cannot carry {item.Name}. It is too heavy.");
                        }
                    }
                    else
                    {
                        player.WarningMessage($"{joe.Name} does not have an item named {itemName}.");
                    }
                }
                else
                {
                    player.WarningMessage("There is no one here to retrieve items from.");
                }
            }
            else
            {
                player.WarningMessage("Retrieve what?");
            }
            return false;
        }
    }
}