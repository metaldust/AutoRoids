using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AutoRoids.Common.Static;
using AutoRoids.CustomClass.Engine;
using AutoRoids.GameBuild.Geometry;

namespace AutoRoids.Engine.EngineLevel
{
    internal class clsCreateEngineShip
    {
        internal void DeleteEngineShip(Transaction acTrans)
        {
            EngineShip EngineRock = StaticRock.engineShip;

            if (EngineRock != null)
            {
                EngineRock.lstBlkRefShip.DeleteBlockReference(acTrans);
                EngineRock.lstBlkRefShield.DeleteBlockReference(acTrans);

                EngineRock.lstBlkRefShip.Clear();
                EngineRock.lstBlkRefShield.Clear();
            }
        }

        internal EngineShip CreateShip(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            DeleteEngineShip(acTrans);

            clsAddBlock clsAddBlock = new clsAddBlock();

            double dblWidth = 0.003;

            // Color Index
            int intShip = 3;
            int intThrust = 2;
            int intShield = 2;

            double dblScale = 0.0;
            double dblDistance = 1.0;

            BlockReference acBlkRefShip = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Ship", intShip, 0, dblWidth, "Continuous", true, out List<Point2d> lstPointShip, true, out dblScale);
            BlockReference acBlkRefThrust = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Thrust", intThrust, 1, dblWidth, "Continuous", false, out List<Point2d> lstPointThrust, false, out dblScale);

            BlockReference acBlkRefInside = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Inside", intShield, 2, dblWidth, "Dashed", true, out List<Point2d> lstPointInside, false, out dblScale);
            BlockReference acBlkRefOutside = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Outside", intShield, 3, dblWidth, "Dashed", true, out List<Point2d> lstPointOutside, false, out dblScale);

            List<BlockReference> lstBlkRefShip = new List<BlockReference> { acBlkRefShip, acBlkRefThrust };
            List<BlockReference> lstBlkRefShield = new List<BlockReference> { acBlkRefInside, acBlkRefOutside };

            List<List<Point2d>> lstLstOriginalShip = new List<List<Point2d>> { lstPointShip, lstPointThrust };
            List<List<Point2d>> lstLstOriginalShield = new List<List<Point2d>> { lstPointInside, lstPointOutside };

            List<double> lstRotationShield = new List<double> { 0, 0 };

            EngineShip EngineShip = new EngineShip(lstBlkRefShip,
                                                   lstBlkRefShield,
                                                   lstLstOriginalShip,
                                                   lstLstOriginalShield,
                                                   lstRotationShield,
                                                   dblScale, dblDistance);
            return EngineShip;
        }

        internal void HideDefault(Transaction acTrans, List<BlockReference> lstBlkRefShip, List<BlockReference> lstBlkRefShield)
        {
            for (int i = 0; i < lstBlkRefShip.Count; i++)
            {
                if (i == 0)
                    (acTrans.GetObject(lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference).Visible = true;
                else
                    (acTrans.GetObject(lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference).Visible = false;
            }

            for (int i = 0; i < lstBlkRefShield.Count; i++)
                (acTrans.GetObject(lstBlkRefShield[1].ObjectId, OpenMode.ForWrite) as BlockReference).Visible = false;
        }

        internal void HideAll(Transaction acTrans, List<BlockReference> lstBlkRefShip, List<BlockReference> lstBlkRefShield)
        {
            for (int i = 0; i < lstBlkRefShip.Count; i++)
            {
                (acTrans.GetObject(lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference).Visible = false;
            }

            for (int i = 0; i < lstBlkRefShield.Count; i++)
                (acTrans.GetObject(lstBlkRefShield[1].ObjectId, OpenMode.ForWrite) as BlockReference).Visible = false;
        }
    }
}