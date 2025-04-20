using System.Collections.Generic;
using AutoRoids.CustomClass.Game;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineShipDebris
    {
        internal readonly List<GameLine> lstLine;
        internal readonly int intColor;
        internal readonly double dblLineWidth;

        internal EngineShipDebris(List<GameLine> lstLine, int intColor, double dblLineWidth)
        {
            this.lstLine = lstLine;
            this.intColor = intColor;
            this.dblLineWidth = dblLineWidth;
        }
    }
}
