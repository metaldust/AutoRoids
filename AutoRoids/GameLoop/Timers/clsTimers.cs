using System.Diagnostics;

namespace AutoRoids.GameLoop.Timers
{
    internal static class clsTimers
    {
        internal static GameStopWatch GameStopWatch;
    }

    internal class GameStopWatch
    {
        internal Stopwatch StopWatchIdle;
        internal Stopwatch StopWatchHyperSpace;
        internal Stopwatch StopWatchShield;
        internal Stopwatch StopWatchBullet;
        internal Stopwatch StopWatchThrust;
        internal Stopwatch StopWatchRestart;

        internal GameStopWatch()
        {
            this.StopWatchIdle = new Stopwatch();
            this.StopWatchHyperSpace = new Stopwatch();
            this.StopWatchShield = new Stopwatch();
            this.StopWatchBullet = new Stopwatch();
            this.StopWatchThrust = new Stopwatch();
            this.StopWatchRestart = new Stopwatch();
        }
    }
}