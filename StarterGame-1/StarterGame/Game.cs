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
    public class Game
    {
        private Player _player;
        private Parser _parser;
        private bool _playing;
        private GameClock _clock;
        private int _countDown = 60;

        public Game()
        {
            _clock = new GameClock(1000);
            //GameWorld gw = new GameWorld();
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance.Entrance);
           // NotificationCenter.Instance.AddObeserver("GameClockTick", GameClockTick); //this subscribes me to the clock ticking

        }


        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            if(_playing)
            {
                bool finished = false;
                while (!finished)
                {
                    Console.Write("\n>");
                    Command command = _parser.ParseCommand(Console.ReadLine());
                    if (command == null)
                    {
                        _player.ErrorMessage("I don't understand...");
                    }
                    else
                    {
                        finished = command.Execute(_player);//this is where the command is executed
                    }
                }
            }

        }


        public void Start()
        {
            _playing = true;
            _player.InfoMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        public string Welcome()
        {
            return "Welcome to the World of CSU!\n\nThe World of CSU is a new, incredibly boring adventure game.\n\nTo win this game, you have to find and satisfy all of Joe's needs\n\nType 'help' if you need help." + _player.CurrentRoom.Description();
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

        public void GameClockTick(Notification notification)
        {
            _countDown--;
            if(_countDown <= 0)
            {
                GameClock clock = (GameClock)notification.Object;
                Console.WriteLine("The time in game is " + clock.TimeInGame);
                Console.WriteLine("Time is up");
            }
        }

    }
}
