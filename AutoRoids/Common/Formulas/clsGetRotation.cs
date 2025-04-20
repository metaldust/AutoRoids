using System;
using Autodesk.AutoCAD.Geometry;

namespace AutoRoids.Common.Formulas
{
    internal class clsGetRotation
    {
        internal Point3d RotatePoint(Point3d pointToRotate, Point3d centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            double pt1 = (cosTheta * (pointToRotate.X - centerPoint.X) -
                          sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X);

            double pt2 = (sinTheta * (pointToRotate.X - centerPoint.X) +
                         cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y);

            return new Point3d(pt1, pt2, 0);
        }
    }
}