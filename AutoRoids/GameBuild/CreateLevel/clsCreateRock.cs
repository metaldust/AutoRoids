using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Formulas;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.CustomClass.Game;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.GameBuild.CreateLevel
{
    internal class clsCreateRock
    {
        internal List<EngineRock> CreateGameRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                               int intRockCount, List<enumSize> lstRockSize, Boolean bolGameStart = true)
        {
            List<GameRockData> lstGameRockData = GetRockData(intRockCount, lstRockSize, bolGameStart);

            (List<BlockReference> lstBlockReference, List<String> lstBlockName,
             List<List<Point2d>> lstLstRockPoint) =
                AddBlockReference(acTrans, acDb, acBlkTbl, lstGameRockData);

            List<EngineRock> lstGameRock =
                AddGameRock(lstBlockReference, lstBlockName, lstLstRockPoint, lstGameRockData);

            return lstGameRock;
        }

        internal (List<BlockReference> lstBlockReference,
                  List<String> lstBlockName,
                  List<List<Point2d>> lstLstRockPoint)
            AddBlockReference(Transaction acTrans, Database acDb, BlockTable acBlkTbl, List<GameRockData> lstGameRockData)
        {
            clsAddBlock clsAddBlock = new clsAddBlock();
            List<BlockReference> lstBlockReference = new List<BlockReference>();
            List<String> lstBlockName = new List<String>();

            List<List<Point2d>> lstLstRockPoint = new List<List<Point2d>>();

            for (int i = 0; i < lstGameRockData.Count; i++)
            {
                string strBlockName = String.Format("Rock-{0}", lstGameRockData[i].intPosIndex);
                lstBlockReference.Add(clsAddBlock.BuildRock(acTrans, acDb, acBlkTbl,
                                                            strBlockName, "Continuous",
                                                            lstGameRockData[i].ptBlockOrigin.ToPoint3d(),
                                                            lstGameRockData[i].dblRotation,
                                                            lstGameRockData[i].intColorIndex,
                                                            lstGameRockData[i].intPosIndex,
                                                            lstGameRockData[i].dblPolylineWidth,
                                                            lstGameRockData[i].dblScale,
                                                            out List<Point2d> lstRockPoint));

                lstBlockName.Add(strBlockName);
                lstLstRockPoint.Add(lstRockPoint);
            }

            return (lstBlockReference, lstBlockName, lstLstRockPoint);
        }

        internal List<EngineRock> AddGameRock(List<BlockReference> lstBlockReference,
                                              List<String> lstBlockName,
                                              List<List<Point2d>> lstLstRockPoint,
                                              List<GameRockData> lstGameRockData)
        {
            List<EngineRock> lstEngineRock = new List<EngineRock>();

            for (int i = 0; i < lstGameRockData.Count; i++)
            {
                BlockReference acBlkRef = lstBlockReference[i];
                string strBlockName = lstBlockName[i];
                Point2d ptBlockOrigin = lstGameRockData[i].ptBlockOrigin;
                double dblBlockRotation = lstGameRockData[i].dblBlockRotation;
                double dblScale = lstGameRockData[i].dblScale;
                double dblRotation = lstGameRockData[i].dblRotation;
                double dblAngle = lstGameRockData[i].dblAngle;
                double dblDistance = lstGameRockData[i].dblDistance;
                enumSize RockSize = lstGameRockData[i].RockSize;
                int intColorIndex = lstGameRockData[i].intColorIndex;
                List<Point2d> lstOriginal = lstLstRockPoint[i];

                acBlkRef.Rotation = dblRotation.ToRadians();

                EngineRock engineRock =
                         new EngineRock(acBlkRef, strBlockName, ptBlockOrigin, dblBlockRotation, dblScale,
                                        dblAngle, dblRotation, dblDistance,
                                        lstOriginal, RockSize, intColorIndex);

                lstEngineRock.Add(engineRock);
            }

            // Init Matrix 3d
            for (int k = 0; k < lstEngineRock.Count; k++)
            {
                clsUpdateMatrix EngineFormula = new clsUpdateMatrix();
                EngineRock engineRock = lstEngineRock[k];
                EngineFormula.UpdateMatrix(ref engineRock);
                lstEngineRock[k] = engineRock;
            }

            return lstEngineRock;
        }

        internal List<GameRockData> GetRockData(int intRockCount, List<enumSize> lstRockSize, Boolean bolGameStart)
        {
            List<Double> lstRotation = new List<double>();
            List<int> lstColorIndex = new List<int>();

            List<GameRockData> lstGameRockData = new List<GameRockData>();

            clsCreatePoints clsGeneratePoints = new clsCreatePoints();
            List<Point2d> lstOrigin = clsGeneratePoints.GetPoints(intRockCount, bolGameStart);

            Random random = clsGeneratePoints.GetRandom();

            List<int> lstPosIndex = new List<int>();

            int intColor = 1;

            for (int i = 0; i < intRockCount; i++)
            {
                lstRotation.Add(random.Next(0, 360));
                lstPosIndex.Add(random.Next(0, 4));

                lstColorIndex.Add(intColor);
                intColor = intColor.GetCounter(1, 4);
            }

            clsCreateLevel clsCreateLevel = new clsCreateLevel();
            clsCreateLevel.GetDataRock(intRockCount,
                                       ref lstRockSize,
                                       out List<Double> lstTravelAngle,
                                       out List<Double> lstTravelRotation,
                                       out List<Double> lstTravelDistance,
                                       out List<Double> lstScale);

            for (int i = 0; i < intRockCount; i++)
            {
                lstGameRockData.Add(new GameRockData(lstRockSize[i],
                                    lstOrigin[i],
                                    lstRotation[i],
                                    lstColorIndex[i],
                                    lstPosIndex[i],
                                    lstTravelAngle[i],
                                    lstTravelRotation[i],
                                    lstTravelDistance[i],
                                    lstScale[i],
                                    StaticRock.dblLineWidth));
            }

            return lstGameRockData;
        }
    }
}