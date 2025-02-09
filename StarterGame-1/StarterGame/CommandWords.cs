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
    public class CommandWords
    {
        private Dictionary<string, Command> _commands;
        public string Name { private set; get; }
        private static Command[] _commandArray = { new GoCommand(), new QuitCommand(), new OpenCommand(), new SayCommand(), new UnlockCommand(), new InsertCommand(), new InspectCommand(), new PickupCommand(), new InventoryCommand(), new DropCommand(), new WeightCommand(), new GiveToNPCCommand(), new PeekNPCCommand(), new RetrieveCommand(), new AskCommand(), new BackCommand()} ;// new EnterBattleCommand(), new EnterTradeCommand(), new exitTradeCommand()};

        public CommandWords() : this(_commandArray) {}
       

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            _commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                _commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            _commands[help.Name] = help;
        }


        public Command Get(string word)
        {
            Command command = null;
            _commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = Name + ": ";
            Dictionary<string, Command>.KeyCollection keys = _commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
