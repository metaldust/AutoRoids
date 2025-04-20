using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.GameBuild.GetPoints;

namespace AutoRoids.GameBuild.Geometry
{
    internal class clsAddBlock
    {
        internal BlockReference BuildRock(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                          string strBlockName, string strLineType,
                                          Point3d ptOrigin, double dblRotation,
                                          int intColor, int intPos, double dblWidth,
                                          double dblScale, out List<Point2d> lstPoint)
        {
            BlockReference rtnValue = null;

            // Open the Block table for read
            clsGetPointsRock clsGetPointsRock = new clsGetPointsRock();
            lstPoint = clsGetPointsRock.GetRock(intPos);

            if (!acBlkTbl.Has(strBlockName))
            {
                clsAddGeometry clsAddGeometry = new clsAddGeometry();
                clsAddGeometry.LoadLinetype(strLineType);

                Polyline acPline = clsAddGeometry.AddPolyLine(acTrans, acDb, lstPoint, 0, dblWidth);

                acPline.Linetype = strLineType;

                List<Entity> lstEntity = new List<Entity> { (Entity)acPline };

                rtnValue = CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);

                rtnValue.Rotation = dblRotation;
                rtnValue.Position = ptOrigin;
                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();
                rtnValue = clsAddBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);
                rtnValue.Rotation = dblRotation;
                rtnValue.Position = ptOrigin;
                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }

