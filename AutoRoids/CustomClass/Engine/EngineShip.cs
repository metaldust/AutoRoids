using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineShip
    {
        internal readonly List<BlockReference> lstBlkRefShip;
        internal readonly List<BlockReference> lstBlkRefShield;

        internal List<BlockReference> lstBlkRefBullet = new List<BlockReference>();

        // Constant Values
        internal readonly double dblScale;

        internal readonly List<List<Point2d>> lstLstOriginalShip;     
        // Dynamic Values

        internal Point2d ptBlockOrigin;
        internal double dblBlockRotation;

        internal List<List<Point2d>> lstLstMatrix3dShip = new List<List<Point2d>>();
        internal List<List<Point2d>> lstLstBoundingBoxShip = new List<List<Point2d>>();

        internal Boolean bolVisibleThrust;
        internal Boolean bolVisibleShield;
        internal readonly List<double> lstRotationShield;

        internal Boolean bolExplode = false;
        internal int intExplode = 0;
        private readonly double dblDistance;
        private readonly List<List<Point2d>> lstLstOriginalShield;

        internal EngineShip(List<BlockReference> lstBlkRefShip,
                            List<BlockReference> lstBlkRefShield,
                            List<List<Point2d>> lstLstOriginalShip,
                            List<List<Point2d>> lstLstOriginalShield,
                            List<double> lstRotationShield,
                            double dblScale, double dblDistance)
        {
            this.lstBlkRefShip = lstBlkRefShip;
            this.lstBlkRefShield = lstBlkRefShield;
            this.dblScale = dblScale;
            this.dblDistance = dblDistance;
            this.lstLstOriginalShip = lstLstOriginalShip;
            this.lstLstOriginalShield = lstLstOriginalShield;
            this.lstRotationShield = lstRotationShield;
        }
    }
}