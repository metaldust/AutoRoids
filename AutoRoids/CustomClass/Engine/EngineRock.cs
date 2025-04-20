using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineRock
    {
        internal BlockReference acBlkRef;
        internal string strBlockName;

        // Constant Values
        internal double dblScale;
        internal double dblDistance;

        internal int intColorIndex;
        internal enumSize RockSize;
        internal List<Point2d> lstOriginal;

        // Dynamic Values
        internal Point2d ptBlockOrigin;
        internal double dblBlockRotation;

        internal double dblRotation;
        internal double dblAngle;

        internal List<Point2d> lstMatrix3d;
        internal List<Point2d> lstBoundingBox;

        internal Boolean bolExploded;

        internal EngineRock(BlockReference acBlkRef, String strBlockName, Point2d ptBlockOrigin,
                            double dblBlockRotation, double dblScale,
                            double dblAngle, double dblRotation, double dblDistance,
                            List<Point2d> lstOriginal, enumSize rockSize, int intColorIndex)
        {
            this.acBlkRef = acBlkRef;
            this.strBlockName = strBlockName;
            this.ptBlockOrigin = ptBlockOrigin;
            this.dblScale = dblScale;
            this.dblBlockRotation = dblBlockRotation;
            this.dblAngle = dblAngle;
            this.dblRotation = dblRotation;
            this.dblDistance = dblDistance;
            this.lstOriginal = lstOriginal;
            this.RockSize = rockSize;
            this.intColorIndex = intColorIndex;
        }
    }
}