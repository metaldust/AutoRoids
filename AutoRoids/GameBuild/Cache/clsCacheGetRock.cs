using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.CustomClass.Game;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.Cache
{
    internal class clsCacheGetRock
    {
        internal static List<GameRockData> lstGameRockData = new List<GameRockData>();
        internal static List<EngineRock> lstEngineRock;
        internal static List<BlockReference> _lstRock;
        internal static List<Boolean> _lstIsVisible;
        internal static int intBlkRef;

        internal BlockReference GetRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl, Point2d pt, int intColorIndex)
        {
            if (_lstRock == null)
                _lstRock = new List<BlockReference>();
             
            if (_lstIsVisible == null)
                _lstIsVisible = new List<Boolean>();

            BlockReference acBlkRef = null;

            if (intBlkRef < _lstRock.Count)
            {
                acBlkRef = _lstRock[intBlkRef];

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
                clsCreateRock clsCreateRock = new clsCreateRock();

                //List<EngineRock> clsCreateRock = CreateGameRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                //                               int intRockCount, List<enumSize> lstRockSize, Boolean bolGameStart = true)


                clsAddBlock clsAddBlock = new clsAddBlock();

                acBlkRef = clsAddBlock.BuildBullet(acTrans, acDb, acBlkTbl, "Bullet", 3, 4 * StaticRock.dblGameScale);
                acBlkRef.Position = pt.ToPoint3d();
                acBlkRef.ColorIndex = intColorIndex;
                _lstRock.Add(acBlkRef);
                _lstIsVisible.Add(true);
            }

            intBlkRef++;

            return acBlkRef;
        }

        internal void HideRock(Transaction acTrans)
        {
            if (_lstRock != null)
            {
                int intCount = 0;
                if (StaticRock.lstBullets.Count > 0)
                    intCount = StaticRock.lstBullets.Count - 1;

                if (_lstIsVisible.Contains(true))
                {
                    for (int i = intCount; i < _lstRock.Count; i++)
                    {
                        if (_lstIsVisible[i])
                        {
                            BlockReference acBlkRef = _lstRock[i];
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