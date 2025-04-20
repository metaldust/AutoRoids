using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.UserFormCode;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreateExplode
    {
        internal void AddExplode(Point2d ptOrigin, double dblScale, int intColor)
        {
            if (StaticRock.lstExplode == null)
                StaticRock.lstExplode = new List<EngineExplode>();

            List<Double> lstDirection = new List<double>();

            int intCount = 8;
            double dblAngle = 360.0 / intCount;
            for (int i = 0; i < intCount; i++)
                lstDirection.Add(i * dblAngle);

            for (int i = 0; i < lstDirection.Count; i++)
            {
                EngineExplode engineExplode = new EngineExplode(ptOrigin, lstDirection[i], dblScale, intColor);

                StaticRock.lstExplode.Add(engineExplode);
            }
        }

        internal static double dblRotateAngle = 0;

        internal void AddDeathBlossom(Point2d ptOrigin)
        {
            if (StaticRock.lstExplode == null)
                StaticRock.lstExplode = new List<EngineExplode>();

            List<Double> lstDirection = new List<double>();

            int intCount = 6;
            double dblAngle = (360.0 / intCount);

            dblAngle = dblAngle.NormalizeAngle();

            for (int i = 0; i < intCount; i++)
                lstDirection.Add(dblAngle * i + dblRotateAngle);

            for (int i = 0; i < lstDirection.Count; i++)
            {
                EngineBullet engineBullet = new EngineBullet(ptOrigin, lstDirection[i]);
                //engineBullet.intColor = 1;

                StaticRock.lstBullets.Add(engineBullet);
            }

            dblRotateAngle += 5;
            dblRotateAngle = dblRotateAngle.NormalizeAngle();
        }

        internal List<EngineRock> CreateNewExplodedRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                                    enumSize RockSize, Point2d ptOrigin, double dblAngle,
                                                    int intColorIndex)
        {
            clsCreatePoints clsGeneratePoints = new clsCreatePoints();
            Random random = clsGeneratePoints.GetRandom();

            int intRockCount = 2; //  random.Next(2, 4);

            clsCreateLevel clsCreateLevel = new clsCreateLevel();
            clsCreateLevel.GetNextSize(ref RockSize);

            List<enumSize> lstRockSize = new List<enumSize> { RockSize };

            clsCreateRock clsCreateRock = new clsCreateRock();
            List<EngineRock> lstGameRock = clsCreateRock.CreateGameRock(acTrans, acDb, acBlkTbl, intRockCount, lstRockSize);

            // Override the default locations and set them to match the center of the existing target
            clsReg clsReg = new clsReg();

            clsReg.GetRockMinDistance(out double dblMin);
            clsReg.GetRockMaxDistance(out double dblMax);

            for (int i = 0; i < lstGameRock.Count; i++)
            {
                double dblRandomAngle = random.GetDouble(0, 45);

                lstGameRock[i].acBlkRef.Position = ptOrigin.ToPoint3d();
                lstGameRock[i].acBlkRef.ColorIndex = intColorIndex;

                double dblNewAngle1 = ((dblAngle + 90) + dblRandomAngle).NormalizeAngle();
                double dblNewAngle2 = ((dblAngle - 90) - dblRandomAngle).NormalizeAngle();

                if (i.IsEven())
                    lstGameRock[i].dblAngle = dblNewAngle1;
                else
                    lstGameRock[i].dblAngle = dblNewAngle2;

                lstGameRock[i].dblDistance = random.GetDouble(dblMin + 2, dblMax + 2);

                lstGameRock[i].dblRotation *= 2;
                lstGameRock[i].ptBlockOrigin = ptOrigin;
                lstGameRock[i].intColorIndex = intColorIndex;
            }

            return lstGameRock;
        }

        internal void EraseGameExplode()
        {
            if (StaticRock.lstExplode != null)
                StaticRock.lstExplode.Clear();
        }
    }
}