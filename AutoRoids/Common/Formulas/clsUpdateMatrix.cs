using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;

namespace AutoRoids.Common.Formulas
{
    internal class clsUpdateMatrix
    {
        internal void UpdateMatrix(ref EngineRock engineRock)
        {
            clsGetMatrix3d clsGetMatrix3d = new clsGetMatrix3d();

            Matrix3d Matrix3d =
                    clsGetMatrix3d.CreateTransformMatrix(engineRock.ptBlockOrigin.ToPoint3d(),
                                                         engineRock.dblBlockRotation, engineRock.dblScale);

            engineRock.lstMatrix3d = clsGetMatrix3d.SetPointMatrix(engineRock.lstOriginal, Matrix3d);
        }

        internal void UpdateMatrix(ref EngineShip engineShip)
        {
            clsGetMatrix3d clsGetMatrix3d = new clsGetMatrix3d();

            Matrix3d Matrix3d =
                    clsGetMatrix3d.CreateTransformMatrix(engineShip.ptBlockOrigin.ToPoint3d(),
                                                         engineShip.dblBlockRotation, engineShip.dblScale);

            engineShip.lstLstMatrix3dShip = new List<List<Point2d>>();

            for (int i = 0; i < engineShip.lstLstOriginalShip.Count; i++)
                engineShip.lstLstMatrix3dShip.Add(clsGetMatrix3d.SetPointMatrix(engineShip.lstLstOriginalShip[i], Matrix3d));
        }
    }
}