using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.Common.Formulas
{
    internal class clsGetInsideBoundary
    {
        internal Boolean IsInside(Point2d pt, List<Point2d> lstPt)
        {
            return lstPt.Contains(pt) || IsInside(lstPt, pt);
        }

        private bool IsInside(List<Point2d> lstPoints, Point2d point)
        {
            bool result = false;
            Point2d a = lstPoints[lstPoints.Count - 1];
            foreach (Point2d b in lstPoints)
            {
                if ((b.X == point.X) && (b.Y == point.Y))
                    return true;

                if ((b.Y == a.Y) && (point.Y == a.Y))
                {
                    if ((a.X <= point.X) && (point.X <= b.X))
                        return true;

                    if ((b.X <= point.X) && (point.X <= a.X))
                        return true;
                }

                if ((b.Y < point.Y) && (a.Y >= point.Y) || (a.Y < point.Y) && (b.Y >= point.Y))
                {
                    if (b.X + (point.Y - b.Y) / (a.Y - b.Y) * (a.X - b.X) <= point.X)
                        result = !result;
                }
                a = b;
            }
            return result;
        }
    }
}