using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.CustomClass.Engine
{
    internal class EngineBackGround
    {
        internal readonly List<BlockReference> lstBlkRef;

        private readonly List<BlockReference> lstStar;
        private readonly List<Point2d> lstStarOrig;
        private readonly List<Point2d> lstStarCurr;


        internal EngineBackGround(List<BlockReference> lstBlkRefStar,
                                  List<BlockReference> lstBlkRefBorder,
                                  List<Point2d> lstPointOutside,
                                  List<Point2d> lstPointInside)

        {
            lstBlkRef = new List<BlockReference>();

            for (int i = 0; i < lstBlkRefStar.Count; i++)
                lstBlkRef.Add(lstBlkRefStar[i]);

            // Copy to Star for Physics
            lstStar = new List<BlockReference>();
            lstStarOrig = new List<Point2d>();
            lstStarCurr = new List<Point2d>();

            for (int i = 0; i < lstBlkRefStar.Count; i++)
            {
                lstStar.Add(lstBlkRefStar[i]);
                lstStarOrig.Add(lstBlkRefStar[i].Position.ToPoint2d());
                lstStarCurr.Add(lstBlkRefStar[i].Position.ToPoint2d());
            }

            for (int i = 0; i < lstBlkRefBorder.Count; i++)
                lstBlkRef.Add(lstBlkRefBorder[i]); ;
        }
    }
}