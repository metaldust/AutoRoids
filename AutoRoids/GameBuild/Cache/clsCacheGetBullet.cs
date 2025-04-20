using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetBullet
    {
        internal static List<BlockReference> _lstBullet;
        internal static List<Boolean> _lstIsVisible;
        internal static int intBlkRef;

        internal BlockReference GetBullet(Transaction acTrans, Database acDb, BlockTable acBlkTbl, Point2d pt, int intColorIndex)
        {
            if (_lstBullet == null)
                _lstBullet = new List<BlockReference>();

            if (_lstIsVisible == null)
                _lstIsVisible = new List<Boolean>();

            BlockReference acBlkRef = null;

            if (intBlkRef < _lstBullet.Count)
            {
                acBlkRef = _lstBullet[intBlkRef];

                if (acBlkRef.IsValid(acTrans))
                {
                    acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                    acBlkRef.Position = pt.ToPoint3d();
                    acBlkRef.Visible = true;
                    acBlkRef.ColorIndex = intColorIndex;
                    acBlkRef.ScaleFactors = new Scale3d(4 * StaticRock.dblGameScale);
                }
                _lstIsVisible[intBlkRef] = true;
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();

                acBlkRef = clsAddBlock.BuildBullet(acTrans, acDb, acBlkTbl, "Bullet", 3, 4 * StaticRock.dblGameScale);
                acBlkRef.Position = pt.ToPoint3d();
                acBlkRef.ColorIndex = intColorIndex;
                _lstBullet.Add(acBlkRef);
                _lstIsVisible.Add(true);
            }

            intBlkRef++;

            return acBlkRef;
        }

        internal void HideBullet(Transaction acTrans)
        {
            if (_lstBullet != null)
            {
                int intCount = 0;
                if (StaticRock.lstBullets.Count > 0)
                    intCount = StaticRock.lstBullets.Count - 1;

                if (_lstIsVisible.Contains(true))
                {
                    for (int i = intCount; i < _lstBullet.Count; i++)
                    {
                        if (_lstIsVisible[i])
                        {
                            BlockReference acBlkRef = _lstBullet[i];
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
}