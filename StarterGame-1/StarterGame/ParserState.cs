using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StarterGame
{

    public abstract class ParserState
    {
        public string Name { private set; get; }
        public abstract ParserState HandleInput(Parser parser, string input);
        public abstract void Enter(Parser parser);
        public abstract void Exit(Parser parser);

        public class NormalParserState : ParserState
        {
            public NormalParserState()
            {
                Name = "Normal Parser State";
            }

            override
            public void Enter(Parser parser)
            {
                Console.WriteLine("");
            }

            override
            public void Exit(Parser parser)
            {
                Console.WriteLine("Exiting Normal State");
            }

            override
            public ParserState HandleInput(Parser parser, string input)
            {
                switch (input)
                {
                    case "PlayerDidEnterBattle":
                        return new BattleParserState();

                    case "PlayerDidEnterTrade":
                        return new TradeParserState();

                    default:
                        return this; // Stay in Normal state
                }
            }
        }

        public class TradeParserState : ParserState
        {
            private CommandWords _tradeCommands;

            public TradeParserState()
            {
                Name = "Trade Parser State";
                Command[] commands = { new ExitTradeCommand() };
                _tradeCommands = new CommandWords(commands);
            }

            override
            public ParserState HandleInput(Parser parser, string input)
            {
                return input == "PlayerDidExitTrade" ? new NormalParserState() : this;
            }

            override
            public void Enter(Parser parser)
            {
                Console.WriteLine("Entering Trade State");
                parser.Push(_tradeCommands);
            }

            override
            public void Exit(Parser parser)
            {
                Console.WriteLine("Exiting Trade State");
                parser.Pop();
            }
        }

        public class BattleParserState : ParserState
        {
            private CommandWords _battleCommands;

            public BattleParserState()
            {
                Name = "Battle Parser State";
                Command[] commands = { new ExitBattleCommand() };
                _battleCommands = new CommandWords(commands);
            }

            override
            public ParserState HandleInput(Parser parser, string input)
            {
                return input == "PlayerDidExitBattle" ? new NormalParserState() : this;
            }

            override
            public  void Enter(Parser parser)
            {
                Console.WriteLine("Entering Battle State");
                parser.Push(_battleCommands);
            }

            override
            public  void Exit(Parser parser)
            {
                Console.WriteLine("Exiting Battle State");
                parser.Pop();
            }
        }
    }
}






    // public abstract class ParserState
    // {
    //     public string Name { private set; get; }
    //     public abstract ParserState HandleInput(Parser parser, string input);
    //     public abstract void Enter(Parser parser);
    //     public abstract void Exit(Parser parser);
        

    //     public class NormalParserState : ParserState
    //     {
    //         public NormalParserState()
    //         {
    //             Name = "Normal Parser State";
    //         }
    //         override
    //         //public abstract void Enter(Parser parser );
    //         public void Enter(Parser parser )
    //         {

    //         }
    //         //public abstract void exit(Parser parser );
    //         public void exit(Parser parser )
    //         {

    //         }
    //         public override ParserState HandleInput(Parser parser, string input)
    //         {
    //             // Determine state transitions based on input
    //             // For example, return new BattleParserState() on "PlayerDidEnterBattle"
    //             switch (input)
    //             {
    //                 case "PlayerDidEnterBattle":
    //                     return new BattleParserState();

    //                 case "PlayerDidEnterTrade":
    //                     return new TradeParserState();

    //                 default:
    //                     return this; // Stay in Normal state by default
    //             }
    //         }
    //     }



    //     public class TradeParserState :ParserState //do soemthing similar for the battlestate
    //     {
    //         CommandWords _tradeCommands;
    //         public TradeParserState()
    //         {
    //             Name = "Trade Parser State";
    //             Command[] _commands = { new ExitTradeCommand()};
    //             _tradeCommands = new CommandWords("Trade Commands", _commands);
    //         }
    //     }
    //     public class BattleParserState : ParserState//this is for the Battle state, for the normal state, we need to push the PlayerDid EnterBattle and PlayerDidExitBattle.
    //     {
    //         private CommandWords _battleCommands;
    //         public BattleParserState()
    //         {
    //             Command[] _commands = { new ExitBattleCommand() };
    //             _battleCommands = new CommandWords(_commands);
    //         }

    //         override
    //         public ParserState HandleInput(Parser parser, string input)
    //         {
    //             ParserState stateToReturn = this;
    //             switch(input)
    //             {
    //                 case "PlayerDidExitBattle":
    //                     stateToReturn = new NormalParserState();
    //                     break;
    //                 default:
    //                     break;
    //             }
    //             return stateToReturn;
    //         }
    //         override
    //         public void Enter(Parser parser)// need to put the exit and enter method in each state, battle, normal and trade.
    //         {
    //             //we need to push the battle commands
    //             parser.Push(_battleCommands);// we push these commands when we enter the battle state
    //         }
    //         override
    //         public void Exit(Parser parser)
    //         {
    //             parser.Pop();
    //         }
        
    //     }

    //     public class TradeParserState : ParserState
    //     {
    //         override
    //         public ParserState HandleInput(Parser parser, string input)
    //         {
    //             ParserState stateToReturn = this;
    //             switch (input)
    //             {
    //                 case "PlayerDidExitTrade":
    //                     stateToReturn = new NormalParserState();
    //                     break;
    //                 default:
    //                     break;
    //             }
    //             return stateToReturn;
    //         }
    //         override
    //         public void Enter(Parser parser)
    //         {
                
    //         }
    //     }
    //     public class BattleParserState : ParserState
    //     {
    //         private CommandWords _battleCommands;
    //         public BattleParserState()
    //         {
    //             Command[] _commands = { new ExitBattleCommand()};
    //             _battleCommands = new CommandWords();
    //         }
    //     }

        
    //     public class TradeParserState : ParserState// for the trade state, we also only put the case PlayerDidExitTrade
    //     {
    //         ParserState.Pop();
    //     }
        
    // }


// private CommandWords _battleCommands;
// public BattleParserState()
// {
//     _battleCommands = new CommandsWords();
// }
// ovewrride
// public

//