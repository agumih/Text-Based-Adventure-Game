using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StarterGame
{
    public class GiveToNPCCommand : Command
    {
        public GiveToNPCCommand()
        {
            this.Name = "give";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                string itemName = this.SecondWord;
                NPC joe = player.CurrentRoom.GetNPC("Joe");

                if (joe != null)
                {
                    // Check if the game has already been won
                    if (joe.HasPlayerWon())
                    {
                        player.InfoMessage("Joe has already declared you the winner!");
                        return false; // Exit early
                    }

                    IItem item = player.RemoveItem(itemName);
                    if (item != null)
                    {
                        if (joe.ReceiveItem(item))
                        {
                            // Only show the message if the item was successfully added to Joe's requests
                            player.InfoMessage($"You gave {item.Name} to {joe.Name}.");
                        }
                        else
                        {
                            // Return the item to the player's inventory if it's not needed
                            player.AddItem(item);
                            player.WarningMessage($"{joe.Name} doesn't need {item.Name}.");
                        }
                    }
                    else
                    {
                        player.WarningMessage($"You don't have an item named {itemName} to give.");
                    }
                }
                else
                {
                    player.WarningMessage("There is no one here to give your item to.");
                }
            }
            else
            {
                player.WarningMessage("Give what?");
            }
            return false;
        }
    }
}


