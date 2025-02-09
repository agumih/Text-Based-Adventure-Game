using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class WeightCommand : Command
    {
        public WeightCommand() : base()
        {
            this.Name = "weight";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("You are not carrying any weights");
            }
            else
            {
                player.DisplayWeight();
            }
            return false;
        }
    }
}