using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetExplode
    {
        internal static List<BlockReference> _lstExplode;
        internal static List<Boolean> _lstIsVisible;
        internal static int intBlkRef;

        internal BlockReference GetExplode(Transaction acTrans, Database acDb, BlockTable acBlkTbl, Point2d pt, int intColorIndex)
        {
            if (_lstExplode == null)
                _lstExplode = new List<BlockReference>();

            if (_lstIsVisible == null)
                _lstIsVisible = new List<Boolean>();

            BlockReference acBlkRef = null;

            if (intBlkRef < _lstExplode.Count)
            {
                acBlkRef = _lstExplode[intBlkRef];

                acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                acBlkRef.Position = pt.ToPoint3d();
                acBlkRef.Visible = true;
                acBlkRef.ColorIndex = intColorIndex;
                acBlkRef.ScaleFactors = new Scale3d(3 * StaticRock.dblGameScale);
                _lstIsVisible[intBlkRef] = true;
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();

                acBlkRef = clsAddBlock.BuildBullet(acTrans, acDb, acBlkTbl, "Explode", intColorIndex, 3 * StaticRock.dblGameScale);
                acBlkRef.Position = pt.ToPoint3d();
                _lstExplode.Add(acBlkRef);
                _lstIsVisible.Add(true);
            }

            intBlkRef++;

            return acBlkRef;
        }

        internal void HideExplode(Transaction acTrans)
        {
            if (_lstExplode != null)
            {
                int intCount = 0;
                if (StaticRock.lstExplode.Count > 0)
                    intCount = StaticRock.lstExplode.Count - 1;

                for (int i = intCount; i < _lstExplode.Count; i++)
                {
                    if (_lstIsVisible[i])
                    {
                        BlockReference acBlkRef = _lstExplode[i];
                        if (acBlkRef.ObjectId.IsValid && !acBlkRef.ObjectId.IsErased)
                        {
                            acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                            acBlkRef.Visible = false;
                            _lstIsVisible[i] = false;
                        }
                    }
                }
            }
        }
    }
}