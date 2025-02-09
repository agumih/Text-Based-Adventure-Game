using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StarterGame
{
    public class GameClock
    {
        private System.Timers.Timer timer;
        private int _timeInGame;
        public int TimeInGame { get { return _timeInGame; } }

        public GameClock(int interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _timeInGame++;
            //Console.WriteLine("Tick! and the game time is " + TimeInGame);
            Notification notification = new Notification("GameClockTick", this);//this generates a notification everytime the clocks ticks
            NotificationCenter.Instance.PostNotification(notification);//this posts the notification. To use it, we just subsrcibe to the notification
        }
    }
}
