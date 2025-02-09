using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class PeekNPCCommand : Command
    {
        public PeekNPCCommand()//peeks at the items that the NPC is storing.
        {
            this.Name = "peek";
        }

        public override bool Execute(Player player)
        {
            NPC joe = player.CurrentRoom.GetNPC("Joe");
            if (joe != null)
            {
                string contents = joe.StorageContents();
                if (string.IsNullOrEmpty(contents))
                {
                    player.InfoMessage($"{joe.Name} has no items stored.");
                }
                else
                {
                    player.InfoMessage($"{joe.Name} is safeguarding the following items:\n{contents}");
                }
            }
            else
            {
                player.WarningMessage("There is no one here to peek into their storage.");
            }
            return false;
        }
    }
}