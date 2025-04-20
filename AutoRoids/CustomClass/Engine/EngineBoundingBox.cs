using System.Collections.Generic;
using AutoRoids.CustomClass.Game;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineBoundingBox
    {
        internal List<GameLine> lstLine;
        internal int intColor;

        internal EngineBoundingBox(List<GameLine> lstLine, int intColor)
        {
            this.lstLine = lstLine;
            this.intColor = intColor;   
        }
    }
}
