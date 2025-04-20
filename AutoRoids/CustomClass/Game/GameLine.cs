using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.CustomClass.Game
{
    internal class GameLine
    {
        internal Point2d ptStart;
        internal Point2d ptEnd;

        internal GameLine(Point2d ptStart, Point2d ptEnd)
        {
            this.ptStart = ptStart;
            this.ptEnd = ptEnd;
        }
    }
}