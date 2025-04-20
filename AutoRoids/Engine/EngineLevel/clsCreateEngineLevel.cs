using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using AutoRoids.Common.Static;
using AutoRoids.Engine.EngineUpdate;
using AutoRoids.GameBuild.Cache;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.GameLoop.Movement;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoRoids.Engine.EngineLevel
{
    internal class clsCreateEngineLevel
    {
        internal void CreateGrid(Boolean bolReset)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    // BlockTableRecord acBTR = (BlockTableRecord)acTrans.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite);
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    SetDefaultValues(acTrans, bolReset);
                    SetDefaultBlocks(acTrans, acDb, acBlkTbl, bolReset);
                }

                acTrans.Commit();
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            dynamic acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
            acadApp.ZoomExtents();
            acDoc.Editor.Regen();
        }

        internal void SetDefaultValues(Transaction acTrans, Boolean bolReset)
        {
            clsCreateCommon clsCreateCommon = new clsCreateCommon();
            clsCreateCommon.SetDefaultProperties();

            StaticRock.SetStaticRegistry(bolReset);

            clsCacheGetBoundingBox clsCacheGetBoundingBox = new clsCacheGetBoundingBox();
            clsCacheGetBoundingBox.HideBoundingBox(acTrans);

            clsCacheGetDebris clsCacheGetShip = new clsCacheGetDebris();
            clsCacheGetShip.HideDebris(acTrans);


            if (bolReset)
            {
                clsMoveShip.dblLocalAngle = 0.0;
                clsMoveShip.dblAccelerationAngle = 0.0;
                clsMoveShip.dblVelocity = 0.0;

                clsCreateExplode clsCreateExplode = new clsCreateExplode();
                clsCreateExplode.EraseGameExplode();

                clsCreateBullet clsCreateBullet = new clsCreateBullet();
                clsCreateBullet.EraseGameBullets();

                clsUpdatePlayer clsUpdatePlayer = new clsUpdatePlayer();
                clsUpdatePlayer.AddDefaultPlayer();
            }
        }

        internal void SetDefaultBlocks(Transaction acTrans, Database acDb, BlockTable acBlkTbl, Boolean bolReset)
        {
            if (bolReset)
            {
                clsCreateEngineShip clsCreateEngineShip = new clsCreateEngineShip();
                StaticRock.engineShip = clsCreateEngineShip.CreateShip(acTrans, acDb, acBlkTbl);

                clsCreateEngineBackGround clsCreateEngineBackGround = new clsCreateEngineBackGround();
                StaticRock.engineBackGround = clsCreateEngineBackGround.CreateBackGround(acTrans, acDb, acBlkTbl);
            }

            clsCreateEngineRock clsCreateEngineRock = new clsCreateEngineRock();
            StaticRock.lstEngineRock = clsCreateEngineRock.CreateGameRock(acTrans, acDb, acBlkTbl, bolReset);
        }
    }
}