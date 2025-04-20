using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.Common.Static
{
    internal static class clsPoints
    {
        internal static List<List<Point2d>> GroupIntoLines(this List<Point2d> lstPoint)
        {
            List<Point2d> _lstPoint = lstPoint.ToList();

            List<List<Point2d>> rtnValue = new List<List<Point2d>>();

            //List<double> lstDistance = new List<double>();

            //for (int i = 1; i < _lstPoint.Count; i++)
            //{
            //    Point2d pt1 = _lstPoint[i - 1];
            //    Point2d pt2 = _lstPoint[i];

            //    lstDistance.Add(pt1.GetDistanceTo(pt2));
            //}

            for (int i = 1; i < _lstPoint.Count; i++)
            {
                List<Point2d> lstLine = new List<Point2d>
                {
                    _lstPoint[i - 1],
                    _lstPoint[i]
                };
                rtnValue.Add(lstLine);
            }

            return rtnValue;
        }

        private static Point2d GetMiddleOffset(this List<Point2d> lstPoint)
        {
            double minX = int.MaxValue;
            double minY = int.MaxValue;
            double maxX = int.MinValue;
            double maxY = int.MinValue;

            foreach (Point2d point in lstPoint)
            {
                minX = Math.Min(minX, point.X);
                minY = Math.Min(minY, point.Y);
                maxX = Math.Max(maxX, point.X);
                maxY = Math.Max(maxY, point.Y);
            }

            double width = maxX - minX;
            double height = maxY - minY;

            return new Point2d(width / 2.0, height / 2.0);
        }

        internal static List<Point2d> AdjustToMiddle(this List<Point2d> lstPoint)
        {
            Point2d pt = GetMiddleOffset(lstPoint);

            for (int i = 0; i < lstPoint.Count; i++)
                lstPoint[i] = new Point2d(lstPoint[i].X - pt.X, lstPoint[i].Y - pt.Y);

            return lstPoint;
        }
    }
}