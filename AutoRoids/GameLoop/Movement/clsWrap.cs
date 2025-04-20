using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Formulas;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.GetPoints;

namespace AutoRoids.GameLoop.Movement
{
    internal class clsWrap
    {
        internal void WrapRock(ref EngineRock engineRock)
        {
            clsUpdateMatrix EngineFormula = new clsUpdateMatrix();
            EngineFormula.UpdateMatrix(ref engineRock);

            clsWrap clsWrap = new clsWrap();

            clsWrap.CalculateRiseOverRun(engineRock.dblAngle, 10.0, out double dblRise, out double dblRun);

            clsGetPointsBorder clsGetPointsBorder = new clsGetPointsBorder();
            List<Point2d> lstBorder = clsGetPointsBorder.GetBorder(1, true);

            clsGetBoundingBox clsGetBoundingBox = new clsGetBoundingBox();
            engineRock.lstBoundingBox = clsGetBoundingBox.GetBoundingBox(engineRock.lstMatrix3d);

            if (clsWrap.WrapPosition(engineRock.lstBoundingBox, lstBorder, dblRise, dblRun, engineRock.intColorIndex, true, ref engineRock.ptBlockOrigin))
                EngineFormula.UpdateMatrix(ref engineRock);
        }

        internal void WrapShip(ref EngineShip EngineShip, Double dblAngle)
        {
            clsUpdateMatrix EngineFormula = new clsUpdateMatrix();
            EngineFormula.UpdateMatrix(ref EngineShip);

            clsWrap clsWrap = new clsWrap();
            clsWrap.CalculateRiseOverRun(dblAngle, 10.0, out double dblRise, out double dblRun);

            clsGetPointsBorder clsGetPointsBorder = new clsGetPointsBorder();
            List<Point2d> lstBorder = clsGetPointsBorder.GetBorder(1, true);

            clsGetBoundingBox clsGetBoundingBox = new clsGetBoundingBox();
            EngineShip.lstLstBoundingBoxShip = new List<List<Point2d>>();
            for (int i = 0; i < EngineShip.lstLstMatrix3dShip.Count; i++)
                EngineShip.lstLstBoundingBoxShip.Add(clsGetBoundingBox.GetBoundingBox(EngineShip.lstLstMatrix3dShip[i]));

            if (WrapPosition(EngineShip.lstLstBoundingBoxShip[0], lstBorder, dblRise, dblRun, 3, true, ref EngineShip.ptBlockOrigin))
                EngineFormula.UpdateMatrix(ref EngineShip);
        }

        //internal void ShowBoundingBox(EngineShip EngineShip)
        //{
        //    clsCacheGetPolyline clsCache = new clsCacheGetPolyline();

        //    if (StaticRock.bolBoundingBox)
        //    {
        //        for (int i = 0; i < EngineShip.lstBoundingBoxShip.Count; i++)
        //        {
        //            List<List<Point2d>> lstMyLine = EngineShip.lstBoundingBoxShip[i].GroupIntoLines();

        //            for (int k = 0; k < lstMyLine.Count; k++)
        //                clsCache.GetPolyline(acTrans, acDb, lstMyLine[k], 3, 0);
        //            break;
        //        }
        //    }
        //}

        //internal void ShowBoundingBox(Transaction acTrans, Database acDb, EngineRock engineRock)
        //{
        //    clsCacheGetPolyline clsCache = new clsCacheGetPolyline();

        //    if (StaticRock.bolBoundingBox)
        //    {
        //        List<List<Point2d>> lstMyLine = engineRock.lstBoundingBox.GroupIntoLines();

        //        for (int k = 0; k < lstMyLine.Count; k++)
        //            clsCache.GetPolyline(acTrans, acDb, lstMyLine[k], engineRock.intColorIndex, 0);
        //    }
        //}

        internal void CalculateRiseOverRun(double angleInDegrees, double distance, out double rise, out double run)
        {
            // Convert angle to radians (Math.Sin and Math.Cos functions in C# use radians)
            double angleInRadians = angleInDegrees * Math.PI / 180.0;

            // Calculate rise and run
            rise = distance * Math.Sin(angleInRadians);
            run = distance * Math.Cos(angleInRadians);
        }

        internal Boolean WrapPosition(List<Point2d> lstBox, List<Point2d> lstBorder,
                                      double dblRise, double dblRun, int intColorIndex, Boolean bolAddBounding, ref Point2d ptPosition)
        {
            if (bolAddBounding)
            {
                clsGetBoundingBox clsGetBoundingBox = new clsGetBoundingBox();
                clsGetBoundingBox.AddBoundingBox(lstBox, intColorIndex);
            }

            Boolean rtnValue = false;
            if (lstBox.Count == 5 && lstBorder.Count == 5)
            {
                double dblHeight = lstBox[3].Y - lstBox[0].Y;
                double dblWidth = lstBox[1].X - lstBox[0].X;

                double dblLeft = lstBox[0].X;
                double dblRight = lstBox[1].X;
                double dblBottom = lstBox[0].Y;
                double dblTop = lstBox[3].Y;

                double dblBorderLeft = lstBorder[0].X;
                double dblBorderRight = lstBorder[1].X;
                double dblBorderBottom = lstBorder[0].Y;
                double dblBorderTop = lstBorder[3].Y;

                // Left
                if (dblBorderRight < dblLeft && dblRun > 0)
                {
                    ptPosition = new Point2d(dblBorderLeft - (dblWidth / 2), ptPosition.Y);
                    rtnValue = true;
                }

                // Right
                if (dblBorderLeft > dblRight && dblRun < 0)
                {
                    ptPosition = new Point2d(dblBorderRight + (dblWidth / 2), ptPosition.Y);
                    rtnValue = true;
                }

                // Top
                if (dblBorderBottom > dblTop && dblRise < 0)
                {
                    ptPosition = new Point2d(ptPosition.X, dblBorderTop + (dblHeight / 2));
                    rtnValue = true;
                }

                // Bottom
                if (dblBorderTop < dblBottom && dblRise > 0)
                {
                    ptPosition = new Point2d(ptPosition.X, dblBorderBottom - (dblHeight / 2));
                    rtnValue = true;
                }
            }

            return rtnValue;
        }
    }
}