
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class BackCommand : Command
    {
        public BackCommand()
        {
            this.Name = "back";
        }

        public override bool Execute(Player player)
        {
            player.Back();
            return false;
        }
    }
}