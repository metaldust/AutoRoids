using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.Common.Formulas
{
    internal class clsGetMatrix3d
    {
        internal List<Point2d> SetPointMatrix(List<Point2d> lstPoint, Matrix3d matrix3D)
        {
            List<Point2d> rtnValue = new List<Point2d>(lstPoint);

            for (int i = 0; i < rtnValue.Count; i++)
            {
                Point3d pt = rtnValue[i].ToPoint3d();
                pt = pt.TransformBy(matrix3D);
                rtnValue[i] = pt.ToPoint2d();
            }

            return rtnValue;
        }

        internal Matrix3d CreateTransformMatrix(Point3d ptOrigin, double dblAngleInDegrees, double dblScale)
        {
            // Create translation matrix for the origin
            Matrix3d translationMatrix = Matrix3d.Displacement(new Vector3d(ptOrigin.X, ptOrigin.Y, ptOrigin.Z));

            // Create rotation matrix
            Matrix3d rotationMatrix = Matrix3d.Rotation(dblAngleInDegrees.ToRadians(), Vector3d.ZAxis, Point3d.Origin);

            // Create scale matrix
            Matrix3d scaleMatrix = Matrix3d.Scaling(dblScale, Point3d.Origin);

            // Combine the matrices in the correct order: translation * rotation * scale
            Matrix3d transformMatrix = translationMatrix * rotationMatrix * scaleMatrix;

            return transformMatrix;
        }
    }
}