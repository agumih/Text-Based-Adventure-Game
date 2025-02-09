using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class AskCommand : Command
    {
        public AskCommand() : base() 
        {
            this.Name = "ask";
        }
        public override bool Execute(Player player)
        {
            NPC npc = player.CurrentRoom.GetNPC("joe"); // Look for Joe
            if (npc == null)
            {
                player.ErrorMessage("There is no one here to ask.");
                return false;
            }

            player.InfoMessage(npc.RespondToAsk());
            return false;
        }
    }
}