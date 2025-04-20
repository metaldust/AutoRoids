using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.GameLoop.Timers;

namespace AutoRoids.GameLoop.Movement
{
    internal class clsMoveHyperSpace
    {
        internal Boolean HyperSpace(List<enumDirection> lstDirection, ref Point2d ptOrigin)
        {
            Boolean rtnValue = false;
            if (lstDirection.Contains(enumDirection.Hyperspace))
            {
                clsCreatePoints clsGeneratePoints = new clsCreatePoints();

                if (clsTimers.GameStopWatch.StopWatchHyperSpace.IsRunning)
                {
                    int intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchHyperSpace.ElapsedMilliseconds);

                    if (intElapsed > 1000)
                    {
                        ptOrigin = clsGeneratePoints.GetPoint();
                        clsTimers.GameStopWatch.StopWatchHyperSpace.Restart();
                        rtnValue = true;
                    }
                }
                else
                {
                    ptOrigin = clsGeneratePoints.GetPoint();
                    clsTimers.GameStopWatch.StopWatchHyperSpace.Restart();
                    rtnValue = true;
                }
            }

            return rtnValue;
        }
    }
}