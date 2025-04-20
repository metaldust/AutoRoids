using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoRoids.GameLoop.Timers
{
    internal class clsTimerEvents
    {
        private static double dblAverage = 0;

        private static readonly List<double> lstAverage = new List<double>();

        internal double PrintTime(double dblCurrentTime)
        {
            if (lstAverage.Count > 120)
                lstAverage.RemoveAt(0);

            lstAverage.Add(dblCurrentTime);

            dblAverage = lstAverage.Count > 0 ? lstAverage.Average() : 0.0;

            dblAverage = Math.Round(dblAverage, 0);

            return dblAverage;
        }
    }
}