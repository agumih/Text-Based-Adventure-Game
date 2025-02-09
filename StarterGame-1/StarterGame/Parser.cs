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
    public class Parser
    {
        private Stack<CommandWords> _commands;
        private ParserState _state;

        public Parser() : this(new CommandWords()){}

        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            
            _commands = new Stack<CommandWords>();//we changed the commands to a stack.
            Push(newCommands);
            //_state = new NormalParserState(); previous one
            _state = new ParserState.NormalParserState();
            _state.Enter(this);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterBattle", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterTrade", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidExitBattle", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidExitTrade", HandleInput);
        }

        public void HandleInput(Notification notification)// we are telling the state that we are in to handle the input, if to exit or enter the state
        {
            ParserState potentialState = _state.HandleInput(this, notification.Name);
            if(potentialState != _state)
            {
                Player player = (Player )notification.Object;
                player.InfoMessage("Exiting " + _state.Name);//this and the above line is another way of announcing the current state the player is in.

                _state.Exit(this);
                _state = potentialState;

                player.InfoMessage("Entering " + _state.Name);
                _state.Enter(this);
               // Notification.Instance.AddObeserve("PlayerDidEnterBattle", HandleInput);
            }

        }
        public void Push(CommandWords commandsToPush)
        {
            _commands.Push(commandsToPush);
        }
        public CommandWords Pop()
        {
            return _commands.Pop();
        }

        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                command = _commands.Peek().Get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.SecondWord = words[1];
                    }
                    else
                    {
                        command.SecondWord = null;
                    }
                }
                else
                {
                    // This is debug line of code, should remove for regular execution
                    //Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                // This is a debug line of code
                //Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string Description()
        {
            return _commands.Peek().Description();
        }
    }
}