            return rtnValue;
        }

        internal BlockReference BuildBorder(Transaction acTrans, Database acDb, BlockTable acBlkTbl, string strBlockName,
                                            int intColor, int intPos, double dblWidth,
                                            double dblScale, string strLineType, out List<Point2d> lstPoint)
        {
            BlockReference rtnValue = null;
            lstPoint = new List<Point2d>();

            if (!acBlkTbl.Has(strBlockName))
            {
                clsAddGeometry clsAddGeometry = new clsAddGeometry();
                clsAddGeometry.LoadLinetype(strLineType);

                clsGetPointsBorder clsGetPointsBorder = new clsGetPointsBorder();
                lstPoint = clsGetPointsBorder.GetBorder(intPos, false);

                Polyline acPline = clsAddGeometry.AddPolyLine(acTrans, acDb, lstPoint, 0, dblWidth);

                acPline.Linetype = strLineType;

                List<Entity> lstEntity = new List<Entity> { (Entity)acPline };

                rtnValue = CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();
                rtnValue = clsAddBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }

            return rtnValue;
        }

        internal BlockReference BuildShip(Transaction acTrans, Database acDb, BlockTable acBlkTbl, string strBlockName,
                                          int intColor, int intPos, double dblWidth,
                                          string strLineType, Boolean bolClosePolyline,
                                          out List<Point2d> lstPoint, Boolean bolIsVisible, out double dblScale)
        {
            dblScale = 24 * StaticRock.dblGameScale;

            clsGetPointsShip clsGetPointsShip = new clsGetPointsShip();
            lstPoint = clsGetPointsShip.GetShip(intPos);

            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                clsAddGeometry clsAddGeometry = new clsAddGeometry();
                clsAddGeometry.LoadLinetype(strLineType);

                Polyline acPline = clsAddGeometry.AddPolyLine(acTrans, acDb, lstPoint, 0, dblWidth, bolClosePolyline);

                acPline.Linetype = strLineType;

                List<Entity> lstEntity = new List<Entity> { (Entity)acPline };

                rtnValue = CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();
                rtnValue = clsAddBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }

            rtnValue = acTrans.GetObject(rtnValue.ObjectId, OpenMode.ForWrite) as BlockReference;
            rtnValue.Visible = bolIsVisible;

            return rtnValue;
        }

        internal BlockReference BuildBullet(Transaction acTrans, Database acDb, BlockTable acBlkTbl, string strBlockName,
                                          int intColor, double dblScale)
        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                List<Entity> lstEntity = CreateBullet(acTrans, acDb, acBlkTbl);

                rtnValue = CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();
                rtnValue = clsAddBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }

            return rtnValue;
        }

        internal BlockReference BuildStar(Transaction acTrans, Database acDb, BlockTable acBlkTbl, string strBlockName,
                                          int intColor, double dblScale)
        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                clsAddGeometry clsAddGeometry = new clsAddGeometry();

                List<Point2d> lstPoints = new List<Point2d> { new Point2d(), new Point2d() };
                Polyline acPline = clsAddGeometry.AddPolyLine(acTrans, acDb, lstPoints, 0);

                List<Entity> lstEntity = new List<Entity> { acPline };

                rtnValue = CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }
            else
            {
                clsAddBlock clsAddBlock = new clsAddBlock();
                rtnValue = clsAddBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);

                rtnValue.ColorIndex = intColor;
                rtnValue.ScaleFactors = new Scale3d(dblScale, dblScale, dblScale);
            }

            return rtnValue;
        }

        internal BlockReference CreateBlock(Transaction acTrans, Database acDb,
                                            List<Entity> lstEntity, string strBlockname, BlockTable acBlkTbl)
        {
            if (!acBlkTbl.Has(strBlockname))
            {
                BlockTableRecord btr = new BlockTableRecord { Name = strBlockname };

                ObjectId objId = acBlkTbl.ObjectId;

                if (objId.IsObjectIdValid(acDb))
                {
                    acBlkTbl = acTrans.GetObject(acBlkTbl.ObjectId, OpenMode.ForWrite) as BlockTable;

                    ObjectId btrId = acBlkTbl.Add(btr);
                    acTrans.AddNewlyCreatedDBObject(btr, true);

                    ObjectIdCollection objIdColl = new ObjectIdCollection();

                    for (int i = 0; i < lstEntity.Count; i++)
                        objIdColl.Add(lstEntity[i].ObjectId);

                    btr.AssumeOwnershipOf(objIdColl);

                    BlockTableRecord ms = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    BlockReference br = new BlockReference(Point3d.Origin, btrId);

                    ms.AppendEntity(br);

                    acTrans.AddNewlyCreatedDBObject(br, true);

                    return br;
                }
            }

            return null;
        }

        internal BlockReference InsertBlock(string strBlockname, string strLayerName, int intColor,
                                          Transaction acTrans, Database acDb)
        {
            BlockReference rtnValue = null;
            {
                // Open the Block table for read
                BlockTable acBlkTbl = default;

                try
                {
                    acBlkTbl = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead);
                }
                catch (System.Exception)
                {
                    return null;
                }

                ObjectId blkRecId = ObjectId.Null;
                if (acBlkTbl.Has(strBlockname))
                {
                    blkRecId = acBlkTbl[strBlockname];
                }
                // Insert the block into the current space
                if (blkRecId != ObjectId.Null)
                {
                    BlockReference acBlkRef = new BlockReference(Point3d.Origin, blkRecId) { Layer = strLayerName };
                    acBlkRef.ColorIndex = intColor;
                    BlockTableRecord btr = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    btr.AppendEntity(acBlkRef);
                    acTrans.AddNewlyCreatedDBObject(acBlkRef, true);
                    rtnValue = acBlkRef;
                }
            }

            return rtnValue;
        }

        internal List<Entity> CreateBullet(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            clsAddGeometry clsAddGeometry = new clsAddGeometry();
            Entity acCircle = clsAddGeometry.AddCircle(acTrans, acDb, 1, 0, 0);

            clsAddHatch clsAddHatch = new clsAddHatch();
            string strPattern = "Solid";

            Hatch acHatch = clsAddHatch.ProcessHatch(acTrans, acDb, acBlkTblRec,
                                                     strPattern, 1, acCircle);

            List<Entity> lstEntity = new List<Entity> { (Entity)acCircle, (Entity)acHatch };

            return lstEntity;
        }
    }
}