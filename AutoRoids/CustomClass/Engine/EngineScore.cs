using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineScore
    {
        internal List<BlockReference> lstBlkRefShip;
        internal Boolean bolReset = false;

        internal EngineScore(List<BlockReference> lstBlkRefShip)
        {
            this.lstBlkRefShip = lstBlkRefShip;
        }
    }
}