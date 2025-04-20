using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.CustomClass.Game;
using AutoRoids.GameBuild.CreateLevel;
using AutoRoids.UserFormCode;

namespace AutoRoids.Engine.EngineLevel
{
    internal class clsCreateEngineRock
    {
        private void DeleteGameRock(Transaction acTrans)
        {
            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;

            if (lstEngineRock != null)
            {
                List<BlockReference> lstBlockReference = new List<BlockReference>();

                for (int i = 0; i < lstEngineRock.Count; i++)
                    lstBlockReference.Add(lstEngineRock[i].acBlkRef);

                lstBlockReference.DeleteBlockReference(acTrans);

                StaticRock.lstEngineRock.Clear();
            }
        }

        internal List<EngineRock> CreateGameRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                                 Boolean bolGameStart = true)
        {
            clsReg clsReg = new clsReg();
            clsReg.GetRockCount(out int intRockCount);

            List<enumSize> lstRockSize = new List<enumSize> { enumSize.Large };

            DeleteGameRock(acTrans);

            List<EngineRock> rtnValue = new List<EngineRock>();

            clsCreateRock clsCreateRock = new clsCreateRock();
            List<GameRockData> lstGameRockData = clsCreateRock.GetRockData(intRockCount, lstRockSize, bolGameStart);

            // Return Block Reference and Original Rock Points
            (List<BlockReference> lstBlockReference, List<String> lstBlockName,
             List<List<Point2d>> lstLstOriginalPoint) =
                clsCreateRock.AddBlockReference(acTrans, acDb, acBlkTbl, lstGameRockData);

            for (int i = 0; i < lstGameRockData.Count; i++)
            {
                BlockReference acBlkRef = lstBlockReference[i];
                String strBlockName = lstBlockName[i];
                Point2d ptBlockOrigin = lstGameRockData[i].ptBlockOrigin;
                double dblBlockRotation = lstGameRockData[i].dblBlockRotation;
                double dblScale = lstGameRockData[i].dblScale;
                double dblRotation = lstGameRockData[i].dblRotation;
                double dblAngle = lstGameRockData[i].dblAngle;
                double dblDistance = lstGameRockData[i].dblDistance;
                enumSize RockSize = lstGameRockData[i].RockSize;
                int intColorIndex = lstGameRockData[i].intColorIndex;
                List<Point2d> lstOriginal = lstLstOriginalPoint[i];

                acBlkRef.Rotation = dblBlockRotation.ToRadians();

                EngineRock engineRock =
                         new EngineRock(acBlkRef, strBlockName, ptBlockOrigin, dblBlockRotation, dblScale,
                                        dblAngle, dblRotation, dblDistance,
                                        lstOriginal, RockSize, intColorIndex);

                rtnValue.Add(engineRock);
            }
            return rtnValue;
        }
    }
}