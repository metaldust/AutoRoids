using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.Geometry;
using AutoRoids.UserFormCode;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoRoids.Engine.EngineUpdate
{
    internal class clsUpdatePlayer
    {

        internal Boolean RemovePlayer(Transaction acTrans)
        {
            EngineScore engineScore = StaticRock.engineScore;
            if (engineScore != null)
            {
                if (engineScore.lstBlkRefShip != null)
                {
                    if (engineScore.lstBlkRefShip.Count > 0)
                    {
                        for (int i = engineScore.lstBlkRefShip.Count - 1; i >= 0; i--)
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineScore.lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference;
                            if (acBlkRef != null) acBlkRef.Erase();
                            engineScore.lstBlkRefShip.RemoveAt(i);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ErasePlayers(Transaction acTrans)
        {
            EngineScore engineScore = StaticRock.engineScore;
            if (engineScore != null)
            {
                if (engineScore.lstBlkRefShip != null)
                {
                    for (int i = 0; i < engineScore.lstBlkRefShip.Count; i++)
                    {
                        if (engineScore.lstBlkRefShip[i].IsValid(acTrans))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineScore.lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference;
                            if (acBlkRef != null) acBlkRef.Erase();
                        }
                    }
                }
            }
        }

        internal void AddDefaultPlayer()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    ErasePlayers(acTrans);

                    clsAddBlock clsAddBlock = new clsAddBlock();

                    List<BlockReference> lstBlockReference = new List<BlockReference>();

                    for (int i = 0; i < StaticRock.intPlayerNumber; i++)
                        AddPlayer(acTrans, acDb, acBlkTbl, ref lstBlockReference, i);

                    StaticRock.engineScore = new EngineScore(lstBlockReference);
                    acTrans.Commit();
                }
            }

            Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();        
            acDoc.Editor.Regen();
        }

        private void AddPlayer(Transaction acTrans, Database acDb, BlockTable acBlkTbl, ref List<BlockReference> lstBlockReference, int i)
        {
            clsReg clsReg = new clsReg();
            clsReg.GetGameScale(out double dblGameScale);

            double dblTop = 1130 - (44.149 * dblGameScale);
            double dblLeft = -1700;

            double dblWidth = 0.003;
            int intColor = 3;
            clsAddBlock clsAddBlock = new clsAddBlock();
            BlockReference acBlkRefShip = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Ship", intColor, 0, dblWidth, "Continuous", true, out List<Point2d> lstPointShip, true, out double dblScale);

            double dblRotation = 90;
            dblRotation = dblRotation.ToRadians();
            acBlkRefShip.Rotation = dblRotation;

            double dblOffset = 60 * i * StaticRock.dblGameScale;
            acBlkRefShip.Position = new Point3d(dblLeft + dblOffset, dblTop, 0);
            acBlkRefShip.ColorIndex = 3;

            lstBlockReference.Add(acBlkRefShip);
        }
    }
}