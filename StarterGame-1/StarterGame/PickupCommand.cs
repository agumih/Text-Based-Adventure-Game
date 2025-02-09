using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            this.Name = "pickup";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Pickup(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nPickup What?");
            }
            return false;
        }
    }
}