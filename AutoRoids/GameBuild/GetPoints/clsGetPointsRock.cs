using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.GetPoints
{
    internal class clsGetPointsRock
    {
        internal List<Point2d> GetRock(int intPos, Matrix3d matrix3D)
        {
            List<Point2d> lstPoint = GetRock(intPos);

            for (int i = 0; i < lstPoint.Count; i++)
            {
                Point3d pt = lstPoint[i].ToPoint3d();
                pt = pt.TransformBy(matrix3D);
                lstPoint[i] = pt.ToPoint2d();
            }

            return lstPoint;
        }

        internal List<Point2d> GetRock(int intPos)
        {
            List<Point2d> lstRock0 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(5,0),
               new Point2d(8,2),
               new Point2d(7,4),
               new Point2d(8,6),
               new Point2d(6,8),
               new Point2d(4,6),
               new Point2d(2,8),
               new Point2d(0,6),
               new Point2d(0,2),
               new Point2d(2,0)
            };

            List<Point2d> lstRock1 = new List<Point2d>()
            {
               new Point2d(0,3),
               new Point2d(2,4),
               new Point2d(0,5),
               new Point2d(3,8),
               new Point2d(6,8),
               new Point2d(8,5),
               new Point2d(8,3),
               new Point2d(6,0),
               new Point2d(4,0),
               new Point2d(4,3),
               new Point2d(2,0),
               new Point2d(0,3)
            };

            List<Point2d> lstRock2 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(5,1),
               new Point2d(6,0),
               new Point2d(8,2),
               new Point2d(5,4),
               new Point2d(8,5),
               new Point2d(8,6),
               new Point2d(5,8),
               new Point2d(2,8),
               new Point2d(3,6),
               new Point2d(0,6),
               new Point2d(0,3),
               new Point2d(2,0),
            };

            List<Point2d> lstRock3 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(3,1),
               new Point2d(6,0),
               new Point2d(8,3),
               new Point2d(6,5),
               new Point2d(8,6),
               new Point2d(6,8),
               new Point2d(4,7),
               new Point2d(2,8),
               new Point2d(0,6),
               new Point2d(1,4),
               new Point2d(0,2),
               new Point2d(2,0)
            };

            switch (intPos)
            {
                case 0:
                    return lstRock0.AdjustToMiddle();

                case 1:
                    return lstRock1.AdjustToMiddle();

                case 2:
                    return lstRock2.AdjustToMiddle();

                default:
                    return lstRock3.AdjustToMiddle();
            }
        }
    }
}