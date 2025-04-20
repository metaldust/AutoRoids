using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.Geometry
{
    internal class clsAddGeometry
    {
        internal Polyline AddPolyLine(Transaction acTrans, Database acDb, List<Point2d> lstPoint, int intColor, double dblWidth = 0, Boolean bolClosePolyline = true)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            Polyline acPline = new Polyline
            {
                Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor)
            };

            acPline.SetDatabaseDefaults();

            for (int i = 0; i < lstPoint.Count; i++)
                acPline.AddVertexAt(i, new Point2d(lstPoint[i].X, lstPoint[i].Y), 0, dblWidth, dblWidth);

            if (bolClosePolyline && lstPoint.Count > 2)
                acPline.Closed = true;

            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acPline);
            acTrans.AddNewlyCreatedDBObject(acPline, true);

            acPline = acTrans.GetObject(acPline.ObjectId, OpenMode.ForWrite) as Polyline;

            return acPline;
        }

        internal Entity AddCircle(Transaction acTrans, Database acDb, double dblSize, int intColor, double dblWidth = 0)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            Circle acCircle = new Circle
            {
                Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor),
                Radius = dblSize
            };

            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acCircle);
            acTrans.AddNewlyCreatedDBObject(acCircle, true);

            // acPline = acTrans.GetObject(acPline.ObjectId, OpenMode.ForWrite) as Polyline;

            return acCircle;
        }

        public static void LoadLinetype(string strLineType)
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Linetype table for read
                LinetypeTable acLineTypTbl;
                acLineTypTbl = acTrans.GetObject(acCurDb.LinetypeTableId,
                                                    OpenMode.ForRead) as LinetypeTable;

                if (acLineTypTbl.Has(strLineType) == false)
                {
                    // Load the Center Linetype
                    acCurDb.LoadLineTypeFile(strLineType, "acad.lin");
                }

                // Save the changes and dispose of the transaction
                acTrans.Commit();
            }
        }

        internal DBPoint AddPoint(Transaction acTrans, Database acDb, Point2d ptPoint, int intColor)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            DBPoint acPoint = new DBPoint
            {
                Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor),
                Position = ptPoint.ToPoint3d()
            };

            acPoint.SetDatabaseDefaults();

            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acPoint);
            acTrans.AddNewlyCreatedDBObject(acPoint, true);

            return acPoint;
        }
    }
}