using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;

namespace AutoRoids.CustomClass.Game
{
    internal class GameRockData
    {
        internal Point2d ptBlockOrigin;
        internal double dblBlockRotation;
        internal int intColorIndex;
        internal int intPosIndex;
        internal double dblAngle;
        internal double dblRotation;
        internal double dblDistance;
        internal double dblScale;
        internal double dblPolylineWidth;
        internal enumSize RockSize;

        internal GameRockData(enumSize rockSize,
                              Point2d ptBlockOrigin,
                              double dblBlockRotation,
                              int intColorIndex,
                              int intPosIndex,
                              double dblAngle,
                              double dblRotation,
                              double dblDistance,
                              double dblScale,
                              double dblPolylineWidth)

        {
            this.ptBlockOrigin = ptBlockOrigin;
            this.dblBlockRotation = dblBlockRotation;
            this.intColorIndex = intColorIndex;
            this.intPosIndex = intPosIndex;
            this.dblAngle = dblAngle;
            this.dblRotation = dblRotation;
            this.dblDistance = dblDistance;
            this.dblScale = dblScale;
            this.dblPolylineWidth = dblPolylineWidth;
            this.RockSize = rockSize;
        }
    }
}