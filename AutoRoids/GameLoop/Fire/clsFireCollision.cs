using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Formulas;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.GameLoop.Movement;

namespace AutoRoids.GameLoop.Fire
{
    internal class clsFireCollision
    {
        internal void OffsetExplode()
        {
            List<EngineExplode> lstExplode = StaticRock.lstExplode;

            if (lstExplode != null)
            {
                double dblOffset = StaticRock.dblExplodeSpeed;

                for (int i = lstExplode.Count - 1; i >= 0; i--)
                {
                    EngineExplode GameExplode = lstExplode[i];

                    if (GameExplode.dblTraveled < GameExplode.dblMaxTraveled * 4.5)
                    {
                        Point2d ptOrigin = GameExplode.ptOrigin;
                        double dblAngle = GameExplode.dblAngle;

                        ptOrigin = ptOrigin.CalculatePoint(dblAngle, dblOffset);
                        // Create larger explosion when ship is moving
                        GameExplode.dblTraveled += (dblOffset - (clsMoveShip.dblVelocity / 2));

                        GameExplode.ptOrigin = ptOrigin;
                        lstExplode[i] = GameExplode;
                    }
                    else
                        StaticRock.lstExplode.RemoveAt(i);
                }
            }
        }

        internal Boolean RockCollision(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            for (int k = StaticRock.lstEngineRock.Count - 1; k >= 0; k--)
            {
                for (int j = StaticRock.lstEngineRock.Count - 1; j >= 0; j--)
                {
                    if (k != j)
                    {
                        EngineRock rock1 = StaticRock.lstEngineRock[k];
                        EngineRock rock2 = StaticRock.lstEngineRock[j];

                        if (!rock1.bolExploded && !rock2.bolExploded)
                        {
                            List<Point2d> lstRock1 = rock1.lstMatrix3d;
                            List<Point2d> lstRock2 = rock2.lstMatrix3d;

                            if (CheckInside(acTrans, acDb, acBlkTbl, rock1, rock2, lstRock1, lstRock2, j) || 
                                CheckInside(acTrans, acDb, acBlkTbl, rock2, rock1, lstRock2, lstRock1, k))
                                    return true;

                        }
                    }

                }
            }
            return false;
        }

        internal Boolean CheckInside(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                  EngineRock rock1, EngineRock rock2, List<Point2d> lstRock1, List<Point2d> lstRock2, int j)
        {
            for (int p = 0; p < lstRock1.Count; p++)
            {
                if (lstRock1 != null && lstRock2 != null)
                {
                    clsGetInsideBoundary clsGetInsideBoundary = new clsGetInsideBoundary();
                    if (clsGetInsideBoundary.IsInside(lstRock1[p], lstRock2))
                    {
                        double dblAngle = rock1.ptBlockOrigin.GetAngle(rock2.ptBlockOrigin);
                        ExplodeRock(acTrans, acDb, acBlkTbl, rock2, j, dblAngle);
                        StaticRock.lstEngineRock[j].bolExploded = true;
                        return true;
                    }
                }
            }

            return false;
        }


