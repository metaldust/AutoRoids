using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.GameBuild.GetPoints
{
    internal class clsGetPointsShield
    {
        //internal List<Point2d> GetShield(int intSides, Matrix3d matrix3D, double dblSize)
        //{
        //    List<Point2d> lstPoint = GetShield(intSides, dblSize);

        //    for (int i = 0; i < lstPoint.Count; i++)
        //    {
        //        Point3d pt = lstPoint[i].ToPoint3d();
        //        pt = pt.TransformBy(matrix3D);
        //        lstPoint[i] = pt.ToPoint2d();
        //    }

        //    return lstPoint;
        //}

        internal List<Point2d> GetShield(int intSides, double dblSize)
        {
            List<Point2d> lstShield = CalculatePolygonVertices(intSides, dblSize);

            return lstShield;
        }

        internal List<Point2d> CalculatePolygonVertices(int sides, double inscribedCircleRadius)
        {
            List<Point2d> rtnValue = new List<Point2d>();

            // Calculate the angle between each vertex
            double angleIncrement = 2 * Math.PI / sides;

            // Calculate the initial angle based on whether sides is even or odd
            double startAngle = sides % 2 == 0 ? -Math.PI / 2 - angleIncrement / 2 : -Math.PI / 2;

            // Calculate the coordinates of each vertex
            for (int i = 0; i < sides; i++)
            {
                double angle = startAngle + i * angleIncrement;
                double x = inscribedCircleRadius * Math.Cos(angle);
                double y = inscribedCircleRadius * Math.Sin(angle);
                rtnValue.Add(new Point2d(x, y));
            }

            rtnValue.Add(rtnValue[0]); // Close the polygon by adding the first vertex at the end

            return rtnValue;
        }
    }
}