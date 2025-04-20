using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.CustomClass.Game;

namespace AutoRoids.Common.Formulas
{
    internal class clsGetBoundingBox
    {
        internal void AddBoundingBox(List<Point2d> lstBox, int intIndexColor)
        {
            if (StaticRock.bolBoundingBox)
            {
                List<List<Point2d>> lstLstMyLine = lstBox.GroupIntoLines();

                List<GameLine> lstGameLine = new List<GameLine>();
                for (int i = 0; i < lstLstMyLine.Count; i++)
                    lstGameLine.Add(new GameLine(lstLstMyLine[i][0], lstLstMyLine[i][1]));

                if (StaticRock.lstBoundingBox == null)
                    StaticRock.lstBoundingBox = new List<EngineBoundingBox>();
       
                StaticRock.lstBoundingBox.Add(new EngineBoundingBox(lstGameLine, intIndexColor));
            }

            //if (StaticRock.bolBoundingBox)
            //{
            //    // if (intIndexColor == 3)
            //    {
            //        List<List<Point2d>> lstLstMyLine = lstBox.GroupIntoLines();


            //        for (int i = 0; i < lstLstMyLine.Count; i++)
            //            // clsCacheGetShip.GetPolyline(acTrans, acDb, lstLstMyLine[i], 3, 1 * StaticRock.dblGameScale);

            //            clsCacheGetShip.GetPolyline(acTrans, acDb, lstLstMyLine[i], 3, 1);



            //        List<GameLine> lstGameLine = new List<GameLine>();

            //        for (int i = 0; i < lstMyLine.Count; i++)
            //            lstGameLine.Add(new GameLine(lstMyLine[i][0], lstMyLine[i][1]));

            //        StaticRock.lstBoundingBox.Add(new EngineBoundingBox(lstGameLine, intIndexColor));
            //    }
            //}
        }

        internal void DrawingBoundingBox(Transaction acTrans, Database acDb)
        {

            //List<EngineBoundingBox> lstBoundingBox = StaticRock.lstBoundingBox;

            //clsCacheGetBoundingBox clsCacheGetBoundingBox = new clsCacheGetBoundingBox();

            //for (int i = 0; i < lstBoundingBox.Count; i++)
            //{
            //    for (int k = 0; k < lstBoundingBox[i].lstLine.Count; k++)
            //    {
            //        List<Point2d> lstLine = new List<Point2d> {
            //            lstBoundingBox[i].lstLine[k].ptStart,
            //            lstBoundingBox[i].lstLine[k].ptEnd };

            //        clsCacheGetBoundingBox.GetPolyline(acTrans, acDb, lstLine, lstBoundingBox[i].intColor, 1);
            //    }
            //}
        }


        internal List<Point2d> GetBoundingBox(List<Point2d> lstPoint)
        {
            if (lstPoint.Count > 0)
            {
                GetBoundingBox(lstPoint, out Point2d pt1, out Point2d pt3);

                Point2d pt2 = new Point2d(pt3.X, pt1.Y);
                Point2d pt4 = new Point2d(pt1.X, pt3.Y);

                return new List<Point2d> { pt1, pt2, pt3, pt4, pt1 };
            }
            else
                return new List<Point2d>();
        }

        private void GetBoundingBox(List<Point2d> lstPoint,
                                     out Point2d ptMin, out Point2d ptMax)
        {
            double X = 0;
            double Y = 0;

            ptMin = new Point2d
              (
                  X = lstPoint.Min(p => p.X),
                  Y = lstPoint.Min(p => p.Y)
              );

            ptMax = new Point2d
             (
                 X = lstPoint.Max(p => p.X),
                 Y = lstPoint.Max(p => p.Y)
             );
        }
    }
}