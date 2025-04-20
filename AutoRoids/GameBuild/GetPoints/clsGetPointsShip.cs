using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Formulas;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.GetPoints
{
    internal class clsGetPointsShip
    {
        internal List<Point2d> GetShip(int intPos, Matrix3d matrix3D)
        {
            List<Point2d> lstPoint = GetShip(intPos);

            for (int i = 0; i < lstPoint.Count; i++)
            {
                Point3d pt = lstPoint[i].ToPoint3d();
                pt = pt.TransformBy(matrix3D);
                lstPoint[i] = pt.ToPoint2d();
            }

            return lstPoint;
        }

        internal List<Point2d> GetShip(int intPos)
        {
            switch (intPos)
            {
                case 0:
                    return GetShip();

                case 1:
                    return GetThrust();

                case 2:
                    return GetInside();

                default:
                    return GetOutside();
            }
        }

        internal List<Point2d> GetShip()
        {
            List<Point2d> lstShip = new List<Point2d>()
            {
               new Point2d(-1, -1.5),
               new Point2d(0, 1.5),
               new Point2d(1,-1.5),
               new Point2d(0.5,-1),
               new Point2d(-0.5,-1),
               new Point2d(-1, -1.5)
            };

            // Adjust Center Point to be in the middle of circle.
            double dblOffset = 0.166;

            for (int i = 0; i < lstShip.Count; i++)
                lstShip[i] = new Point2d(lstShip[i].X, lstShip[i].Y + dblOffset);

            clsGetRotation clsGetRotation = new clsGetRotation();
            // Rotate Ship to align to Zero
            for (int i = 0; i < lstShip.Count; i++)
                lstShip[i] = clsGetRotation.RotatePoint(lstShip[i].ToPoint3d(), new Point3d(), -90).ToPoint2d();

            return lstShip;
        }

        internal List<Point2d> GetThrust()
        {
            List<Point2d> lstThrust = new List<Point2d>()
            {
               new Point2d(-0.5,-1),
               new Point2d(0,-2.0),
               new Point2d(0.5,-1),
            };

            double dblOffset = 0.166;
            // Adjust Center Point to be in the middle of circle.
            for (int i = 0; i < lstThrust.Count; i++)
                lstThrust[i] = new Point2d(lstThrust[i].X, lstThrust[i].Y + dblOffset);

            clsGetRotation clsGetRotation = new clsGetRotation();
            // Rotate Thrust to align to Zero
            for (int i = 0; i < lstThrust.Count; i++)
                lstThrust[i] = clsGetRotation.RotatePoint(lstThrust[i].ToPoint3d(), new Point3d(), -90).ToPoint2d();

            return lstThrust;
        }

        internal List<Point2d> GetInside()
        {
            clsGetPointsShield clsGetPointsShield = new clsGetPointsShield();

            return clsGetPointsShield.GetShield(6, 4.0);
        }

        internal List<Point2d> GetOutside()
        {
            clsGetPointsShield clsGetPointsShield = new clsGetPointsShield();

            return clsGetPointsShield.GetShield(6, 4.0);
        }
    }
}