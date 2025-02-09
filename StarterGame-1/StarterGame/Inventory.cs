using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
        }
        override
        public bool Execute(Player player)
        {
            if(this.HasSecondWord())
            {
                player.WarningMessage("\nOpen What?");
            }
            else
            {
                player.DisplayInventory();
            }
            return false;
        }
    }
}