        internal Boolean ShipCollision(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            EngineShip engineShip = StaticRock.engineShip;

            // Matrix is Set at Wrap
            List<Point2d> lstShip = engineShip.lstLstMatrix3dShip[0];

            for (int k = StaticRock.lstEngineRock.Count - 1; k >= 0; k--)
            {
                EngineRock engineRock = StaticRock.lstEngineRock[k];

                if (!engineRock.bolExploded)
                {
                    List<Point2d> lstRock = engineRock.lstMatrix3d;

                    for (int p = 0; p < lstShip.Count; p++)
                    {
                        if (lstShip != null && lstRock != null)
                        {
                            clsGetInsideBoundary clsGetInsideBoundary = new clsGetInsideBoundary();
                            if (clsGetInsideBoundary.IsInside(lstShip[p], lstRock))
                            {
                                double dblAngle = engineShip.ptBlockOrigin.GetAngle(engineRock.ptBlockOrigin);
                                ExplodeRock(acTrans, acDb, acBlkTbl, engineRock, k, dblAngle);
                                StaticRock.lstEngineRock[k].bolExploded = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


        internal void FireCollision(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            for (int i = StaticRock.lstBullets.Count - 1; i >= 0; i--)
            {
                EngineBullet engineBullets = StaticRock.lstBullets[i];

                Point2d pt = engineBullets.ptOrigin;
                double dblAngle = engineBullets.dblAngle;

                if (FireCollision(acTrans, acDb, acBlkTbl, pt, dblAngle))
                    StaticRock.lstBullets[i].dblTraveled = StaticRock.dblBulletMaxTravel;
            }
        }

        internal Boolean FireCollision(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                       Point2d pt, double dblAngle)
        {
            Boolean rtnValue = false;

            List<Point2d> lstOrigin = new List<Point2d>();
            List<Double> lstAngle = new List<Double>();
            List<enumSize> lstSize = new List<enumSize>();
            List<int> lstIndexColor = new List<int>();
            // List<Double> lstDistance = new List<double>();
            List<Double> lstScale = new List<double>();
            List<int> lstColor = new List<int>();

            for (int k = StaticRock.lstEngineRock.Count - 1; k >= 0; k--)
            {
                EngineRock engineRock = StaticRock.lstEngineRock[k];

                if (!engineRock.bolExploded)
                {
                    List<Point2d> lstMatrix3d = engineRock.lstMatrix3d;
                    clsGetInsideBoundary clsGetInsideBoundary = new clsGetInsideBoundary();
                    if (clsGetInsideBoundary.IsInside(pt, lstMatrix3d))
                    {
                        // Delete Bullet
                        rtnValue = true;

                        lstOrigin.Add(engineRock.ptBlockOrigin);
                        lstAngle.Add(dblAngle);
                        lstSize.Add(StaticRock.lstEngineRock[k].RockSize);
                        lstIndexColor.Add(engineRock.intColorIndex);
                        //lstDistance.Add(StaticRock.lstEngineRock[k].dblDistance * 2);
                        lstScale.Add(engineRock.dblScale);
                        lstColor.Add(engineRock.intColorIndex);

                        // Delete Rock
                        StaticRock.lstEngineRock[k].bolExploded = true;
                    }
                }
            }

            if (lstOrigin.Count > 0)
            {
                AddNewRock(acTrans, acDb, acBlkTbl, lstOrigin,
                           lstAngle,
                           lstSize,
                           lstIndexColor);


                AddExplode(lstOrigin,
                           lstScale,
                           lstColor);
            }

            return rtnValue;
        }


        internal void ExplodeRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                  EngineRock engineRock, int k, double dblAngle)
        {

            List<Point2d> lstOrigin = new List<Point2d>();
            List<Double> lstAngle = new List<Double>();
            List<enumSize> lstSize = new List<enumSize>();
            List<int> lstIndexColor = new List<int>();
            //List<Double> lstDistance = new List<double>();
            List<Double> lstScale = new List<double>();
            List<int> lstColor = new List<int>();


            lstOrigin.Add(engineRock.ptBlockOrigin);
            lstAngle.Add(dblAngle);
            lstSize.Add(StaticRock.lstEngineRock[k].RockSize);
            lstIndexColor.Add(engineRock.intColorIndex);
            //lstDistance.Add(StaticRock.lstEngineRock[k].dblDistance * 2);
            lstScale.Add(engineRock.dblScale);
            lstColor.Add(engineRock.intColorIndex);

            AddNewRock(acTrans, acDb, acBlkTbl, lstOrigin,
                       lstAngle,
                       lstSize,
                       lstIndexColor);


            AddExplode(lstOrigin,
                       lstScale,
                       lstColor);
        }

        internal void AddExplode(List<Point2d> lstOrigin,
                                 List<double> lstScale,
                                 List<int> lstColor)
        {
            clsCreateExplode clsCreateExplode = new clsCreateExplode();
            for (int i = 0; i < lstOrigin.Count; i++)
            {
                clsCreateExplode.AddExplode(lstOrigin[i], lstScale[i], lstColor[i]);
            }
        }

        internal void AddNewRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                 List<Point2d> lstOrigin,
                                 List<Double> lstAngle,
                                 List<enumSize> lstSize,
                                 List<int> lstColorIndex)

        {
            clsCreateExplode clsCreateExplode = new clsCreateExplode();
            for (int i = 0; i < lstSize.Count; i++)
            {
                if (lstSize[i] != enumSize.Small)
                {
                    List<EngineRock> lstGameRock =
                        clsCreateExplode.CreateNewExplodedRock(acTrans, acDb, acBlkTbl,
                                                               lstSize[i],
                                                               lstOrigin[i],
                                                               lstAngle[i],
                                                               lstColorIndex[i]);

                    for (int k = 0; k < lstGameRock.Count; k++)
                        StaticRock.lstEngineRock.Add(lstGameRock[k]);
                }
            }
        }
    }
}