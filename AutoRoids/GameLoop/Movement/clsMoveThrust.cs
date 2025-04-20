using System;
using System.Collections.Generic;
using AutoRoids.Common.Enum;
using AutoRoids.GameLoop.Timers;

namespace AutoRoids.GameLoop.Movement
{
    internal class clsMoveThrust
    {
        internal Boolean Thrust(List<enumDirection> lstDirection, out Boolean bolToggle)
        {
            bolToggle = false;
            if (lstDirection.Contains(enumDirection.Up))
            {
                bolToggle = ToggleThrust();
                return false;
            }
            else
                clsTimers.GameStopWatch.StopWatchThrust.Stop();

            return true;
        }

        internal Boolean ToggleThrust()
        {
            Boolean rtnValue = false;

            if (clsTimers.GameStopWatch.StopWatchThrust.IsRunning)
            {
                int intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchThrust.ElapsedMilliseconds);

                if (intElapsed > 50)
                {
                    rtnValue = true;
                    clsTimers.GameStopWatch.StopWatchThrust.Restart();
                }
            }
            else
            {
                clsTimers.GameStopWatch.StopWatchThrust.Restart();
                rtnValue = true;
            }

            return rtnValue;
        }
    }
}