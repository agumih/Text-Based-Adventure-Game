using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    /*
     * Fall 2024
     */
    public class InsertCommand : Command
    {

        public InsertCommand() : base()
        {
            this.Name = "insert";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Insert(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nInsert Where?");
            }
            return false;
        }
    }
}