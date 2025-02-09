using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{

    public class InspectCommand : Command
    {
        public InspectCommand() : base()
        {
            this.Name = "inspect";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Inspect(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\ninspect What?");
            }
            return false;
        }
    }
}