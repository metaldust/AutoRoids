using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.Cache;
using AutoRoids.GameLoop.Movement;

namespace AutoRoids.Engine.EngineUpdate
{
    internal class clsUpdateRock
    {
        internal void UpdateRockDataFrame()
        {
            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;

            if (lstEngineRock != null)
            {
                for (int i = 0; i < lstEngineRock.Count; i++)
                {
                    EngineRock engineRock = lstEngineRock[i];

                    engineRock.ptBlockOrigin =
                        engineRock.ptBlockOrigin.CalculatePoint(engineRock.dblAngle, engineRock.dblDistance);

                    engineRock.dblBlockRotation =
                        (engineRock.dblBlockRotation += engineRock.dblRotation).NormalizeAngle();

                    clsWrap clsWrap = new clsWrap();
                    clsWrap.WrapRock(ref engineRock);

                    lstEngineRock[i] = engineRock;
                }
            }
        }

        internal void UpdateGraphics(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            ProcessRock(acTrans);

            ProcessShip(acTrans);

            ProcessShield(acTrans);

            ProcessBullet(acTrans, acDb, acBlkTbl);

            ProcessExplode(acTrans, acDb, acBlkTbl);

            ProcessDebris(acTrans, acDb);

            ProcessBoundingBox(acTrans, acDb);
        }

        internal void ProcessRock(Transaction acTrans)
        {
            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;

            if (lstEngineRock != null)
            {
                for (int i = lstEngineRock.Count - 1; i >= 0; i--)
                {
                    EngineRock engineRock = lstEngineRock[i];

                    if (engineRock.acBlkRef.IsValid(acTrans))
                    {
                        if (!engineRock.bolExploded)
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineRock.acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                            acBlkRef.Position = engineRock.ptBlockOrigin.ToPoint3d();
                            acBlkRef.Rotation = engineRock.dblBlockRotation.ToRadians();
                        }
                        else
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineRock.acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                            acBlkRef.Erase();
                            lstEngineRock.RemoveAt(i);
                        }
                    }
                }
            }
        }

        internal void ProcessShield(Transaction acTrans)
        {
            EngineShip EngineShip = StaticRock.engineShip;
            for (int i = 0; i < EngineShip.lstBlkRefShield.Count; i++)
            {
                BlockReference acBlkRef = EngineShip.lstBlkRefShield[i];
                if (acBlkRef.IsValid(acTrans))
                {
                    acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;

                    if (EngineShip.bolVisibleShield)
                    {
                        acBlkRef.Visible = true;
                        acBlkRef.Rotation = EngineShip.lstRotationShield[i].ToRadians();
                        acBlkRef.Position = EngineShip.ptBlockOrigin.ToPoint3d();
                    }
                    else
                    {
                        if (acBlkRef.Visible == true)
                            acBlkRef.Visible = false;
                    }
                }
            }
        }

        internal void ProcessShip(Transaction acTrans)
        {
            EngineShip EngineShip = StaticRock.engineShip;
            for (int i = 0; i < EngineShip.lstBlkRefShip.Count; i++)
            {
                BlockReference acBlkRef = EngineShip.lstBlkRefShip[i];
                if (acBlkRef.IsValid(acTrans))
                {
                    acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;

                    acBlkRef.Position = EngineShip.ptBlockOrigin.ToPoint3d();

                    acBlkRef.Rotation = EngineShip.dblBlockRotation.ToRadians();

                    if (i == 1)
                    {
                        if (!EngineShip.bolExplode)
                            acBlkRef.Visible = EngineShip.bolVisibleThrust;
                        else
                            acBlkRef.Visible = false;
                    }
                    else
                    {
                        if (!EngineShip.bolExplode)
                            acBlkRef.Visible = true;
                        else
                            acBlkRef.Visible = false;
                    }
                }
            }
        }

        internal void ProcessBullet(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            clsCacheGetBullet clsCacheGetBullet = new clsCacheGetBullet();
            clsCacheGetBullet.HideBullet(acTrans);

            if (StaticRock.lstBullets != null)
            {
                for (int i = 0; i < StaticRock.lstBullets.Count; i++)
                {
                    EngineBullet engineBullet = StaticRock.lstBullets[i];

                    clsCacheGetBullet.GetBullet(acTrans, acDb, acBlkTbl, engineBullet.ptOrigin, engineBullet.intColor);
                }
            }
        }

        internal void ProcessExplode(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            if (StaticRock.lstExplode != null)
            {
                clsCacheGetExplode clsCacheGetExplode = new clsCacheGetExplode();
                clsCacheGetExplode.HideExplode(acTrans);

                for (int i = 0; i < StaticRock.lstExplode.Count; i++)
                {
                    EngineExplode engineExplode = StaticRock.lstExplode[i];

                    clsCacheGetExplode.GetExplode(acTrans, acDb, acBlkTbl, engineExplode.ptOrigin, engineExplode.intColor);
                }
            }
        }


        internal void ProcessDebris(Transaction acTrans, Database acDb)
        {
            clsCacheGetDebris clsCacheGetDebris = new clsCacheGetDebris();

            if (StaticRock.lstShipDebris != null)
            {
                if (StaticRock.lstShipDebris.Count > 0)
                {
                    for (int i = 0; i < StaticRock.lstShipDebris.Count; i++)
                    {
                        EngineShipDebris ShipDebris = StaticRock.lstShipDebris[i];

                        for (int j = 0; j < ShipDebris.lstLine.Count; j++)
                        {
                            List<Point2d> lstPoints = new List<Point2d> { ShipDebris.lstLine[j].ptStart, ShipDebris.lstLine[j].ptEnd };
                            clsCacheGetDebris.GetDebris(acTrans, acDb, lstPoints, ShipDebris.intColor, ShipDebris.dblLineWidth);
                        }
                    }
                }
                else
                    clsCacheGetDebris.HideDebris(acTrans);
            }
        }

        internal void ProcessBoundingBox(Transaction acTrans, Database acDb)
        {
            clsCacheGetBoundingBox clsCacheGetBoundingBox = new clsCacheGetBoundingBox();

            if (StaticRock.lstBoundingBox != null)
            {
                if (StaticRock.lstBoundingBox.Count > 0)
                {             
                    clsCacheGetBoundingBox.HideRemaining(acTrans);

                    for (int i = 0; i < StaticRock.lstBoundingBox.Count; i++)
                    {
                        EngineBoundingBox ShipDebris = StaticRock.lstBoundingBox[i];

                        for (int j = 0; j < ShipDebris.lstLine.Count; j++)
                        {
                            List<Point2d> lstPoints = new List<Point2d> { ShipDebris.lstLine[j].ptStart, ShipDebris.lstLine[j].ptEnd };
                            clsCacheGetBoundingBox.GetBoundingBox(acTrans, acDb, lstPoints, ShipDebris.intColor, 1);
                        }
                    }
                }
                else
                    clsCacheGetBoundingBox.HideBoundingBox(acTrans);
            }
        }



    }
}