using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class ExitBattleCommand : Command
    {
        public ExitBattleCommand() : base()
        {
            this.Name = "exit";
        }

        override
        public bool Execute(Player player)
        {
            player.ExitBattle();
            return false;
        }
    }
}   