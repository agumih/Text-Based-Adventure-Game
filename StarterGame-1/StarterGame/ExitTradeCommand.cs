using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class ExitTradeCommand : Command
    {
        public ExitTradeCommand() : base()
        {
            this.Name = "Exit Trade";
        }
        override
        public bool Execute(Player player)
        {
            throw new NotImplementedException();
        }

    }
}