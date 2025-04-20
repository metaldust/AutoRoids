using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineExplode
    {
        internal Point2d ptOrigin;
        internal double dblAngle;
        internal double dblDistance;
        internal double dblTraveled;
        internal double dblMaxTraveled;
        internal int intColor;

        internal EngineExplode(Point2d ptOrigin, double dblAngle,
                               double dblMaxTraveled, int intColor)
        {
            this.ptOrigin = ptOrigin;
            this.dblAngle = dblAngle;
            this.intColor = intColor;
            this.dblMaxTraveled = dblMaxTraveled;
        }
    }
}