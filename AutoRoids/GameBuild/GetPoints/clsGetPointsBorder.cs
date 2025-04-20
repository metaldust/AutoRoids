using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.GetPoints
{
    internal class clsGetPointsBorder
    {
        internal List<Point2d> GetBorder(int intPos, Boolean bolScale)
        {
            intPos = 1;

            List<Point2d> rtnValue = new List<Point2d>();
            List<Point2d> lstInside = new List<Point2d>()
            {
               new Point2d(0,0),
               new Point2d(36,0),
               new Point2d(36,24),
               new Point2d(0,24),
               new Point2d(0,0)
            };

            List<Point2d> lstOutside = new List<Point2d>()
            {
               new Point2d(0,0),
               new Point2d(37,0),
               new Point2d(37,25),
               new Point2d(0,25),
               new Point2d(0,0)
            };

            switch (intPos)
            {
                case 0:
                    rtnValue = lstOutside.AdjustToMiddle();
                    break;

                default:
                    rtnValue = lstInside.AdjustToMiddle();
                    break;
            }

            if (bolScale)
            {
                for (int k = 0; k < rtnValue.Count; k++)
                    rtnValue[k] *= 100;
            }

            return rtnValue;
        }
    }
}